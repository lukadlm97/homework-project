﻿using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        Task<Customer?> GetByUsername(string username,CancellationToken cancellationToken=default);
    }
}
