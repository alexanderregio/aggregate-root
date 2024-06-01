using AggregateRoot.Domain.Primitives;

namespace AggregateRoot.Domain.ValueObjects;

public class Endereco : ValueObject
{
    public string Rua { get; }
    public int Numero { get; }
    public string Complemento { get; }
    public string Bairro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public string Cep { get; }

    private Endereco(string rua, int numero, string complemento, string bairro, string cidade, string estado, string cep)
    {
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;
    }

    public static Endereco Create(string rua, int numero, string complemento, string bairro, string cidade, string estado, string cep)
    {
        if (string.IsNullOrWhiteSpace(rua))
            throw new ArgumentException($"'{nameof(rua)}' não pode ser vazio.", nameof(rua));

        if (numero < 1)
            throw new ArgumentException($"'{nameof(numero)}' não pode ser menor que zero.", nameof(numero));

        if (string.IsNullOrWhiteSpace(complemento))
            throw new ArgumentException($"'{nameof(complemento)}' não pode ser vazio.", nameof(complemento));

        if (string.IsNullOrWhiteSpace(bairro))
            throw new ArgumentException($"'{nameof(bairro)}' não pode ser vazio.", nameof(bairro));

        if (string.IsNullOrWhiteSpace(cidade))
            throw new ArgumentException($"'{nameof(cidade)}' não pode ser vazio.", nameof(cidade));

        if (string.IsNullOrWhiteSpace(estado))
            throw new ArgumentException($"'{nameof(estado)}' não pode ser vazio.", nameof(estado));

        if (string.IsNullOrWhiteSpace(cep))
            throw new ArgumentException($"'{nameof(cep)}' Não pode ser vazio.", nameof(cep));

        // Demais validações necessários ao contexto do sistema ...

        return new(rua, numero, complemento, bairro, cidade, estado, cep);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Rua;
        yield return Numero;
        yield return Complemento;
        yield return Bairro;
        yield return Cidade;
        yield return Estado;
        yield return Cep;
    }
}