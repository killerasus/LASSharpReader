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
        private string _unit;
        private string _data;
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
        /// Unit column
        /// </summary>
        public string Unit
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
        /// Data column
        /// </summary>
        public string Data
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