using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Writers;

public class ConsoleWriter : IWriter
{
    public void WriteMessage(string message)
    {
        Console.Write(message);
    }

    public void WriteMessageLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteErrorMessage(string message)
    {
        Console.WriteLine("Error: " + message);
    }
}