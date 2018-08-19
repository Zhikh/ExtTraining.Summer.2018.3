using System;
using System.Collections.Generic;
using System.IO;
using No7.Solution.Interface;

namespace No7.Solution.Concrete
{
    public sealed class DataProvider : IDataProvider<string>
    {
        #region Fields
        private readonly Stream _stream;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="DataProvider"/>
        /// </summary>
        /// <param name="stream"></param>
        public DataProvider(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>
        /// Gets whole data from file
        /// </summary>
        /// <returns> Collection of elements of string type </returns>
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
        #endregion
    }
}
