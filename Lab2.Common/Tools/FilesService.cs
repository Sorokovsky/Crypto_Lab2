namespace Lab2.Common.Tools;

public class FilesService
{
    private readonly string _folder;

    public FilesService(string folder = "files")
    {
        _folder = folder;
        if (Directory.Exists(_folder) is false) Directory.CreateDirectory(_folder);
    }

    public bool Exists(string path)
    {
        return File.Exists(GetPath(path));
    }

    public string Read(string path)
    {
        if (Exists(path) is false) throw new ArgumentException("Файлу не знайдено.");
        using var stream = File.OpenRead(GetPath(path));
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private void Write(string path, string text, bool rewrite = false)
    {
        var fullPath = GetPath(path);
        if (Exists(path) is false) throw new ArgumentException("Файлу не знайдено.");
        var mode = rewrite ? FileMode.Truncate : FileMode.Append;
        using var stream = File.Open(fullPath, mode);
        using var writer = new StreamWriter(stream);
        writer.Write(text);
    }

    public void Rewrite(string path, string text)
    {
        Write(path, text, true);
    }

    public void Create(string path, string text)
    {
        Write(path, text);
    }

    private string GetPath(string filePath)
    {
        return Path.Combine(_folder, filePath);
    }
}