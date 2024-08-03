using HealthMed.Domain.Entities;

namespace HealthMed.Web.Models;

public class TransacaoViewModel
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public decimal Preco { get; set; }
    public bool Tipo { get; set; } 
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    public string UsuarioId { get; set; } 

    public virtual Usuario Usuario { get; set; }
}