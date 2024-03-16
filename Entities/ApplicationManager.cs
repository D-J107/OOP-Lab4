using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public class ApplicationManager
{
    private FileSystemMediator _fileSystemMediator;
    private OutputMediator _outputMediator;
    private Dictionary<string, Handler> _commandHandlers;
    public ApplicationManager()
    {
        _fileSystemMediator = new FileSystemMediator();
        _outputMediator = new OutputMediator();
        _commandHandlers = new Dictionary<string, Handler>
        {
            { "connect", new ConnectHandler(_fileSystemMediator) },
            { "tree goto", new TreeGotoHandler(_fileSystemMediator) },
            { "tree list", new TreeListHandler(_fileSystemMediator, _outputMediator) },
            { "file show", new FileShowHandler(_outputMediator) },
            { "file move", new FileMoveHandler(_fileSystemMediator) },
            { "file copy", new FileCopyHandler(_fileSystemMediator) },
            { "file delete", new FileDeleteHandler(_fileSystemMediator) },
            { "file rename", new FileRenameHandler(_fileSystemMediator) },
        };
    }

    public void StartApplication(string[] argumentsFromMain)
    {
        var parser = new ParserHandler(_commandHandlers);
        var mainArguments = new Arguments(argumentsFromMain);
        parser.Handle(mainArguments);
        while (true)
        {
            string? currentCommand = Console.ReadLine();
            if (string.IsNullOrEmpty(currentCommand)) continue;
            string[] arguments = currentCommand.Split(' ');
            var currentArguments = new Arguments(arguments);
            ProgramStatus programRunningStatus = parser.Handle(currentArguments);
            if (programRunningStatus is WrongArguments status)
            {
                _outputMediator.OutputErrorMessage(status.ErrorMessage);
            }

            if (programRunningStatus is Disconnect) break;
        }
    }
}