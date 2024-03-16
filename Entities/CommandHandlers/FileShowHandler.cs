using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class FileShowHandler : Handler
{
    private OutputMediator _output;

    public FileShowHandler(OutputMediator output)
    {
        _output = output;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new WrongArguments("file show: arguments expired!");
        arguments.Index++;
        if (arguments.Index >= arguments.Length) return new WrongArguments("file show: path argument missed!");
        string pathToFile = arguments[arguments.Index];
        if (!File.Exists(pathToFile)) return new WrongArguments($"file show: file {pathToFile} does not exist!");
        string mode = "console";
        if (arguments.Index < arguments.Length)
        {
            arguments.Index++;
            string flag = arguments[arguments.Index];
            if (flag != "-m") return new WrongArguments($"file show: unknown flag : {flag}!");
            arguments.Index++;
            mode = arguments[arguments.Index];
            if (!_output.IsWriterTypeExist(mode))
                return new WrongArguments($"file show: unknown file output mode : {mode}!");
        }

        string textToOutput = File.ReadAllText(pathToFile);
        _output.SetWriter(mode);
        _output.OutputMessage(textToOutput);
        return new Normal();
    }
}