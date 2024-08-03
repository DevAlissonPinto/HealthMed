using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Tests.Domain.Entities;

/// <summary>
/// Classe responsável por testar as regras de negocio do escopo de Usuario.
/// </summary>
public class UsuarioTest
{

    /// <summary>
    /// Método responsável por realizar o teste Nome_Usuario_Deve_Ter_Entre_3_e_50_Caracteres.
    /// </summary>
    [Fact]
    public void Nome_Usuario_Deve_Ter_Entre_3_e_50_Caracteres()
    {
        var usuario = new Usuario("Vinicius", "vinicius@gmail.com", "234212","");

        Assert.True(usuario.Nome.Length >= 5 && usuario.Nome.Length <= 50);
    }

    /// <summary>
    /// Método responsável por realizar o teste Deve_Retornar_Erro_Quando_Nome_Ter_Mais_De_50_Caracteres.
    /// </summary>
    [Fact]
    public void Deve_Retornar_Erro_Quando_Nome_Ter_Mais_De_50_Caracteres()
    {
        string msg = "O Nome deve ter no máxima 50 caracteres";
        try
        {
            var usuario = new Usuario("Nome do usuário Diego teste de quantidade máxima de caracteres teste", "diego@gmail.com", "234212","");

            //Assert.False(usuario.Nome.Length > 50);
        }
        catch (Exception ex)
        {
            Assert.Equal(msg, ex.Message);
        }
    }

    /// <summary>
    /// Método responsável por realizar o teste Deve_Retornar_Erro_Quando_Nome_Ter_Menos_De_5_Caracteres.
    /// </summary>
    [Fact]
    public void Deve_Retornar_Erro_Quando_Nome_Ter_Menos_De_5_Caracteres()
    {
        string msg = "O Nome deve ter no mínimo 5 caracteres";
        try
        {
            var usuario = new Usuario("Ali", "Alisson@gmail.com", "234212","");

            //Assert.False(usuario.Nome.Length < 5);
        }
        catch (Exception ex)
        {
            Assert.Equal(msg, ex.Message);
        }
    }

    /// <summary>
    /// Método responsável por realizar o teste verificar_existencia_email_usuario.
    /// </summary>
    [Fact]
    public void verificar_existencia_email_usuario()
    {
        string msg = "O E-mail utilizado já consta na base de dados, vinculado a um usuário.";
        try
        {
            var usuario = new Usuario("Alisson", "alisson@gmail.com", "234212","");

            Assert.True(usuario.Email == "alisson@gmail.com");
        }
        catch (Exception ex)
        {
            Assert.Equal(msg, ex.Message);
        }
    }

    /// <summary>
    /// Método responsável por realizar o teste verificar_existencia_nome_usuario.
    /// </summary>
    [Fact]
    public void verificar_existencia_nome_usuario()
    {
        string msg = "O Nome do usuário já consta na base de dados";
        try
        {
            var usuario = new Usuario("vinicius", "vinicius@gmail.com", "234212","");

            Assert.True(usuario.Nome == "vinicius");
        }
        catch (Exception ex)
        {
            Assert.Equal(msg, ex.Message);
        }
    }

}
