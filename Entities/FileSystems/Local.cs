using System;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;

public class Local : IFileSystem
{
    public string GetCurrentPathLocation()
    {
        return Environment.CurrentDirectory;
    }

    public bool AreAddressIsExistenceDirectory(string address)
    {
        return Directory.Exists(address);
    }

    public bool CheckFileForExistence(string fileName)
    {
        return File.Exists(fileName);
    }

    public void ChangeEnvironmentLocation(string pathToNewEnvironment)
    {
        Environment.CurrentDirectory = pathToNewEnvironment;
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        File.Move(sourcePath, destinationPath);
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        File.Copy(sourcePath, destinationPath);
    }

    public void DeleteFile(string fileName)
    {
        File.Delete(fileName);
    }

    public void RenameFile(string pathToFile, string newFileName)
    {
        string? directoryWhereFileExists = Directory.GetParent(pathToFile)?.FullName;
        if (directoryWhereFileExists == null) return;
        string fullNewFileName = directoryWhereFileExists + '/' + newFileName;
        FileStream disposeMe = File.Create(fullNewFileName);
        File.Copy(pathToFile, fullNewFileName);
        File.Delete(pathToFile);
        disposeMe.Dispose();
    }
}