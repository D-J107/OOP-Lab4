using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;

public class OutputMediator
{
    private Dictionary<string, IWriter> _knownWriters;
    private string _currentWriter;

    public OutputMediator(string standardWriterName = "", IWriter? standardWriter = null)
    {
        _knownWriters = new Dictionary<string, IWriter>()
        {
            { "console", new ConsoleWriter() },
        };
        _currentWriter = "console";

        if (string.IsNullOrEmpty(standardWriterName) || standardWriter == null) return;
        _knownWriters.Add(standardWriterName, standardWriter);
        _currentWriter = standardWriterName;
    }

    public void OutputMessage(string message)
    {
        _knownWriters[_currentWriter].WriteMessage(message);
    }

    public void OutputMessageWithLine(string message)
    {
        _knownWriters[_currentWriter].WriteMessageLine(message);
    }

    public void OutputErrorMessage(string message)
    {
        _knownWriters[_currentWriter].WriteErrorMessage(message);
    }

    public void SetWriter(string writerType)
    {
        _currentWriter = writerType;
    }

    public void AddNewWriterType(string writerType, IWriter writer)
    {
        _knownWriters.Add(writerType, writer);
    }

    public bool IsWriterTypeExist(string writerType)
    {
        return _knownWriters.Any(pair => pair.Key == writerType);
    }
}