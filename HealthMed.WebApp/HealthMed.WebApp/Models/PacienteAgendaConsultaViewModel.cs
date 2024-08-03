using HealthMed.Domain.Entities;


namespace HealthMed.Web.Models
{
    public class PacienteAgendaConsultaViewModel
    {
        public AgendaHorarioMedico Horario { get; set; }
        public int MedicoId { get; set; }
        public ProfissionalMedico Medico { get; set; }
        public int PacienteId { get; set; }
        public Usuario Paciente { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
