namespace BuildingBlocks.Exception;

public class LogicException(string message = "Operation Is Invalid") : System.Exception(message), IException
{
    public int GetStatusCode() => 400;

    public string GetTitle() => "Logic Exception";

    public object GetResult() => base.Message;
}