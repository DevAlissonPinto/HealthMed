namespace HealthMed.Web.Models
{
    public class AgendaHorarioMedicoViewModel
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; }
        public int MedicoId { get; set; }
        public MedicoViewModel Medico {get; set;}
    }
}
