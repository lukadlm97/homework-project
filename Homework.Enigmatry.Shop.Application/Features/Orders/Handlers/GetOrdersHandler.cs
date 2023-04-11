using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests;
using Homework.Enigmatry.Shop.Application.Features.Orders.Validators;
using Homework.Enigmatry.Shop.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Homework.Enigmatry.Shop.Application.Features.Orders.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersRequest, OperationResult<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly LogTraceData _logTraceData;

        public GetOrdersHandler(IOrderRepository orderRepository, IMapper mapper, LogTraceData logTraceData)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logTraceData = logTraceData;
        }
        public async Task<OperationResult<OrderDto>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(GetOrdersHandler), nameof(Handle)));

            var validator = new GetOrdersValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new OperationResult<OrderDto>(OperationStatus.InvalidValues,
                    ErrorMessage: string.Join(',', validationResult.Errors
                                                                    .Select(x => x.ErrorMessage)));
            }

            var orders = await _orderRepository.GetAll();
            if (orders.IsNullOrEmpty())
            {
                return new OperationResult<OrderDto>(OperationStatus.NotExist);
            }
           

            return new OperationResult<OrderDto>(OperationStatus.Success,
                Results: orders.Select(x=>new OrderDto(
                    x.Id,
                    new ArticleDto(x.ArticleId, x.Article.Name, x.Article.Price),
                    x.Price,
                    new CustomerDto(x.CustomerId, x.Customer.Username)))
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize).ToList(),
                TotalAvailable: orders.Count());
        }
    }
}
