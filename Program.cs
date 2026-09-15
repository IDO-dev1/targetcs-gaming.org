using System;
using System.Windows.Forms;
using TargetCS_Gaming.org.Helpers;

namespace TargetCS_Gaming.org;

internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        var silent = CommandLineParser.HasFlag(args, "--silent");
        var startup = CommandLineParser.HasFlag(args, "--startup");
        var exitAfterUpdate = CommandLineParser.HasFlag(args, "--exit-after-update");

        Application.Run(new MainForm(
            silentMode: silent,
            startupMode: startup,
            exitAfterUpdate: exitAfterUpdate));
    }
}
