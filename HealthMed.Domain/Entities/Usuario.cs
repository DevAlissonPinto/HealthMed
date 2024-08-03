using HealthMed.Domain.Entities.Base;

namespace HealthMed.Domain.Entities;

public class Usuario : EntityBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Senha { get; set; }

    // Navegação
    

    public Usuario()
    {

    }
    public Usuario(string nome, string email, string senha, string cpf)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        CPF = cpf;
    }
}
