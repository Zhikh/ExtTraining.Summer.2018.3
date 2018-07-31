using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    public static class StreamWorker
    {
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

        private static void InputValidation(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }
        }
    }
}
