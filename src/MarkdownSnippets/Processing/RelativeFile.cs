using System.IO;

static class RelativeFile
{
    public static bool Find(string rootDirectory, string key, string? relativePath, string? linePath, out string path)
    {
        if (!key.Contains("."))
        {
            path = null!;
            return false;
        }

        var relativeToRoot = Path.Combine(rootDirectory, key);
        if (File.Exists(relativeToRoot))
        {
            path= relativeToRoot;
            return true;
        }

        var documentDirectory = Path.GetDirectoryName(relativePath);
        if (documentDirectory != null)
        {
            var relativeToDocument = Path.Combine(rootDirectory, documentDirectory.Trim('/', '\\'), key);
            if (File.Exists(relativeToDocument))
            {
                path = relativeToDocument;
                return true;
            }
        }

        var lineDirectory = Path.GetDirectoryName(linePath);
        if (lineDirectory != null)
        {
            var relativeToLine = Path.Combine(lineDirectory, key);
            if (File.Exists(relativeToLine))
            {
                path =relativeToLine;
                return true;
            }
        }

        if (File.Exists(key))
        {
            path = key;
            return true;
        }

        path = null!;
        return false;
    }
}