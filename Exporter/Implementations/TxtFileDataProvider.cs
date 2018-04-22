namespace Exporter.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Exporter.Interfaces;

    /// <summary>
    /// A class that provides data from a text file.
    /// </summary>
    public class TxtFileDataProvider : IDataProvider<string>
    {
        private string _filePath;

        /// <summary>
        /// The constructor for creating the entity.
        /// </summary>
        /// <param name="filePath">
        /// Original file.
        /// </param>
        public TxtFileDataProvider(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// The path to the text file.
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(FilePath)} is incorrect. The path must not be empty or null.", nameof(FilePath));
                }

                _filePath = value;
            }
        }

        /// <summary>
        /// Provision of data.
        /// </summary>
        /// <returns>
        /// Data set.
        /// </returns>
        public IEnumerable<string> GetData()
        {
            var data  = new List<string>();

            using (var stream = new StreamReader(FilePath, Encoding.UTF8))
            {
                while (!stream.EndOfStream)
                {
                    string s = stream.ReadLine();

                    data.Add(s);
                }
            }

            return data;
        }
    }
}
