namespace TargetCS_Gaming.org.Helpers;

public static class CommandLineParser
{
    public static bool HasFlag(string[] args, string flag)
    {
        return args.Any(a =>
            string.Equals(a, flag, StringComparison.OrdinalIgnoreCase));
    }

    public static string? GetValue(string[] args, string key)
    {
        for (var i = 0; i < args.Length - 1; i++)
        {
            if (string.Equals(args[i], key, StringComparison.OrdinalIgnoreCase))
                return args[i + 1];
        }

        return null;
    }
}
