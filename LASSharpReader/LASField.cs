using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASSharpReader
{
    /// <summary>
    /// Represents a LAS line field
    /// </summary>
    public struct LASField
    {
        
        private string _mnemonic;
        private string _value;
        private string _comment;

        /// <summary>
        /// Mnemonic column
        /// </summary>
        public string Mnemonic
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        /// <summary>
        /// Value column
        /// </summary>
        public string Value
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        /// <summary>
        /// Comment column
        /// </summary>
        public string Comment
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}