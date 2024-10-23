namespace BuildingBlocks.Exception;

public class UnexpectedException() : System.Exception("Internal Server Error"), IException
{
    public int GetStatusCode() => 500;
    public string GetTitle() => "Internal Server Error";
    public object GetResult() => "Internal Server Error";
}