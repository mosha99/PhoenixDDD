namespace BuildingBlocks.Exception;

public class LogicException(string message = "Operation Is Invalid") : System.Exception(message), IException
{
    public virtual int GetStatusCode() => 400;

    public virtual string GetTitle() => "Logic Exception";

    public object GetResult() => base.Message;
}