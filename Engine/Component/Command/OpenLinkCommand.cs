using System.Diagnostics;
using System.Runtime.InteropServices;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component.Command
{
    /// <summary>
    /// Open a custom link to a website.
    /// </summary>
    class OpenLinkCommand : ICommand
    {
        private string URL { get; set; }
        public OpenLinkCommand(string link)
        {
            URL = link;
        }
        public void Execute()
        {
            try
            {
                Process.Start(URL);
            }
            catch
            {
                //hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    URL = URL.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(URL) { UseShellExecute = true });
                }
                else
                {
                    throw new Exception("Failed to open link.");
                }
            }
        }
    }
}