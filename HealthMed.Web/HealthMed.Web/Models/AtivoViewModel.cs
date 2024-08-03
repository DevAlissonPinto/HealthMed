using System.ComponentModel.DataAnnotations;

namespace HealthMed.Web.Models;

public class AtivoViewModel
{
    public int Id { get; set; }

    [Display(Name = "Tipo de ativo")]
    public enumTipoAtivoViewModel TipoAtivo { get; set; }

    [Display(Name = "Nome do ativo")]
    public string Nome { get; set; }

    [Display(Name = "Código de negociação")]
    public string Codigo { get; set; }

}

public enum enumTipoAtivoViewModel  
{
    Acao = 1,
    FII = 2,
    ETF = 3,
    Cripto = 4
}
