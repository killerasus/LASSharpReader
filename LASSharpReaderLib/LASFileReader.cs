using System;
using System.Collections.Generic;
using System.Linq;

namespace LASSharpReader
{
    /// <summary>
    /// Implements a LAS 2.0 file reader
    /// </summary>
    public class LASFileReader
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
            _nullValue = -999.25;
            _stepValue = 0.0;
            _wrap = false;
            _filePath = "";
            _versionInformation.Clear();
            _wellInformation.Clear();
            _curveInformation.Clear();
            _parameterInformation.Clear();
            _otherInformation = "";
            _asciiValues.Clear();
            _foundVersionSection = false;
            _foundWellSection = false;
            _foundCurveSection = false;
            _foundParametersSection = false;
            _foundOtherSection = false;
            _foundAsciiSection = false;
        }
        
        /// <summary>
        /// Reads and validates a LAS 2.0 file
        /// </summary>
        /// <param name="LASFilePath">Path to the LAS file</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if LAS file was correctly read</returns>
        public bool ReadFile( string LASFilePath, ref string errorMessage  )
        {
            Reset();

            bool valid = true;
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
                    if (_state != LASReadingState.End && !processLine(line, ref lineProcessingErrorMessage))
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
            if (IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                if (validateVersionSection(ref errorMessage))
                {
                    return processSectionInitialLetter(line[1], ref errorMessage);
                }
                else
                {
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
                if (field.Mnemonic.Length == 0)
                {
                    errorMessage = "LAS line mnemonic is empty.";
                    return false;
                }
                else
                {
                    _versionInformation.Add(field);
                }
                return true;
            }
        }

        /// <summary>
        /// Validates Version Section
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if version section was valid</returns>
        private bool validateVersionSection(ref string errorMessage)
        {
            bool hasVers = false;
            bool hasWrap = false;

            foreach (LASField field in _versionInformation)
            {
                if (field.Mnemonic.Equals("VERS"))
                {
                    if (!field.Data.Equals("2.0"))
                    {
                        errorMessage = "LAS version is not 2.0. It is " + field.Data + ".";
                        return false;
                    }
                    else
                    {
                        hasVers = true;
                    }
                }
                else if (field.Mnemonic.Equals("WRAP"))
                {
                    hasWrap = true;

                    if (field.Data.Equals("YES"))
                    {
                        _wrap = true;
                    }
                    else if (field.Data.Equals("NO"))
                    {
                        _wrap = false;
                    }
                    else
                    {
                        errorMessage = "WRAP value not YES or NO.";
                        return false;
                    }
                }
            }
            
            if (!hasVers)
            {
                errorMessage = "VERS mnemonic not found in Version Section.";
                return false;
            }

            if (!hasWrap)
            {
                errorMessage = "WRAP mnemonic not found in Version Section.";
                return false;
            }

            return true;
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
            if (IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                if (validateWellSection(ref errorMessage))
                {
                    return processSectionInitialLetter(line[1], ref errorMessage);
                }
                else
                {
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
                if (field.Mnemonic.Length == 0)
                {
                    errorMessage = "LAS line mnemonic is empty.";
                    return false;
                }
                else
                {
                    _wellInformation.Add(field);
                }
                return true;
            }
        }

        /// <summary>
        /// Validates Well Section
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if valid</returns>
        private bool validateWellSection(ref string errorMessage)
        {
            bool hasStart = false;
            bool hasStop = false;
            bool hasStep = false;
            bool hasNull = false;
            bool hasComp = false;
            bool hasWell = false;
            bool hasField = false;
            bool hasLocation = false;
            bool hasProvince = false;
            bool hasCounty = false;
            bool hasState = false;
            bool hasCountry = false;
            bool hasService = false;
            bool hasDate = false;
            bool hasUWI = false;
            bool hasAPI = false;

            string startUnit = "";
            string stopUnit = "";
            string stepUnit = "";

            // Validates STRT, STOP and STEP fields
            foreach( LASField field in _wellInformation )
            {
                if (field.Mnemonic.Equals("STRT"))
                {
                    hasStart = true;
                    if (!IsNullOrWhiteSpace(field.Unit))
                    {
                        startUnit = field.Unit;
                        try
                        {
                            System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
                            _startValue = double.Parse(field.Data, format);
                        }
                        catch (ArgumentNullException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                        catch (FormatException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                        catch (OverflowException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                    }
                    else
                    {
                        errorMessage = "STRT has no unit.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("STOP"))
                {
                    hasStop = true;
                    if (!IsNullOrWhiteSpace(field.Unit))
                    {
                        stopUnit = field.Unit;
                        try
                        {
                            System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
                            _stopValue = double.Parse(field.Data, format);
                        }
                        catch (ArgumentNullException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                        catch (FormatException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                        catch (OverflowException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                    }
                    else
                    {
                        errorMessage = "STOP has no unit.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("STEP"))
                {
                    hasStep = true;
                    if (!IsNullOrWhiteSpace(field.Unit))
                    {
                        stepUnit = field.Unit;
                        try
                        {
                            System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
                            _stepValue = double.Parse(field.Data, format);
                        }
                        catch(ArgumentNullException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                        catch(FormatException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                        catch(OverflowException exception)
                        {
                            errorMessage = exception.Message;
                            return false;
                        }
                    }
                    else
                    {
                        errorMessage = "STEP has no unit.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("NULL"))
                {
                    hasNull = true;
                    try
                    {
                        System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
                        _nullValue = double.Parse(field.Data, format);
                    }
                    catch (ArgumentNullException exception)
                    {
                        errorMessage = exception.Message;
                        return false;
                    }
                    catch (FormatException exception)
                    {
                        errorMessage = exception.Message;
                        return false;
                    }
                    catch (OverflowException exception)
                    {
                        errorMessage = exception.Message;
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("COMP"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasComp = true;
                    }
                    else
                    {
                        errorMessage = "COMP data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("WELL"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasWell = true;
                    }
                    else
                    {
                        errorMessage = "WELL data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("FLD"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasField = true;
                    }
                    else
                    {
                        errorMessage = "FLD data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("LOC"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasLocation = true;
                    }
                    else
                    {
                        errorMessage = "LOC data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("PROV"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasProvince = true;
                    }
                    else
                    {
                        errorMessage = "PROV data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("CNTY"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasCounty = true;
                    }
                    else
                    {
                        errorMessage = "CNTY data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("STAT"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasState = true;
                    }
                    else
                    {
                        errorMessage = "STAT data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("CTRY"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasCountry = true;
                    }
                    else
                    {
                        errorMessage = "CTRY data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("SRVC"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasService = true;
                    }
                    else
                    {
                        errorMessage = "SRVC data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("DATE"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasDate = true;
                    }
                    else
                    {
                        errorMessage = "DATE data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("UWI"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasUWI = true;
                    }
                    else
                    {
                        errorMessage = "UWI data is empty.";
                        return false;
                    }
                }
                else if (field.Mnemonic.Equals("API"))
                {
                    if (!IsNullOrWhiteSpace(field.Data))
                    {
                        hasAPI = true;
                    }
                    else
                    {
                        errorMessage = "API data is empty.";
                        return false;
                    }
                }
            }

            // Validates STRT, STOP, STEP and NULL existence
            if (!hasStart)
            {
                errorMessage = "STRT not defined in Well Section.";
                return false;
            }

            if (!hasStop)
            {
                errorMessage = "STOP not defined in Well Section.";
                return false;
            }

            if (!hasStep)
            {
                errorMessage = "STEP not defined in Well Section.";
                return false;
            }

            if (!hasNull)
            {
                errorMessage = "NULL not defined in Well Section";
                return false;
            }

            // Validates STRT, STOP and STEP units
            if (!startUnit.Equals(stopUnit) || !stopUnit.Equals(stepUnit))
            {
                errorMessage = "STRT, STOP and STEP must have the same unit.";
                return false;
            }
            else
            {
                _domainUnit = startUnit;
            }

            // Validates STRT, STOP and STEP values
            if (_stepValue > 0 && _startValue > _stopValue)
            {
                errorMessage = "STRT is greater than STOP but STEP is positive.";
                return false;
            }
            else if (_stepValue < 0 && _startValue < _stopValue)
            {
                errorMessage = "STRT is lower than STOP but STEP is negative.";
                return false;
            }

            // Validates other mnemonics

            if (!hasComp)
            {
                errorMessage = "COMP not defined in Well Section.";
                return false;
            }

            if (!hasWell)
            {
                errorMessage = "WELL not defined in Well Section.";
                return false;
            }

            if (!hasField)
            {
                errorMessage = "FLD not defined in Well Section.";
                return false;
            }

            if (!hasLocation)
            {
                errorMessage = "LOC not defined in Well Section.";
                return false;
            }

            if (!hasProvince && !hasState && !hasCounty && !hasCountry)
            {
                errorMessage = "PROV, STAT, CNTY or CTRY not defined in Well Section.";
                return false;
            }

            if (!hasService)
            {
                errorMessage = "SRVC not defined in Well Section.";
                return false;
            }

            if (!hasDate)
            {
                errorMessage = "DATE not defined in Well Section.";
                return false;
            }

            if (!hasUWI && !hasAPI)
            {
                errorMessage = "UWI or API not defined in Well Section.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Process a LAS line while in the context of Curve Section
        /// </summary>
        /// <param name="line">Line to be processed</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if the processing was OK</returns>
        private bool processLineCurveSectionState(string line, ref string errorMessage)
        {
            if (IsNullOrWhiteSpace(line))
            {
                errorMessage = "Empty line.";
                return false;
            }
            else if (line[0] == '~')
            {
                if (validateCurveSection(ref errorMessage))
                {
                    return processSectionInitialLetter(line[1], ref errorMessage);
                }
                else
                {
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
        /// Validates Curve Section
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>True if Curve Section is valid</returns>
        private bool validateCurveSection(ref string errorMessage)
        {
            if (_curveInformation.Count == 0)
            {
                errorMessage = "Curve Section must not be empty.";
                return false;
            }

            LASField field = _curveInformation[0];

            // Validates domain curve
            if (!field.Mnemonic.Equals("DEPT") && !field.Mnemonic.Equals("DEPTH") && 
                !field.Mnemonic.Equals("TIME"))
            {
                errorMessage = "First curve must be DEPT, DEPTH or TIME.";
                return false;
            }

            // Validates domain unit
            switch(field.Mnemonic)
            {
                case "DEPT":
                case "DEPTH":
                    switch (field.Unit)
                    {
                        case "M":
                        case "F":
                        case "FT":
                            break;
                        default:
                            errorMessage = "DEPT or DEPTH only accepts M, F or FT as unit.";
                            return false;
                    }
                    break;
                default: //TIME
                    // Checks if unit is empty or has : / \ - (meaning it is in a non-floating point convertible format)
                    if (IsNullOrWhiteSpace(field.Unit) || !field.Unit.FirstOrDefault(isInvalidDateChar).Equals('\0'))
                    {
                        errorMessage = "TIME unit must be convertible to a floating point representation of time.";
                        return false;
                    }
                    break;
            }

            // Validates domain curve unit compared to START, STOP and STEP unit
            if (!field.Unit.Equals(_domainUnit))
            {
                errorMessage = string.Format("Unit {0} is different from START, STOP and STEP unit {1}", field.Unit, _domainUnit);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tests if a character is in invalid time unit format expected
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool isInvalidDateChar(char arg)
        {
            switch(arg)
            {
                case '\\':
                case '/':
                case ':':
                case '-':
                    return true;
                default:
                    return false;
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
            if (IsNullOrWhiteSpace(line))
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
            if (IsNullOrWhiteSpace(line))
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
            if (line.Count() > 0)
            {
                if (line[0] == '~')
                {
                    errorMessage = "ASCII section should be the last section";
                    return false;
                }

                char[] separators = { ' ', '\t' };
                int count = _curveInformation.Count;
                string[] splitLine = line.Split(separators);
                int readParameters = splitLine.Count<string>();

                if (!_wrap)
                {
                    if (readParameters == count)
                    {
                        return processAsciiData(ref errorMessage, splitLine);
                    }
                    else
                    {
                        errorMessage = string.Format("Wrong number of parameters. Expected {0}, read {1}.", count, readParameters);
                        return false;
                    }
                }
                else
                {
                    //if (_domainReading)
                    //{
                    //    if (readParameters == 1)
                    //    {
                    //        _domainReading = !_domainReading;
                            return processAsciiData(ref errorMessage, splitLine);
                    //    }
                    //    else
                    //    {
                    //        errorMessage = "Wrap in use, but domain line has more than one value.";
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    int expectedDataParameters = Math.Min(count - 1, 1);

                    //    if (readParameters == expectedDataParameters)
                    //    {
                    //        _domainReading = !_domainReading;
                    //        return processAsciiData(ref errorMessage, splitLine);
                    //    }
                    //    else
                    //    {
                    //        errorMessage = string.Format("Wrap in use, but data line has less ({0}) values than expected ({1}).", readParameters, expectedDataParameters);
                    //        return false;
                    //    }
                    //}
                }
            }
            else
            {
                _state = LASReadingState.End;
                return true;
            }
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
        /// Process a variable set of string data parameters
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <param name="data">Variadic string parameters</param>
        /// <returns>True if data processing was OK</returns>
        private bool processAsciiData(ref string errorMessage, params string[] data)
        {
            try
            {
                System.Globalization.NumberFormatInfo format = new System.Globalization.NumberFormatInfo();
                double epsilon = _stepValue * 0.001;

                for (int i = 0; i < data.Length; ++i)
                {
                    double parsed = double.Parse(data[i], format);

                    // Checks step in case of domain column
                    if (i == 0 && !(_stepValue - 0.0 <= double.Epsilon))
                    {
                        // If it isn't the first line read
                        if (_asciiValues.Count > 0)
                        {
                            int index = _asciiValues.Count - _curveInformation.Count;
                            double difference = parsed - _asciiValues[index];

                            if (!(difference - _stepValue <= epsilon))
                            {
                                errorMessage = string.Format("Value has a step different than {0}.", _stepValue);
                                return false;
                            }
                        }
                    }

                    _asciiValues.Add(parsed);
                }

                return true;
            }
            catch (ArgumentNullException exception)
            {
                errorMessage = exception.Message;
            }
            catch (FormatException exception)
            {
                errorMessage = exception.Message;
            }
            catch (OverflowException exception)
            {
                errorMessage = exception.Message;
            }

            return false;
        }

        /// <summary>
        /// Checks if a string is null or whitespaced
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>True if string is null or just whitespace characters</returns>
        private bool IsNullOrWhiteSpace(String value)
        {
            if (value == null)
            {
                return true;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!Char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
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
        /// Version information section
        /// </summary>
        public List<LASField> VersionInformation
        {
            get
            {
                return _versionInformation;
            }
        }

        /// <summary>
        /// Well information section
        /// </summary>
        public List<LASField> WellInformation
        {
            get
            {
                return _wellInformation;
            }
        }

        /// <summary>
        /// Curve information section
        /// </summary>
        public List<LASField> CurveInformation
        {
            get
            {
                return _curveInformation;
            }
        }

        /// <summary>
        /// Parameters information section
        /// </summary>
        public List<LASField> ParametersInformation
        {
            get
            {
                return _parameterInformation;
            }
        }

        /// <summary>
        /// Other information section
        /// </summary>
        public string OtherInformation
        {
            get
            {
                return _otherInformation;
            }
        }

        /// <summary>
        /// ASCII data section
        /// </summary>
        public List<double> ASCIIData
        {
            get
            {
                return _asciiValues;
            }
        }

        /// <summary>
        /// Information line separators in a LAS File
        /// </summary>
        /// <see cref="Modified from http://stackoverflow.com/questions/25521539/regex-for-las-entries"/>
        private static string _pattern = @"^([\w\s]*)\s*\.([^\s]*)\s*([^:]*)\s*:(.*)$";

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
        /// Well start value
        /// </summary>
        private double _startValue;

        /// <summary>
        /// Well stop value
        /// </summary>
        private double _stopValue;

        /// <summary>
        /// Well step value
        /// </summary>
        private double _stepValue;

        /// <summary>
        /// Domain column unit, as defined in START, STOP, STEP and domain curve
        /// </summary>
        private string _domainUnit;

        /// <summary>
        /// If wrapping is used in ascii section
        /// </summary>
        private bool _wrap;

        /// <summary>
        /// If a version section was found
        /// </summary>
        private bool _foundVersionSection;

        /// <summary>
        /// If a well section was found
        /// </summary>
        private bool _foundWellSection;

        /// <summary>
        /// If a parameters section was found
        /// </summary>
        private bool _foundParametersSection;

        /// <summary>
        /// If a curve section was found
        /// </summary>
        private bool _foundCurveSection;

        /// <summary>
        /// If an other section was found
        /// </summary>
        private bool _foundOtherSection;

        /// <summary>
        /// If an ascii section was found
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
