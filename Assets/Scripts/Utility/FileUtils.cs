using System.IO;
using SimpleFileBrowser;
using UnityEngine;

namespace Utility
{
    public static class FileUtils
    {
        public static void WriteObjectAsJsonToFile(object o, string pathToFile)
        {
            var filename = Path.GetFileName(pathToFile);
            var directoryPath = Path.GetDirectoryName(pathToFile);
            var json = JsonUtility.ToJson(o, true);
            var pathToJson = FileBrowserHelpers.CreateFileInDirectory(directoryPath, filename);
            FileBrowserHelpers.WriteTextToFile(pathToJson, json);
        }
    }
}