﻿using Homework.Enigmatry.Application.Shared.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IVendorRepository
    {
        Task<ArticleDetailsDto?> Get(int id,string vendorHttpClientName,CancellationToken cancellationToken=default);
    }
}
