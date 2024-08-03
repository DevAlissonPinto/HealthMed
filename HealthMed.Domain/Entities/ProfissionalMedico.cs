using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public class ProfissionalMedico : Usuario
    {
        public string NumeroCRM { get; set; }
        public List<AgendaHorarioMedico> HorariosMedico { get; set; }

        public ProfissionalMedico()
        {

        }
        public ProfissionalMedico(string numeroCRM, List<AgendaHorarioMedico> horariosMedico)
        {
            NumeroCRM = numeroCRM;
            HorariosMedico = horariosMedico;
        }
    }
}
