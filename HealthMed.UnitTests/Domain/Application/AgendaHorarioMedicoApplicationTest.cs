using FluentAssertions;
using HealthMed.Application;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Services;
using HealthMed.Infra.Repository.Context;
using Moq;

namespace HealthMed.Tests.Domain.Application;

public class AgendaHorarioMedicoApplicationTest
{
    private readonly Mock<IUnitOfWork<HealthMedContext>> _unitOfWorkMock;
    private readonly Mock<IAgendaHorarioMedicoService<HealthMedContext>> _agendaHorarioMedicoServiceMock;
    private readonly IAgendaHorarioMedicoApplication<HealthMedContext> _agendaHorarioMedicoApplication;

    public AgendaHorarioMedicoApplicationTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork<HealthMedContext>>();
        _agendaHorarioMedicoServiceMock = new Mock<IAgendaHorarioMedicoService<HealthMedContext>>();
        _agendaHorarioMedicoApplication = new AgendaHorarioMedicoApplication<HealthMedContext>(_unitOfWorkMock.Object, _agendaHorarioMedicoServiceMock.Object);
    }

    [Fact(DisplayName = "SaveAsync salvar uma agenda e realizar commit")]
    public async Task SaveAsync_DeveSalvarAgendaHorarioERealizarCommit()
    {
        // Arrange
        var agendaHorario = new AgendaHorarioMedico(DateTime.Now, true, 1) { Id = 1 };
        _agendaHorarioMedicoServiceMock.Setup(s => s.SaveAsync(agendaHorario)).ReturnsAsync(agendaHorario);

        // Act
        var result = await _agendaHorarioMedicoApplication.SaveAsync(agendaHorario);

        // Assert
        _agendaHorarioMedicoServiceMock.Verify(s => s.SaveAsync(agendaHorario), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        result.Should().Be(agendaHorario);
    }

    [Fact(DisplayName = "UpdateAsync salvar uma agenda e realizar commit")]
    public async Task UpdateAsync_DeveAtualizarAgendaHorarioERealizarCommit()
    {
        // Arrange
        var agendaHorario = new AgendaHorarioMedico(DateTime.Now, true, 1) { Id = 1 };
        _agendaHorarioMedicoServiceMock.Setup(s => s.UpdateAsync(agendaHorario)).ReturnsAsync(agendaHorario);

        // Act
        var result = await _agendaHorarioMedicoApplication.UpdateAsync(agendaHorario);

        // Assert
        _agendaHorarioMedicoServiceMock.Verify(s => s.UpdateAsync(agendaHorario), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        result.Should().Be(agendaHorario);
    }

}
