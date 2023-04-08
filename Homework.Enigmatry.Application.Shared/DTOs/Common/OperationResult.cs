using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Application.Shared.DTOs.Common
{
    public record OperationResult<T>(OperationStatus Status,T? Result=default, IReadOnlyList<T>? Results=default,string? ErrorMessage = default);
}
