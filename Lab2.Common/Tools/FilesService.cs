namespace Lab2.Common.Tools;

public class FilesService
{
    private readonly string _folder;

    public FilesService(string folder)
    {
        _folder = folder;
    }

    public bool Exists(string path)
    {
        return File.Exists(GetPath(path));
    }

    public string Read(string path)
    {
        if (Exists(path) is false) throw new ArgumentException("Файлу не знайдено.", nameof(path));
        using var stream = File.OpenRead(GetPath(path));
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public void Write(string path, string text, bool rewrite = false)
    {
        var fullPath = GetPath(path);
        if (Exists(fullPath) is false) throw new ArgumentException("Файлу не знайдено.", nameof(path));
        var mode = rewrite ? FileMode.Truncate : FileMode.Append;
        using var stream = File.Open(fullPath, mode);
        using var writer = new StreamWriter(stream);
        writer.Write(text);
    }

    public void Create(string path, string text)
    {
        var fullPath = GetPath(path);
        if (Exists(fullPath)) throw new ArgumentException("Файл вже існує.", nameof(path));
        using var stream = File.Open(fullPath, FileMode.Create);
        using var writer = new StreamWriter(stream);
        writer.Write(text);
    }

    private string GetPath(string filePath)
    {
        return Path.Combine(_folder, filePath);
    }
}