using fwRelik.Terminal.CallbackArgs;
using fwRelik.Terminal.Constants;
using fwRelik.Terminal.Extensions;
using System.Diagnostics;

namespace fwRelik.Terminal
{
    /// <summary>
    /// Working with an internal process using the powershell.
    /// </summary>
    public static class TerminalClient
    {
        /// <summary>
        /// Callback type for output processing.
        /// </summary>
        /// <param name="args">
        /// Returns the <see cref="TerminalCallbackOutputArgs">TerminalCallbackOutputArgs</see> instance as parameters.
        /// </param>
        public delegate void GetOutputCallback(TerminalCallbackOutputArgs args);

        /// <summary>
        /// Callback type for processing output with return values.
        /// </summary>
        /// <typeparam name="T">Value returned from callback.</typeparam>
        /// <param name="args">
        /// Returns the <see cref="TerminalCallbackOutputArgs">TerminalCallbackOutputArgs</see> instance as parameters.
        /// </param>
        /// <returns><typeparamref name="T"/></returns>
        public delegate T GetOutputCallbackWithReturn<T>(TerminalCallbackOutputArgs args);
        private static readonly string s_executor = TerminalConstants._powerShell;

        /// <summary>
        /// Executes the given command.
        /// </summary>
        /// <param name="command">Command in string type.</param>
        /// <returns>Returns a <see cref="TerminalHandler"/> instance for further processing.</returns>
        public static TerminalHandler Command(string command)
        {
            return BaseCommand(command);
        }

        /// <summary>
        /// Method with the ability to pass a callback to process the output of the command.
        /// </summary>
        /// <param name="command">Command in string type.</param>
        /// <param name="getOutputCallback">Callback.</param>
        public static void Command(string command, GetOutputCallback getOutputCallback)
        {
            BaseCommand(command).GetOutput(
                TerminalCallbackOutputArgs => getOutputCallback(TerminalCallbackOutputArgs)
            );
        }

        /// <summary>
        /// Method with the ability to pass a callback to process the output of the command.
        /// </summary>
        /// <typeparam name="T">Value returned from callback.</typeparam>
        /// <param name="command">Command in string type.</param>
        /// <param name="getOutputCallbackWithReturn">Callback with return value.</param>
        /// <returns><typeparamref name="T"/></returns>
        public static T Command<T>(string command, GetOutputCallbackWithReturn<T> getOutputCallbackWithReturn)
        {
            return BaseCommand(command).GetOutput(
                TerminalCallbackOutputArgs => getOutputCallbackWithReturn(TerminalCallbackOutputArgs)
            );
        }

        /// <summary>
        /// Basic command executor and handler creation.
        /// </summary>
        /// <param name="command">Command in string type.</param>
        /// <returns><see cref="TerminalHandler"/></returns>
        /// <exception cref="ArgumentException"></exception>
        private static TerminalHandler BaseCommand(string command)
        {
            try
            {
                var process = SetSettings();
                process.StartInfo.Arguments = command;

                process.Start();

                var stdOutput = process.StandardOutput;
                var stdInput = process.StandardInput;
                var stdError = process.StandardError;

                return new TerminalHandler(stdOutput, stdInput, stdError);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Basic setup of a new process
        /// </summary>
        /// <returns>Customized <see cref="Process"/></returns>
        private static Process SetSettings()
        {
            var process = new Process();

            process.StartInfo.FileName = s_executor;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;

            return process;
        }
    }
}