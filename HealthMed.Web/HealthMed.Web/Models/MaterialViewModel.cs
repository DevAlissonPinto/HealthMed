using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HealthMed.Web.Models;

public class MaterialViewModel
{
    public int Id { get; set; }

    [DisplayName("Nome")]
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [MaxLength(50, ErrorMessage = "O campo Nome deve ter até 50 caracteres")]
    [MinLength(5, ErrorMessage = "O campo Nome deve ter no mínimo 5 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [DisplayName("Codigo")]
    [Required(ErrorMessage = "O campo Codigo é obrigatório")]
    [MaxLength(10, ErrorMessage = "O campo Codigo deve ter até 10 caracteres")]
    [MinLength(2, ErrorMessage = "O campo Codigo deve ter no mínimo 2 caracteres")]
    public string Codigo { get; set; } = string.Empty;
}
