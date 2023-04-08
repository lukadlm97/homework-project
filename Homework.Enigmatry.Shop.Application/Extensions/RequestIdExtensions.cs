using Homework.Enigmatry.Shop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Enigmatry.Shop.Application.Extensions
{
    public static class RequestIdExtensions
    {
        public static string CreateArticleCacheKey(this int articleId)
        {
            return nameof(Article) + articleId;
        }
    }
}
