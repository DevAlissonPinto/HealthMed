using HealthMed.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public class AgendaHorarioMedico : EntityBase
    {
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; }
        public int MedicoId { get; set; }
        public ProfissionalMedico Medico { get; set; }

        protected AgendaHorarioMedico()
        {
            
        }

        public AgendaHorarioMedico(DateTime dataHora, bool disponivel, int medicoId)
        {
            DataHora = dataHora;
            Disponivel = disponivel;
            MedicoId = medicoId;
        }
    }
}
