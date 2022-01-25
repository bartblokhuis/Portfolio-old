using System.Reflection;

namespace System.IO;

public static class PathExtensions
{
    public static string GetApplicationBasePath()
    {
        // to get the location the assembly is executing from
        string basePath = Assembly.GetExecutingAssembly().Location;

        //once you have the path you get the directory with:
        var directory = Path.GetDirectoryName(basePath);

        return directory;
    }
}

