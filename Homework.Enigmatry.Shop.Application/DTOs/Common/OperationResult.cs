using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Shop.Application.DTOs.Common
{
    public record OperationResult<T>(T? Result, IReadOnlyList<T>? Results, OperationStatus Status);
}
