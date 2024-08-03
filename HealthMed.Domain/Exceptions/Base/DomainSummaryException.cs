namespace HealthMed.Domain.Exceptions.Base;

public class DomainSummaryException : Exception
{
    public List<ItemInfoException> Exceptions { get; set; } = new List<ItemInfoException>();

    public DomainSummaryException() { }

    public DomainSummaryException(List<ItemInfoException> exceptions) => this.Exceptions.AddRange(exceptions);


    public void Add(ItemInfoException exceptionItemInfo)
    {
        //Throw.IfIsNull(exceptionItemInfo, nameof(exceptionItemInfo));
        this.Exceptions.Add(exceptionItemInfo);
    }

    public void Add(string model, string reference, string message)
    {
        var exceptionItemInfo = new ItemInfoException(model, reference, message);
        this.Add(exceptionItemInfo);
    }
}
