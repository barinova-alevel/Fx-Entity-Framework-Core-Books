using System;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using Serilog;

namespace Books.DataAccessLayer
{
    public class CustomDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (DateTime.TryParseExact(text, new[] { "MM/dd/yyyy", "yyyy-MM-dd","dd-MM-yyyy", "M/d/yyyy" },
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            Log.Information($"Invalid date format for value '{text}'");
            return null;
        }

    }
}
