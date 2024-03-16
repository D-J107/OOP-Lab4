using System;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class FileDeleteHandler : Handler
{
    private FileSystemMediator _fileSystem;

    public FileDeleteHandler(FileSystemMediator fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new WrongArguments("file delete: arguments expired");
        arguments.Index++;
        if (arguments.Index >= arguments.Length) return new WrongArguments("file delete: no path to deleting given!");
        string pathToDelete = arguments[arguments.Index];
        try
        {
            _fileSystem.DeleteFile(pathToDelete);
        }
        catch (UnauthorizedAccessException e)
        {
            return new WrongArguments(e.Message);
        }

        return new Normal();
    }
}