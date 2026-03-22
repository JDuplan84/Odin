namespace Odin.Core.Models;

/// <summary>
/// Represents a condition attached to a dialogue INFO record.
/// </summary>
public record DialogueCondition
{
    public required string Function { get; init; }
    public string? ComparisonValue { get; init; }
    public ConditionOperator Operator { get; init; }
    public string? Reference { get; init; }
}

public enum ConditionOperator
{
    EqualTo,
    NotEqualTo,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo
}
