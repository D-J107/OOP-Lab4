using System;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandHandlers;

public class TreeListHandler : Handler
{
    private ConsoleColor _colorForDirectories;
    private ConsoleColor _colorForFiles;
    private FileSystemMediator _fileSystem;
    private OutputMediator _outputer;

    public TreeListHandler(FileSystemMediator fileSystemMediator, OutputMediator outputMediator)
    {
        _colorForDirectories = ConsoleColor.Blue;
        _colorForFiles = ConsoleColor.White;
        _fileSystem = fileSystemMediator;
        _outputer = outputMediator;
    }

    public override ProgramStatus Handle(Arguments arguments)
    {
        int depth = 1;
        if (arguments != null && arguments.Index + 1 < arguments.Length && arguments[arguments.Index + 1] == "-d")
        {
            arguments.Index++;
            if (arguments.Index + 1 == arguments.Length)
            {
                return new WrongArguments("tree list: Missed {depth} argument!");
            }

            arguments.Index++;
            if (!int.TryParse(arguments[arguments.Index], out depth))
            {
                return new WrongArguments($"tree list: argument {arguments[arguments.Index]} is not valid!");
            }

            if (depth <= 0)
            {
                return new WrongArguments("tree list: Depth value must be positive!");
            }
        }

        if (string.IsNullOrEmpty(_fileSystem.GetCurrentPathLocation()))
        {
            return new WrongArguments("tree list: Unknown location to print a tree!");
        }

        ShowFileListInTree(_fileSystem.GetCurrentPathLocation(), depth);
        return new Normal();
    }

    private void ShowFileListInTree(string directory, int depth, string prefix = "")
    {
        if (depth <= 0) return;
        var information = new DirectoryInfo(directory);
        if (!information.Exists) return;
        FileSystemInfo[] allInfos = information.GetFileSystemInfos();
        if (allInfos.Length == 0 && string.IsNullOrEmpty(prefix))
        {
            _outputer.OutputMessageWithLine("0 files, 0 directories");
            return;
        }

        foreach (FileSystemInfo info in allInfos)
        {
            _outputer.OutputMessage(prefix + "├── ");
            WriteName(info);
            if (Directory.Exists(info.FullName))
            {
                ShowFileListInTree(info.FullName, depth - 1, prefix + "│   ");
            }
        }
    }

    private void WriteName(FileSystemInfo infoAboutFile)
    {
        if (Directory.Exists(infoAboutFile.FullName))
        {
            Console.ForegroundColor = _colorForDirectories;
            _outputer.OutputMessageWithLine(infoAboutFile.Name);
            Console.ForegroundColor = _colorForFiles;
            return;
        }

        _outputer.OutputMessageWithLine(infoAboutFile.Name);
    }
}