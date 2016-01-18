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
            _versionInformation = new List<LASField>();
            _wellInformation = new List<LASField>();
            _curveInformation = new List<LASField>();
            _parameterInformation = new List<LASField>();
            _asciiValues = new List<double>();
        }

        /// <summary>
        /// Resets the LASFileReader states
        /// </summary>
        private void Reset()
        {
            _state = LASReadingState.Initial;
            _nullValue = -9999.25;
            _filePath = "";
            _wellInformation.Clear();
            _curveInformation.Clear();
            _parameterInformation.Clear();
            _otherInformation = "";
            _asciiValues.Clear();
            _foundVersionSection = false;
            _foundWellSection = false;
            _foundCurveSection = false;
            _foundParametersSection = false;
            _foundAsciiSection = false;
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
                        errorMessage = string.Format("Line[{0}]: {1}", lines, lineProcessingErrorMessage);
#if DEBUG
                        errorMessage += string.Format("\nState = {0}", _state.ToString());
#endif
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
                    _foundVersionSection = true;
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
                return processSectionInitialLetter(line[1], ref errorMessage);
            }
            else if (line[0] == '#') // Comment line
            {
                return true;
            }
            else
            {
                LASField field = processLineInformationSection(line);
                if (field.Mnemonic.Length == 0)
                {
                    errorMessage = "LAS line mnemonic is empty.";
                    return false;
                }
                else
                {
                    if (field.Mnemonic.Equals("VERS"))
                    {
                        if (!field.Data.Equals("2.0"))
                        {
                            errorMessage = "LAS version is not 2.0. It is " + field.Data + ".";
                            return false;
                        }
                    }
                    //@TODO: Verify if it will be necessary to check for WRAP

                    _versionInformation.Add(field);
                }
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
            string[] splitLine = System.Text.RegularExpressions.Regex.Split(line, _pattern);

#if DEBUG
            foreach( string group in splitLine )
            {
                System.Console.Out.WriteLine(group);
            }
#endif

            return new LASField(splitLine[1].Trim(), splitLine[2].Trim(), splitLine[3].Trim(), splitLine[4].Trim());
        }

        /// <summary>
        /// Process a LAS line while in the context of Well Section
        /// </summary>
        /// <param name="line">Line to be processed</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if the processing was OK</returns>
        private bool processLineWellSectionState(string line, ref string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                return processSectionInitialLetter(line[1], ref errorMessage);
            }
            else if (line[0] == '#') // Comment line
            {
                return true;
            }
            else
            {
                LASField field = processLineInformationSection(line);
                if (field.Mnemonic.Length == 0)
                {
                    errorMessage = "LAS line mnemonic is empty.";
                    return false;
                }
                else
                {
                    if (field.Mnemonic.Equals("VERS"))
                    {
                        if (!field.Data.Equals("2.0"))
                        {
                            errorMessage = "LAS version is not 2.0.";
                            return false;
                        }
                    }

                    _versionInformation.Add(field);
                }
                return true;
            }
        }

        /// <summary>
        /// Process a LAS line while in the context of Curve Section
        /// </summary>
        /// <param name="line">Line to be processed</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if the processing was OK</returns>
        private bool processLineCurveSectionState(string line, ref string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                return processSectionInitialLetter(line[1], ref errorMessage);
            }
            else if (line[0] == '#') // Comment line
            {
                return true;
            }
            else
            {
                LASField field = processLineInformationSection(line);
                if (field.Mnemonic.Length == 0)
                {
                    errorMessage = "LAS line mnemonic is empty.";
                    return false;
                }
                else
                {
                    _curveInformation.Add(field);
                }
                return true;
            }
        }

        /// <summary>
        /// Process a LAS line while in the context of Parameters Section
        /// </summary>
        /// <param name="line">Line to be processed</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if processing was OK</returns>
        private bool processLineParameterSectionState(string line, ref string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                return processSectionInitialLetter(line[1], ref errorMessage);
            }
            else if (line[0] == '#') // Comment line
            {
                return true;
            }
            else
            {
                LASField field = processLineInformationSection(line);
                if (field.Mnemonic.Length == 0)
                {
                    errorMessage = "LAS line mnemonic is empty.";
                    return false;
                }
                else
                {
                    _parameterInformation.Add(field);
                }
                return true;
            }
        }

        /// <summary>
        /// Process a LAS line while in the context of Other Section
        /// </summary>
        /// <param name="line">Line to be processed</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if the processing was OK</returns>
        private bool processLineOtherSectionState(string line, ref string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                return processSectionInitialLetter(line[1], ref errorMessage);
            }
            else if (line[0] == '#') // Comment line
            {
                return true;
            }
            else
            {
                _otherInformation += line.Trim() + " ";
                return true;
            }
        }

        /// <summary>
        /// Process a LAS line while in the context of ASCII Section
        /// </summary>
        /// <param name="line"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool processLineASCIISectionState(string line, ref string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the initial letter of a section line is valid in the current state.
        /// </summary>
        /// <param name="initial">Initial section character. Valid chars are V, W, C, P, O, A</param>
        /// <param name="errorMessage"></param>
        /// <returns>True if the initial section letter was valid</returns>
        private bool processSectionInitialLetter(char initial, ref string errorMessage)
        {
            switch (initial)
            {
                case 'A':
                    {
                        if (checkAsciiReady())
                        {
                            _state = LASReadingState.ASCIISection;
                            _foundAsciiSection = true;
                            return true;
                        }
                        else
                        {
                            errorMessage = "ASCII section can't be defined at the current line. Check the LAS 2.0 specification.";
                            return false;
                        }
                    }
                case 'C':
                    if (!_foundCurveSection)
                    {
                        _state = LASReadingState.CurveSection;
                        _foundCurveSection = true;
                        return true;
                    }
                    else
                    {
                        errorMessage = "A curve section was already defined.";
                        return false;
                    }
                case 'O':
                    if (!_foundOtherSection)
                    {
                        _state = LASReadingState.OtherSection;
                        _foundOtherSection = true;
                        return true;
                    }
                    else
                    {
                        errorMessage = "An other section was already defined.";
                        return false;
                    }
                case 'P':
                    if (!_foundParametersSection)
                    {
                        _state = LASReadingState.ParametersSection;
                        _foundParametersSection = true;
                        return true;
                    }
                    else
                    {
                        errorMessage = "A parameter section was already defined.";
                        return false;
                    }
                case 'V':
                    {
                        if (!_foundVersionSection)
                        {
                            _state = LASReadingState.VersionSection;
                            _foundVersionSection = true;
                            return true;
                        }
                        else
                        {
                            errorMessage = "A version section was already defined.";
                            return false;
                        }
                    }
                case 'W':
                    {
                        if (!_foundWellSection)
                        {
                            _state = LASReadingState.WellSection;
                            _foundWellSection = true;
                            return true;
                        }
                        else
                        {
                            errorMessage = "An well section was already defined.";
                            return false;
                        }
                    }
                default:
                    errorMessage = "Line identifier unknown.";
                    return false;
            }
        }

        /// <summary>
        /// Checks if can read ASCII section. According to LAS 2.0 specification, it must be the last section.
        /// </summary>
        /// <returns>True if can already read the ASCII section</returns>
        private bool checkAsciiReady()
        {
            return (!_foundAsciiSection && _foundVersionSection && _foundWellSection && _foundCurveSection);
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
        /// <see cref="Modified from http://stackoverflow.com/questions/25521539/regex-for-las-entries"/>
        /// <see cref="https://regex101.com/r/nK5qM4/2"/>
        private static string _pattern = @"^([\w\s]*)\s*\.([a-zA-Z0-9_\/]*[^ ])\s*([^:]*)\s*:(.*)$";

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
        /// 
        /// </summary>
        private bool _foundVersionSection;

        /// <summary>
        /// 
        /// </summary>
        private bool _foundWellSection;

        /// <summary>
        /// 
        /// </summary>
        private bool _foundParametersSection;

        /// <summary>
        /// 
        /// </summary>
        private bool _foundCurveSection;

        /// <summary>
        /// 
        /// </summary>
        private bool _foundOtherSection;

        /// <summary>
        /// 
        /// </summary>
        private bool _foundAsciiSection;

        /// <summary>
        /// Version information section
        /// </summary>
        private List<LASField> _versionInformation;

        /// <summary>
        /// Curve information section
        /// </summary>
        private List<LASField> _curveInformation;

        /// <summary>
        /// Well information section
        /// </summary>
        private List<LASField> _wellInformation;

        /// <summary>
        /// Parameter information section
        /// </summary>
        private List<LASField> _parameterInformation;

        /// <summary>
        /// Other information section
        /// </summary>
        private string _otherInformation;

        /// <summary>
        /// ASCII section values
        /// </summary>
        private List<double> _asciiValues;
    }
}

