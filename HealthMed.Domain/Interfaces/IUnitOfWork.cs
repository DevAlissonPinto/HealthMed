namespace HealthMed.Domain.Interfaces;

public interface IUnitOfWork<TContext>
{
    int Commit();
}
