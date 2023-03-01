using fwRelik.Terminal.CallbackArgs;

namespace fwRelik.Terminal.Extensions
{
    /// <summary>
    /// Output processing.
    /// </summary>
    public class TerminalHandler
    {
        /// <summary>
        /// Callback type for output processing.
        /// </summary>
        /// <param name="args">
        /// Returns the <see cref="TerminalCallbackOutputArgs"/> instance as parameters.
        /// </param>
        public delegate void GetOutputCallback(TerminalCallbackOutputArgs args);

        /// <summary>
        /// Callback type for processing output with return values.
        /// </summary>
        /// <typeparam name="T">Value returned from callback.</typeparam>
        /// <param name="args">
        /// Returns the <see cref="TerminalCallbackOutputArgs"/> instance as parameters.
        /// </param>
        /// <returns>Returned from Callback: <typeparamref name="T"/></returns>
        public delegate T GetOutputCallbackOther<T>(TerminalCallbackOutputArgs args);

        private readonly StreamReader _stdOutput;
        private readonly StreamReader _stdError;
        private readonly StreamWriter _stdInput;

        /// <summary>
        /// Creates a <see cref="TerminalHandler"/> instance.
        /// </summary>
        /// <returns><see cref="TerminalHandler"/> instance.</returns>
        internal TerminalHandler(StreamReader stdOutput, StreamWriter stdInput, StreamReader stdError)
        {
            _stdOutput = stdOutput;
            _stdError = stdError;
            _stdInput = stdInput;
        }

        /// <summary>
        /// Provides the ability to process output.
        /// </summary>
        /// <param name="callback">Returns the <see cref="TerminalCallbackOutputArgs"/> instance as parameters.</param>
        /// <returns><see cref="TerminalHandler"/>.</returns>
        public TerminalHandler GetOutput(GetOutputCallback callback)
        {
            var stdout = _stdOutput.ReadToEnd();
            var error = _stdError.ReadToEnd();
            var args = error != string.Empty
                ? new TerminalCallbackOutputArgs(stdout, new ArgumentException(error))
                : new TerminalCallbackOutputArgs(stdout);

            callback(args);

            return this;
        }

        /// <summary>
        /// Provides the ability to process output.
        /// </summary>
        /// <param name="callback">Returns the <see cref="TerminalCallbackOutputArgs"/> instance as parameters.</param>
        /// <returns>Returns the result ot the callback.</returns>
        public T GetOutput<T>(GetOutputCallbackOther<T> callback)
        {
            var stdout = _stdOutput.ReadToEnd();
            var error = _stdError.ReadToEnd();
            var args = error != string.Empty ? new TerminalCallbackOutputArgs(stdout, new ArgumentException(error)) : new TerminalCallbackOutputArgs(stdout);

            return callback(args);
        }
    }
}
