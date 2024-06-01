using AggregateRoot.Domain.Primitives;

namespace AggregateRoot.Domain.ValueObjects;

public sealed class EnderecoEmail : ValueObject
{
    public string Value { get; }

    private const int MaxLength = 50;

    private EnderecoEmail(string value)
        => Value = value;

    public static EnderecoEmail Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Email inválido");

        if (value.Length > MaxLength)
            throw new ArgumentNullException(nameof(value), $"Email deve ter no máximo {MaxLength} caracteres");

        // Valida padrão do e-mail, etc...

        return new(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}