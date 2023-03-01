namespace fwRelik.Terminal.Extensions
{
    public class TerminalParser
    {
        /// <summary>
        /// Used if an instance of the <see cref="TerminalParser">TerminalParser</see> class was created
        /// </summary>
        private readonly string _stdOutValue;

        /// <summary>
        /// Creating a <see cref="TerminalParser">TerminalParser </see> instance.
        /// </summary>
        public TerminalParser(string stdOut)
        {
            _stdOutValue = stdOut;
        }

        /// <summary>
        /// Checking value in output.
        /// If the <paramref name="stateValue"/> matches the found value, it will return true. Otherwise false.
        /// </summary>
        /// <param name="keyword">The name of the property whose value is being checked.</param>
        /// <param name="stateValue">State property.</param>
        /// <param name="stateRow">On which line after the property to look for the value. 
        /// Default value: 0</param>
        public bool CheckValue(string keyword, string stateValue, int stateRow = 0)
        {
            return BaseCheckValue(_stdOutValue, keyword, stateValue, stateRow);
        }

        /// <summary>
        /// Checking value in output.
        /// If the <paramref name="stateValue"/> matches the found value, it will return true. Otherwise false.
        /// </summary>
        /// <param name="stdOut">String type data in which will provide a search for values.</param>
        /// <param name="keyword">The name of the property whose value is being checked.</param>
        /// <param name="stateValue">State property.</param>
        /// <param name="stateRow">On which line after the property to look for the value. 
        /// Default value: 0</param>
        public static bool CheckValue(string stdOut, string keyword, string stateValue, int stateRow = 0)
        {
            return BaseCheckValue(stdOut, keyword, stateValue, stateRow);
        }

        /// <summary>
        /// Parsing the output to create a line-by-line data format.
        /// </summary>
        /// <param name="stdOut">The output to be parsing</param>
        /// <param name="padding">Indent from the top. 
        /// Values that fall within this range will be excluded from the list. 
        /// Default value: 0</param>
        /// <returns>Line-separated list</returns>
        public static string[] ParseToNumarationRow(string stdOut, int padding = 0)
        {
            List<string> indexedOutput = stdOut.Split(Environment.NewLine).Where(row => row != string.Empty).ToList();

            if (padding > 0)
                for (int i = 0; i < padding; i++)
                    indexedOutput.RemoveAt(0);

            return indexedOutput.ToArray();
        }

        private static bool BaseCheckValue(string stdOut, string keyword, string stateValue, int stateRow = 0)
        {
            var indexedOutput = stdOut.Split(Environment.NewLine);

            for (int i = 0; i < indexedOutput.Length; i++)
                if (indexedOutput[i].Contains(keyword) && indexedOutput[i + stateRow].Contains(stateValue))
                    return true;

            return false;
        }
    }
}