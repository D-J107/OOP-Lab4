using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class TreeGotoHandler : Handler
{
    private FileSystemMediator _systemMediator;

    public TreeGotoHandler(FileSystemMediator fileSystemMediator)
    {
        _systemMediator = fileSystemMediator;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new WrongArguments("tree goto: arguments expired!");
        arguments.Index++;
        if (arguments.Index >= arguments.Length)
        {
            return new WrongArguments("tree goto: Missed [Path] argument!");
        }

        string gotoPosition = arguments[arguments.Index];
        if (gotoPosition == "..")
        {
            _systemMediator.GoToParentFromCurrentLocation();
            arguments.Index++;
            return new Normal();
        }

        var info = new FileInfo(gotoPosition);

        if (info.Directory == null)
        {
            return new WrongArguments($"tree goto: {gotoPosition}: No such file or directory!");
        }

        _systemMediator.ChangeEnvironmentLocation(gotoPosition);
        arguments.Index++;
        return new Normal();
    }
}