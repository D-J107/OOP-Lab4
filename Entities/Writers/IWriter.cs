namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Writers;

public interface IWriter
{
    public void WriteMessage(string message);
    public void WriteMessageLine(string message);
    public void WriteErrorMessage(string message);
}