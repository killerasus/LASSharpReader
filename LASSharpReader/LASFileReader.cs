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
            _state = LASReadingState.Initial;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LASFilePath"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool ReadFile( string LASFilePath, ref string errorMessage  )
        {
            bool valid = false;
            _filePath = LASFilePath;
            
            try
            {
                System.IO.StreamReader lasFile;
                lasFile = System.IO.File.OpenText(_filePath);

                int lines = 0;
                string line = "";
                string lineProcessingErrorMessage = "";
                
                while ((line = lasFile.ReadLine()) != null)
                {
                    lines++;
                    if (!processLine(line, ref lineProcessingErrorMessage))
                    {
                        errorMessage = string.Format("Line[{0}]: {1}", line, lineProcessingErrorMessage);
                        valid = false;
                        break;
                    }
                }

                if (lines == 0)
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
        /// Process a line of the LAS file
        /// </summary>
        /// <param name="line">The line to be processed</param>
        /// <param name="errorMessage">Error message is returned as reference</param>
        /// <returns>True if the processing was ok, false in case of error</returns>
        private bool processLine( string line, ref string errorMessage )
        {
            switch (_state)
            {
                case LASReadingState.Initial:
                    return processLineInitialState(line, ref errorMessage);
                case LASReadingState.VersionSection:
                    return processLineVersionSectionState(line, ref errorMessage);
                case LASReadingState.WellSection:
                    return processLineWellSectionState(line, ref errorMessage);
                case LASReadingState.CurveSection:
                    return processLineCurveSectionState(line, ref errorMessage);
                case LASReadingState.ParametersSection:
                    return processLineParameterSectionState(line, ref errorMessage);
                case LASReadingState.OtherSection:
                    return processLineOtherSectionState(line, ref errorMessage);
                case LASReadingState.ASCIISection:
                    return processLineASCIISectionState(line, ref errorMessage);
                default:
                    break;
            }

            errorMessage = "Unidentified error.";
            return false;
        }
        
        /// <summary>
        /// Process line when in Initial State
        /// </summary>
        /// <param name="line">Line from LAS file</param>
        /// <param name="errorMessage">Error message returned as reference</param>
        /// <returns>True if the processing was ok, false in case of error</returns>
        private bool processLineInitialState(string line, ref string errorMessage)
        {
            if (line.Length == 0 || line[0] == '#')
            {
                return true;
            }
            else if (line[0] == '~')
            {
                if (line[1] == 'V')
                {
                    _state = LASReadingState.VersionSection;
                    return true;
                }
                else
                {
                    errorMessage = "First section line is not a Version Information Section.";
                    return false;
                }
            }
            else
            {
                errorMessage = "Unidentified error.";
                return false;
            }
        }

        /// <summary>
        /// Process line when in Version Section state
        /// </summary>
        /// <param name="line">Line from LAS file</param>
        /// <param name="errorMessage">Error message returned as reference</param>
        /// <returns>True if the processing was ok, false in case of error</returns>
        private bool processLineVersionSectionState(string line, ref string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                if (line[1] == 'W')
                {
                    _state = LASReadingState.WellSection;
                    return true;
                }
                else
                {
                    errorMessage = "First section line is not a Version Information Section.";
                    return false;
                }
            }
            else if (line[0] == '#') // Comment line
            {
                return true;
            }
            else
            {
                LASField field = processLineInformationSection(line);
                return true;
            }
        }

        /// <summary>
        /// Builds a LASField structure from a LAS information section line
        /// </summary>
        /// <param name="line">An information section line</param>
        /// <returns>The LASField represented by the line</returns>
        private LASField processLineInformationSection(string line)
        {
            /// http://stackoverflow.com/questions/25521539/regex-for-las-entries
            /// https://regex101.com/r/nK5qM4/1
            string pattern = @"^([\w\s]*)\s*\.([^ ]*)\s*([^:]*)\s*:(.*)$";
            string[] brokenLine = System.Text.RegularExpressions.Regex.Split(line, pattern);

#if DEBUG
            foreach( string group in brokenLine )
            {
                System.Console.Out.WriteLine(group);
            }
#endif

            return new LASField(brokenLine[0], brokenLine[1], brokenLine[2], brokenLine[3]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool processLineWellSectionState(string line, ref string errorMessage)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool processLineCurveSectionState(string line, ref string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool processLineParameterSectionState(string line, ref string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool processLineOtherSectionState(string line, ref string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool processLineASCIISectionState(string line, ref string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Represents the current state in the LASReading State Machine
        /// </summary>
        private enum LASReadingState
        {
            Initial = 0,
            VersionSection,
            WellSection,
            CurveSection,
            ParametersSection,
            OtherSection,
            ASCIISection,
            End,
            Error
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
        /// Information line separators in a LAS File
        /// </summary>
        private static char[] separators = { '.', ' ', ':' };

        /// <summary>
        /// State of the reading operation
        /// </summary>
        private LASReadingState _state;

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
        private string _otherInformation;

        /// <summary>
        /// ASCII section values
        /// </summary>
        private double[] _asciiValues;
    }
}

