using Itmo.ObjectOrientedProgramming.Lab4.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args == null) return;
        var manager = new ApplicationManager();
        manager.StartApplication(args);
    }
}