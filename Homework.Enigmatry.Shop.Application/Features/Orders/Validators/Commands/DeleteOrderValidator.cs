using FluentValidation;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests.Commands;

namespace Homework.Enigmatry.Shop.Application.Features.Orders.Validators.Commands
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderRequest>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderValidator(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;

            RuleFor(request => request.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater then 0");

            RuleFor(request => request.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater then 0"); ;

            RuleFor(request => request.CustomerId)
                .MustAsync(async (id, token) =>
                {
                    return await _customerRepository.Exists(id);
                })
                .WithMessage("You must be register as customer.")
                .MustAsync(async (id, token) =>
                {
                    var customer = await _customerRepository.Get(id);
                    return customer != null &&
                           string.Compare(customer.Role, Constants.Constants.AdminRole, StringComparison.InvariantCulture) == 0;
                }).WithMessage("Customer must be authorized at admin role.");
            RuleFor(request => request.OrderId)
                .MustAsync(async (id, token) =>
                {
                    return await _orderRepository.Exists(id);
                }).WithMessage("Specified order must exist.");
        }
    }
}
