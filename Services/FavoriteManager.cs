using System.Text;
using TargetCS_Gaming.org.Models;

namespace TargetCS_Gaming.org.Services;

public class FavoriteManager
{
    public string Backup(string file)
    {
        if (!File.Exists(file))
            throw new FileNotFoundException("ServerBrowser.vdf was not found.", file);

        var backup = file + ".backup";

        if (File.Exists(backup))
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            backup = $"{file}.backup_{timestamp}";
        }

        File.Copy(file, backup, false);
        return backup;
    }

    public int AddServers(
        string file,
        IEnumerable<ServerEntry> servers)
    {
        if (!File.Exists(file))
            throw new FileNotFoundException("ServerBrowser.vdf was not found.", file);

        var originalBytes = File.ReadAllBytes(file);
        var encoding = DetectEncoding(originalBytes);
        var text = encoding.GetString(originalBytes);

        var newline = text.Contains("\r\n") ? "\r\n" : "\n";
        var added = 0;
        var changed = false;

        foreach (var server in servers)
        {
            if (string.IsNullOrWhiteSpace(server.Address))
                continue;

            if (ContainsServer(text, server.Address))
                continue;

            text = AddServer(text, server, newline);
            added++;
            changed = true;
        }

        if (changed)
        {
            File.WriteAllBytes(file, encoding.GetBytes(text));
        }

        return added;
    }

    private bool ContainsServer(string text, string address)
    {
        return text.Contains(address, StringComparison.OrdinalIgnoreCase);
    }

    private string AddServer(
        string text,
        ServerEntry server,
        string newline)
    {
        var escapedName = Escape(server.Name);
        var escapedAddress = Escape(server.Address);

        var entry =
            $"{newline}\"favorite\"{newline}" +
            $"{{{newline}" +
            $"    \"name\"      \"{escapedName}\"{newline}" +
            $"    \"address\"   \"{escapedAddress}\"{newline}" +
            $"    \"lastplayed\" \"0\"{newline}" +
            $"}}{newline}";

        var closingIndex = text.LastIndexOf('}');
        if (closingIndex < 0)
            throw new InvalidDataException("Invalid ServerBrowser.vdf structure.");

        var before = text[..closingIndex];
        var after = text[closingIndex..];

        return before + entry + after;
    }

    private static string Escape(string value)
    {
        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"");
    }

    private static Encoding DetectEncoding(byte[] bytes)
    {
        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
            return new UTF8Encoding(true);

        if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE)
            return Encoding.Unicode;

        if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF)
            return Encoding.BigEndianUnicode;

        return new UTF8Encoding(false);
    }
}
