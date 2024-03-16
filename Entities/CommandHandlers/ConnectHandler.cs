using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class ConnectHandler : Handler
{
    private FileSystemMediator _systemMediator;
    public ConnectHandler(FileSystemMediator fileSystemMediator)
    {
        _systemMediator = fileSystemMediator;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new WrongArguments("connect: arguments expired!");
        arguments.Index++;
        string fileAddress = arguments[arguments.Index];
        if (!_systemMediator.CheckFileForBeingADirectory(fileAddress))
        {
            return new WrongArguments($"connect: {fileAddress} No such directory!");
        }

        _systemMediator.ChangeEnvironmentLocation(fileAddress);

        arguments.Index++;
        if (arguments.Index >= arguments.Length)
        {
            return new WrongArguments("connect: file system mode flag expected!");
        }

        string flag = arguments[arguments.Index];
        if (flag != "-m")
        {
            return new WrongArguments($"connect: -m flag expected, but {flag} was given!");
        }

        arguments.Index++;
        string fileSystemName = arguments[arguments.Index];
        if (!_systemMediator.CheckFileSystemForExistingInKnownBase(fileSystemName))
        {
            return new WrongArguments($"connect: unknown file system mode: {fileSystemName}");
        }

        _systemMediator.ChangeFileSystemByName(fileSystemName);
        return new Normal();
    }
}