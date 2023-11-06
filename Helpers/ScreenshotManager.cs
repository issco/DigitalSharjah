using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;

namespace MRHE.Helpers
{
    public class ScreenshotManager
    {
        private static int _sequenceCounter = 0;
        static public void TakeScreenshot(IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.Info)
        {
            var screenshotTaker = (ITakesScreenshot)driver;
            var snapshotFile = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                string.Join('-', _sequenceCounter.ToString(), screenshotType.ToString(), Guid.NewGuid().ToString(), ".png"));

            screenshotTaker.GetScreenshot().SaveAsFile(snapshotFile, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(snapshotFile);

            _sequenceCounter++;
        }

        private static IntPtr GetWindowHandleBasedOnTitle(string title)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle.Contains(title))
                {
                    return proc.MainWindowHandle;
                }
            }
            return IntPtr.Zero;
        }
    }

    public enum ScreenshotType
    {
        Info,
        Warning,
        Error,
        Critical
    }
}
