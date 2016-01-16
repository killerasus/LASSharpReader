using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASSharpReader
{
    /// <summary>
    /// Implements a LAS 2.0 file reader
    /// </summary>
    class LASFileReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LASFileReader( )
        {

        }
        
        public bool ReadFile( string LASFilePath, ref string errorMessage  )
        {
            bool valid = false;
            _filePath = LASFilePath;
            
            try
            {
                System.IO.FileStream lasFile;
                lasFile = System.IO.File.OpenRead(_filePath);

                if (lasFile.Length > 0)
                {
                    ///TODO: Implement LAS file reading treatment
                    valid = true;
                }
                else
                {
                    errorMessage = "File size = 0.";
                    valid = false;
                }

                lasFile.Close();
            }
            catch(ArgumentNullException exception)
            {
                errorMessage = exception.Message;
                valid = false;
            }
            catch(System.IO.PathTooLongException exception)
            {
                errorMessage = exception.Message;
                valid = false;
            }
            catch(System.IO.DirectoryNotFoundException exception)
            {
                errorMessage = exception.Message;
                valid = false;
            }
            catch(UnauthorizedAccessException exception)
            {
                errorMessage = exception.Message;
                valid = false;
            }
            catch(System.IO.FileNotFoundException exception)
            {
                errorMessage = exception.Message;
                valid = false;
            }
            
            return valid;
        }

        /// <summary>
        /// Returns the filepath associated in readFile
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
            }
        }

        /// <summary>
        /// Path to the current open LAS file
        /// </summary>
        private string _filePath;

        /// <summary>
        /// Well null value
        /// </summary>
        private double _nullValue;

        /// <summary>
        /// Version information section
        /// </summary>
        private LASField[] _versionInformation;

        /// <summary>
        /// Curve information section
        /// </summary>
        private LASField[] _curveInformation;

        /// <summary>
        /// Well information section
        /// </summary>
        private LASField[] _wellInformation;

        /// <summary>
        /// Parameter information section
        /// </summary>
        private LASField[] _parameterInformation;

        /// <summary>
        /// Other information section
        /// </summary>
        private LASField[] _otherInformation;

        /// <summary>
        /// ASCII section values
        /// </summary>
        private double[] _asciiValues;
    }
}
