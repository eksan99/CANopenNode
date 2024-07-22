using System.Diagnostics;
using System.IO;
using Xunit;

namespace Tests
{
    public class CliTest : libEDSsharp.EDSsharp
    {
        string RunEDSSharp(string arguments)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "EDSSharp.exe";
            p.StartInfo.Arguments = arguments;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        [Fact]
        public void XddToCanOpenNodeLegacy()
        {
            RunEDSSharp("--type CanOpenNode --infile minimal_project.xdd --outfile Legacy");
            string[] files = Directory.GetFiles(".", "Legacy.*");
            Assert.Equal(2, files.Length);
        }
        [Fact]
        public void XddToCanOpenNodeV4()
        {
            RunEDSSharp("--type CanOpenNodeV4 --infile minimal_project.xdd --outfile V4");
            string[] files = Directory.GetFiles(".", "V4.*");
            Assert.Equal(2, files.Length);
        }
    }
}
