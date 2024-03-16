using System;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class FileMoveHandler : Handler
{
    private FileSystemMediator _fileSystem;

    public FileMoveHandler(FileSystemMediator fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new WrongArguments("file move: arguments expired!");
        arguments.Index++;
        if (arguments.Index >= arguments.Length) return new WrongArguments("file move: no source path given!");
        string sourcePath = arguments[arguments.Index];
        arguments.Index++;
        if (arguments.Index >= arguments.Length) return new WrongArguments("file move: no destination path given!");
        string destinationPath = arguments[arguments.Index];
        if (!_fileSystem.CheckFileForExisting(sourcePath))
            return new WrongArguments($"file move: {sourcePath} does not exist!");
        try
        {
            _fileSystem.MoveFile(sourcePath, destinationPath);
        }
        catch (UnauthorizedAccessException e)
        {
            return new WrongArguments(e.Message);
        }

        return new Normal();
    }
}