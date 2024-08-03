using HealthMed.Application;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Services;
using HealthMed.Infra.Repository.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Tests.Domain.Application;

public class ProfissionalMedicoApplicationTest
{
    private readonly Mock<IUnitOfWork<HealthMedContext>> _unitOfWorkMock;
    private readonly Mock<IProfissionalMedicoService<HealthMedContext>> _profissionalMedicoServiceMock;
    private readonly ProfissionalMedicoApplication<HealthMedContext> _profissionalMedicoApplication;

    public ProfissionalMedicoApplicationTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork<HealthMedContext>>();
        _profissionalMedicoServiceMock = new Mock<IProfissionalMedicoService<HealthMedContext>>();
        _profissionalMedicoApplication = new ProfissionalMedicoApplication<HealthMedContext>(_unitOfWorkMock.Object, _profissionalMedicoServiceMock.Object);
    }

    
}
