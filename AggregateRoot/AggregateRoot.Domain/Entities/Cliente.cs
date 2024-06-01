using AggregateRoot.Domain.Primitives;
using AggregateRoot.Domain.ValueObjects;

namespace AggregateRoot.Domain.Entities;

public sealed class Cliente
    (Guid id, string nome, EnderecoEmail email, DateTime dataNascimento, string telefone, Endereco[] enderecos) : Entity(id)
{
    public string Nome { get; set; } = nome;

    public EnderecoEmail Email { get; set; } = email;

    public DateTime DataNascimento { get; set; } = dataNascimento;

    public string Telefone { get; set; } = telefone;

    public Endereco[] Enderecos { get; set; } = enderecos;
}
