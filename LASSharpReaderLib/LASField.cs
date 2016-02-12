namespace LASSharpReader
{
    /// <summary>
    /// Represents a LAS line field
    /// </summary>
    public struct LASField
    {
        
        private string _mnemonic;
        private string _unit;
        private string _data;
        private string _comment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mnemonic">Mnemonic string</param>
        /// <param name="unit">Unit string</param>
        /// <param name="data">Data string</param>
        /// <param name="comment">Comment string</param>
        public LASField(string mnemonic, string unit, string data, string comment) : this()
        {
            _mnemonic = mnemonic;
            _unit = unit;
            _data = data;
            _comment = comment;
        }

        /// <summary>
        /// Mnemonic column
        /// </summary>
        public string Mnemonic
        {
            get
            {
                return _mnemonic;
            }

            set
            {
                _mnemonic = value;
            }
        }

        /// <summary>
        /// Unit column
        /// </summary>
        public string Unit
        {
            get
            {
                return _unit;
            }

            set
            {
                _unit = value;
            }
        }

        /// <summary>
        /// Data column
        /// </summary>
        public string Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }

        /// <summary>
        /// Comment column
        /// </summary>
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
            }
        }
    }
}