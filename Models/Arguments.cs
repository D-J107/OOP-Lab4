namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class Arguments
{
    private string[] _arguments;

    public Arguments(string[] arguments)
    {
        _arguments = arguments;
        if (arguments != null) Length = arguments.Length;
        Index = 0;
    }

    public int Index { get; set; }
    public int Length { get; }

    public string this[int key]
    {
        get => _arguments[key];
        set => _arguments[key] = value;
    }
}