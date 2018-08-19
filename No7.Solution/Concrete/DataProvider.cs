using No7.Solution.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace No7.Solution.Concrete
{
    public sealed class DataProvider : IDataProvider<string>
    {
        private readonly Stream _stream;

        public DataProvider(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public IEnumerable<string> GetAll()
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(_stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
