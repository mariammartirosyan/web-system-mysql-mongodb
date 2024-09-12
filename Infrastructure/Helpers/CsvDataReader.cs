﻿using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers
{
    public static class CsvDataReader<T,P> where P : ClassMap
    {
        public static IEnumerable<T> GetData(string path)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Comment = '%',
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<P>();
                return csv.GetRecords<T>().ToList<T>();
            }

        }
    }
}

