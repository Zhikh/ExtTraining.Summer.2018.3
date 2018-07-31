using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    public static class StreamWorker
    {
        #region public methods
        public static List<string> ReadAll(string path)
        {
            InputValidation(path);

            var lines = new List<string>();

            using (var reader = new StreamReader(path)) 
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        public static List<string> ReadAll(Stream stream)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(stream))
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

        #region Additional methods
        private static void InputValidation(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }
        }
        #endregion
    }
}
