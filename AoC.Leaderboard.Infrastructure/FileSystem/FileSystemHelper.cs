namespace AoC.Leaderboard.Infrastructure.FileSystem;

public static class FileSystemHelper
{
    public static string GetDataDirectory()
    {
        var userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        if (string.IsNullOrEmpty(userHome))
        {
            throw new InvalidOperationException("Unable to determine user home directory.");
        }

        var dir = Path.Combine(userHome, ".aoclb");
        Directory.CreateDirectory(dir);

        return dir;
    }
}