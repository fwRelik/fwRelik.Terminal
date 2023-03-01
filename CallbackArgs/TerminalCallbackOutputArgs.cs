namespace fwRelik.Terminal.CallbackArgs
{
    public class TerminalCallbackOutputArgs : EventArgs
    {
        /// <summary>
        /// Will contain output data.
        /// </summary>
        public string StdOut;
        /// <summary>
        /// Will contain Exception if an error occurred.
        /// </summary>
        public Exception? Error;

        /// <summary>
        /// Wrapper for passed values.
        /// </summary>
        /// <param name="stdout">Will contain output data.</param>
        public TerminalCallbackOutputArgs(string stdout) { StdOut = stdout; }

        /// <summary>
        /// Wrapper for passed values.
        /// </summary>
        /// <param name="stdout">Will contain output data.</param>
        /// <param name="error">Will contain Exception if an error occurred.</param>
        public TerminalCallbackOutputArgs(string stdout, Exception error)
        {
            StdOut = stdout;
            Error = error;
        }
    }
}
