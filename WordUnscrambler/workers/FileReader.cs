using System;
using System.IO;

namespace WordUnscrambler.workers
{
    class FileReader
    {
        class FileReader1
        {
            public string[] Read(string filename)
            {
                string[] fileContent;
                try
                {
                    fileContent = File.ReadAllLines(filename);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
                return fileContent;

            }
        }

        internal string[] Read(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
