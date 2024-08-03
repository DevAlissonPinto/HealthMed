using HealthMed.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthMed.Domain.Entities
{
    public class PacienteAgendaConsulta : EntityBase
    {
        public int HorarioId { get; set; }
        public AgendaHorarioMedico Horario { get; set; }
        public int MedicoId { get; set; }
        public ProfissionalMedico Medico { get; set; }
        public int PacienteId { get; set; }
        public Usuario Paciente { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
