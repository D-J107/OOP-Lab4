namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class WrongArguments : ProgramStatus
{
    public WrongArguments(string message)
    {
        ErrorMessage = message;
    }

    public string ErrorMessage { get; }
}