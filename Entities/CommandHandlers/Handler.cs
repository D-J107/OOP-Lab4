using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public abstract class Handler
{
    public abstract ProgramStatus Handle(Arguments arguments);
}