namespace HealthMed.Domain.Exceptions.Base;

public class ItemInfoException
{
    public string Model { get; protected set; }
    public string Reference { get; protected set; }
    public string Message { get; protected set; }
    public object[] Arguments { get; protected set; }


    public ItemInfoException(string model, string reference, string message, params object[] arguments)
    {
        //Throw.IfIsNullOrWhiteSpace(model);
        //Throw.IfIsNullOrWhiteSpace(reference);
        //Throw.IfIsNullOrWhiteSpace(message);

        this.Model = model;
        this.Reference = reference;
        this.Message = message;
        this.Arguments = arguments;
    }
}
