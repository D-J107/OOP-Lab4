using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class ParserHandler : Handler
{
    private Dictionary<string, Handler> _commandHandlers;
    public ParserHandler(Dictionary<string, Handler> knownHandlersCommands)
    {
        _commandHandlers = knownHandlersCommands;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        if (arguments == null) return new Normal();
        for (; arguments.Index < arguments.Length; arguments.Index++)
        {
            switch (arguments[arguments.Index])
            {
                case "tree":
                    arguments.Index++;
                    return arguments[arguments.Index] switch
                    {
                        "goto" => _commandHandlers["tree goto"].Handle(arguments),
                        "list" => _commandHandlers["tree list"].Handle(arguments),
                        _ => new WrongArguments($"Unknown tree command: {arguments[arguments.Index]}!"),
                    };
                case "connect":
                    return _commandHandlers["connect"].Handle(arguments);
                case "disconnect":
                    return new Disconnect();
                case "file":
                    arguments.Index++;
                    return arguments[arguments.Index] switch
                    {
                        "show" => _commandHandlers["file show"].Handle(arguments),
                        "move" => _commandHandlers["file move"].Handle(arguments),
                        "copy" => _commandHandlers["file copy"].Handle(arguments),
                        "delete" => _commandHandlers["file delete"].Handle(arguments),
                        "rename" => _commandHandlers["file rename"].Handle(arguments),
                        _ => new WrongArguments($"Unknown file command: {arguments[arguments.Index]}"),
                    };
            }
        }

        return new Normal();
    }
}