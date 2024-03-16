namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.FileSystems;

public interface IFileSystem
{
    public string GetCurrentPathLocation();
    public bool AreAddressIsExistenceDirectory(string address);
    public bool CheckFileForExistence(string fileName);
    public void ChangeEnvironmentLocation(string pathToNewEnvironment);
    public void MoveFile(string sourcePath, string destinationPath);
    public void CopyFile(string sourcePath, string destinationPath);
    public void DeleteFile(string fileName);
    public void RenameFile(string pathToFile, string newFileName);
}