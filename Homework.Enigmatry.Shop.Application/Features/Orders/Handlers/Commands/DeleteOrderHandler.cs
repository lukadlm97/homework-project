using AutoMapper;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Application.Features.Orders.Handlers.Queries;
using MediatR;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Orders.Validators.Commands;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Shop.Application.Features.Orders.Handlers.Commands
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderRequest, OperationResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly LogTraceData _logTraceData;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderHandler(IUnitOfWork unitOfWork, IMapper mapper, LogTraceData logTraceData)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logTraceData = logTraceData;
        }
        public async Task<OperationResult<OrderDto>> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(GetOrdersHandler), nameof(Handle)));

            var validator = new DeleteOrderValidator(_unitOfWork.CustomerRepository,_unitOfWork.OrderRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new OperationResult<OrderDto>(OperationStatus.InvalidValues,
                    ErrorMessage: string.Join(',', validationResult.Errors
                                                                    .Select(x => x.ErrorMessage)));
            }

            var order = await _unitOfWork.OrderRepository.Get(request.OrderId);
            if (order is null)
            {
                return new OperationResult<OrderDto>(OperationStatus.NotExist);
            }

            order.Price = 0;
            order.IsDeleted=true;
            order.Date = null;

            await _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.Save();


            return new OperationResult<OrderDto>(OperationStatus.Success,
                Result:  new OrderDto(
                    order.Id,
                    new ArticleDto(order.ArticleId, order.Article.Name, order.Article.Price),
                order.Price,
                    new CustomerDto(order.CustomerId, order.Customer.Username),!order.IsDeleted,order.Date));
        }
    }
}
