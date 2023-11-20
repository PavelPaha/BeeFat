using System.Diagnostics.CodeAnalysis;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Domain.Models.User;

[method: SetsRequiredMembers]
public class BodyParameters(double weight, double height, int age) : ValueType<BodyParameters>
{
    public required double Weight { get; set; } = weight;
    public required double Height { get; set; } = height;
    public required int Age { get; set; } = age;
}