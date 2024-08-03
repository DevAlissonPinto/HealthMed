namespace HealthMed.Domain.Interfaces;

public interface IPagedList<T>
{
    int Total { get; }
    IEnumerable<T> Data { get; }
}
