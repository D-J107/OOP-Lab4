using System;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class FileRenameHandler : Handler
{
    private FileSystemMediator _fileSystem;
    public FileRenameHandler(FileSystemMediator fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new WrongArguments("file rename: arguments expired!");
        arguments.Index++;
        if (arguments.Index >= arguments.Length) return new WrongArguments("file move: no path given!");
        string pathToFile = arguments[arguments.Index];
        arguments.Index++;
        if (arguments.Index >= arguments.Length) return new WrongArguments("file move: no new name given!");
        string newFileName = arguments[arguments.Index];
        if (!_fileSystem.CheckFileForExisting(pathToFile))
            return new WrongArguments($"file move: {pathToFile} does not exist!");
        try
        {
            _fileSystem.RenameFile(pathToFile, newFileName);
        }
        catch (UnauthorizedAccessException e)
        {
            return new WrongArguments(e.Message);
        }

        return new Normal();
    }
}