using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Mediators;

public class FileSystemMediator
{
    private Dictionary<string, IFileSystem> _knownFileSystems;
    private string _currentFileSystem;

    public FileSystemMediator(string standardFileSystemName = "", IFileSystem? standardFileSystem = null)
    {
        _knownFileSystems = new Dictionary<string, IFileSystem>()
        {
            { "local", new Local() },
        };
        _currentFileSystem = "local";

        if (string.IsNullOrEmpty(standardFileSystemName) || standardFileSystem == null) return;
        _currentFileSystem = standardFileSystemName;
        _knownFileSystems.Add(standardFileSystemName, standardFileSystem);
    }

    public string GetCurrentPathLocation()
    {
        return _knownFileSystems[_currentFileSystem].GetCurrentPathLocation();
    }

    public bool CheckFileForBeingADirectory(string pathToFile)
    {
        return _knownFileSystems[_currentFileSystem].AreAddressIsExistenceDirectory(pathToFile);
    }

    public bool CheckFileSystemForExistingInKnownBase(string fileSystemName)
    {
        return _knownFileSystems.Any(pair => pair.Key == fileSystemName);
    }

    public bool CheckFileForExisting(string pathToFile)
    {
        return _knownFileSystems[_currentFileSystem].CheckFileForExistence(pathToFile);
    }

    public void AddFileSystem(string fileSystemName, IFileSystem newSystem)
    {
        if (CheckFileSystemForExistingInKnownBase(fileSystemName)) return;
        _knownFileSystems.Add(fileSystemName, newSystem);
    }

    public void ChangeFileSystemByName(string fileSystemName)
    {
        _currentFileSystem = fileSystemName;
    }

    public void ChangeEnvironmentLocation(string path)
    {
        _knownFileSystems[_currentFileSystem].ChangeEnvironmentLocation(path);
    }

    public void MoveFile(string source, string destination)
    {
        try
        {
            _knownFileSystems[_currentFileSystem].MoveFile(source, destination);
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
    }

    public void CopyFile(string source, string destination)
    {
        _knownFileSystems[_currentFileSystem].CopyFile(source, destination);
    }

    public void DeleteFile(string path)
    {
        _knownFileSystems[_currentFileSystem].DeleteFile(path);
    }

    public void RenameFile(string pathToFile, string newFileName)
    {
        _knownFileSystems[_currentFileSystem].RenameFile(pathToFile, newFileName);
    }

    public void GoToParentFromCurrentLocation()
    {
        string currentPathLocation = _knownFileSystems[_currentFileSystem].GetCurrentPathLocation();
        string? parentLocation = Directory.GetParent(currentPathLocation)?.FullName;
        if (parentLocation == null) return;
        currentPathLocation = parentLocation;
        ChangeEnvironmentLocation(currentPathLocation);
    }

    public IFileSystem GetFileSystemByName(string fileSystemName)
    {
        foreach (KeyValuePair<string, IFileSystem> pair in _knownFileSystems.Where(pair => pair.Key == fileSystemName))
        {
            return pair.Value;
        }

        throw new ArgumentException("Unknown file system!");
    }
}