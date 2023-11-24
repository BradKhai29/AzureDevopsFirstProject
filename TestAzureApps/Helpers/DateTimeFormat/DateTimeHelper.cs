using SampleUnitTestProject_NetCore.Helpers.DateTimeFormat;
using System.Globalization;

namespace DemoSeleniumAutomationTesting.Helpers.DateTimeFormat
{
    public sealed class DateTimeHelper
    {
        #region Properties
        public BuiltInFormat OutputFormat { get; } = BuiltInFormat.Default;
        private static readonly DateTimeHelper DefaultInstance = new DateTimeHelper(BuiltInFormat.Default);
        public static DateTimeHelper Instance => DefaultInstance;
        #endregion


        public DateTimeHelper(BuiltInFormat format)
        {
            if (format is null)
            {
                throw new ArgumentNullException("Please input valid BuiltInFormat");
            }

            OutputFormat = format;
        }

        /// <summary>
        ///     Return a string that represent for the inputDateTime based on the Default BuiltIn Format.
        /// </summary>
        /// <remarks>
        ///     Throw <see cref="ArgumentNullException"/> if OutputFormat is set <b>null</b>
        /// </remarks>
        /// <param name="inputDateTime"></param>
        /// <exception cref="ArgumentNullException">
        ///     When <paramref name="instance" /> is null.
        /// </exception>
        /// <returns>
        ///     String represent for the inputDateTime after formatted.
        /// </returns>
        public string ToStringDefault(DateTime? inputDateTime)
        {
            if (inputDateTime.HasValue)
            {
                return ToStringWithFormat(inputDateTime, BuiltInFormat.Default);
            }

            throw new ArgumentException("Input date time cannot be null");
        }


        public string ToStringWithFormat(DateTime? inputDateTime, BuiltInFormat format)
        {
            if (inputDateTime.HasValue)
            {
                return inputDateTime?.ToString(format.Format);
            }
            throw new ArgumentException("Input date time cannot be null");
        }


        public DateTime ToDateTimeDefault(string dateTimeString)
        {
            return DateTime.ParseExact(
                        dateTimeString,
                        format: OutputFormat.Format,
                        provider: CultureInfo.InvariantCulture,
                        style: DateTimeStyles.None);
        }


        public DateTime ToDateTimeWithFormat(string dateTimeString, BuiltInFormat format)
        {
            return DateTime.ParseExact(
                        dateTimeString,
                        format: format.Format,
                        provider: CultureInfo.InvariantCulture,
                        style: DateTimeStyles.None);
        }


        /// <summary>
        ///     Compare 2 input <paramref name="dateTime1"/>, <paramref name="dateTime2"/> 
        ///     and return the bool result corresponding to the input <paramref name="dateTimeCompareType"/>.
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <param name="dateTimeCompareType"></param>
        /// <param name="datePartOnly"></param>
        /// <returns>
        ///     boolean: true / false corresponding to the input <paramref name="dateTimeCompareType"/>.
        /// </returns>
        public static bool Compare(DateTime dateTime1, DateTime dateTime2, DateTimeCompareType dateTimeCompareType, bool datePartOnly = true)
        {
            dateTime1 = datePartOnly ? dateTime1.Date : dateTime1;
            dateTime2 = datePartOnly ? dateTime2.Date : dateTime2;

            switch (dateTimeCompareType)
            {
                case DateTimeCompareType.Equal:
                    return dateTime1.CompareTo(dateTime2) == 0;

                case DateTimeCompareType.EarlierThan:
                    return dateTime1.CompareTo(dateTime2) < 0;

                case DateTimeCompareType.LaterThan:
                    return dateTime1.CompareTo(dateTime2) > 0;

                default: return false;
            };
        }
    }
}