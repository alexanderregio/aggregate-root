## Aggregate Root: Controlando a Consistência do Cliente

Este documento explora o conceito de Aggregate Root em Domain Driven Design (DDD) com exemplos de código em C#, utilizando as classes `Cliente` e `Endereco` como exemplo prático.

### O que é um Aggregate Root?

Um Aggregate Root é um padrão de design utilizado em DDD para garantir a consistência de um conjunto de objetos relacionados.  Ele atua como um **guardião**, controlando o acesso e as modificações dentro do grupo, garantindo que as regras de negócio sejam aplicadas de forma coerente. 

No nosso exemplo, a classe `Cliente` é o Aggregate Root, responsável por gerenciar as informações de um cliente, incluindo seus endereços.

### Implementação de Aggregate Roots em C#

**1. Classe Base `Entity`**

```C#
public abstract class Entity(Guid id) : IEquatable<Entity>
{
    public Guid Id { get; private init; } = id;

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;

        if (obj.GetType() != GetType())
            return false;

        if (obj is not Entity entity)
            return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }

    public static bool operator ==(Entity first, Entity second)
        => first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entity first, Entity second)
        => !(first == second);

}
```

* **`Entity` é uma classe abstrata:** Isso significa que ela não pode ser instanciada diretamente, mas serve como base para outras entidades.
* **`Id` é uma propriedade:** Armazena o identificador único da entidade.
* **`Equals`, `GetHashCode` e operadores `==` e `!=`:** Implementados para garantir a comparação de igualdade entre entidades.

**2. Classe `Cliente` (Aggregate Root)**

```C#
public sealed class Cliente
    (Guid id, string nome, EnderecoEmail email, DateTime dataNascimento, string telefone, Endereco[] enderecos) : Entity(id)
{
    public string Nome { get; set; } = nome;

    public EnderecoEmail Email { get; set; } = email;

    public DateTime DataNascimento { get; set; } = dataNascimento;

    public string Telefone { get; set; } = telefone;

    public Endereco[] Enderecos { get; set; } = enderecos;
}
```

* **`Cliente` é uma classe selada:** Isso significa que ela não pode ser herdada por outras classes.
* **`Cliente` herda da classe `Entity`:** Todas as entidades devem herdar da classe `Entity` para garantir um identificador único.
* **`Cliente` possui atributos relacionados:** Nome, Email, DataNascimento, Telefone e Enderecos.
* **`Enderecos` é um array:** Isso significa que um cliente pode ter múltiplos endereços.
* **`Cliente` é o Aggregate Root:** É responsável por gerenciar seus próprios dados, incluindo a manipulação dos endereços.

**3. Classe `Endereco` (Value Object)**

```C#
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
```

* **`Endereco` é um Value Object:** Ele representa um valor imutável e não possui um identificador único.
* **`Endereco` é usado dentro do `Cliente`:** O cliente possui um array de `Enderecos`, mostrando a relação entre o Aggregate Root e outros objetos.
* **`Endereco` é imutável:** Isso significa que seus valores não podem ser alterados após a criação. As alterações devem ser feitas criando um novo objeto `Endereco`.

### Conclusões

Este exemplo demonstra como utilizar um Aggregate Root em DDD com C#. A classe `Cliente` atua como o Aggregate Root, controlando as operações de acesso e modificação dos seus atributos, incluindo o array de `Enderecos`. O Aggregate Root encapsula a lógica de negócio relacionada ao cliente, garantindo a consistência dos dados.

**Importante:** A validação de dados e regras de negócio devem ser implementadas dentro da classe `Cliente`, garantindo que as alterações nos dados do cliente e seus endereços sejam consistentes.

### Benefícios de usar Aggregate Roots

* **Consistência:**  O Aggregate Root garante a consistência dos dados dentro do seu grupo, aplicando as regras de negócio de forma coerente.
* **Simplicidade:** Abstrai a complexidade do domínio, simplificando a interação com o agregado.
* **Coerência Transacional:**  As operações dentro de um agregado são tratadas como uma única transação, garantindo a integridade dos dados.
* **Escalabilidade:** Facilita a implementação de mecanismos de persistência, já que o Aggregate Root controla os dados dentro do agregado.

### Quando usar Aggregate Roots

* Quando você precisa garantir a consistência de um grupo de objetos relacionados.
* Quando você deseja encapsular a lógica de negócio de um determinado domínio.
* Quando você deseja simplificar a interação com os objetos do domínio.

### Exemplos de Aggregate Roots

* **Pedido de compra:** O pedido é o Aggregate Root, gerenciando os itens do pedido.
* **Conta bancária:** A conta é o Aggregate Root, gerenciando os movimentos de crédito e débito.
* **Cliente:** O cliente é o Aggregate Root, gerenciando seus endereços e pedidos.

### Conclusões

Aggregate Roots são um conceito fundamental em DDD que permite modelar o domínio de forma precisa e eficiente. Ao utilizar Aggregate Roots, você pode melhorar a qualidade do código, tornando-o mais legível, mais seguro e mais fácil de manter.



