namespace BuildingBlocks.Exception;
public interface IException
{
    public int GetStatusCode();
    public string GetTitle();
    public object GetResult();
}