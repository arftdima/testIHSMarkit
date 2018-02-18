using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Test_IHSMarkit.arf_.arf_ex;
using Test_IHSMarkit.arf_.arf_parallel;
using System.Collections.Concurrent;
using System.Threading;

namespace Test_IHSMarkit.arf_
{
    public class arf_Dictionary
    {
        private Dictionary<String, Int32> dictionary { get; set; }
        private Object l_k = new Object();

        public arf_Dictionary()
        {
            this.dictionary = new Dictionary<String, Int32>();
        }        
        public arf_Dictionary(Dictionary<String, Int32> dictionary)
        {
            this.dictionary = dictionary;
        }

        public void Add(String key, Int32 value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] += value;
            }
        }

        public void AddDate(String path, Encoding encoding)
        {
            List<String> listFileContents = new List<String>();
            using (StreamReader s_reader = new StreamReader(path, encoding))
            {
                String dataLine;
                while ((dataLine = s_reader.ReadLine()) != null)
                    listFileContents.Add(dataLine);
            }
            
            foreach (String line in listFileContents)
            {
                String key;
                Int32 value;

                var buff = line.ToLower();
                var ar_str = buff.Split(',');

                if (ar_str.Length != 2 || !Int32.TryParse(ar_str[1], out value))
                {
                    throw new arf_Exception("\t-> wrong format <-");
                }
                else
                {
                    key = ar_str[0];
                }
                lock(l_k)
                    Add(key, value);
            }
        }

        public void AddDate(List<String> listPathsFiles, Boolean delete, Encoding encoding, Int32 countTask = 1)
        {
            Boolean parallel = countTask <= 1 ? false : true;
            if (!parallel)
            {
                foreach (String _file in listPathsFiles)
                {
                    AddDate(_file, encoding);
                    if (delete)
                        File.Delete(_file);
                }
            }
            else
            {
                arf_Distributor<String> distributor = new arf_Distributor<String>(AddDate, listPathsFiles, encoding);
                distributor.Start(countTask);
                if (delete)
                    foreach (String _file in listPathsFiles)
                        File.Delete(_file);
            }
        }

        public void WriteToFile(String outputh, Boolean append, Encoding encoding)
        {
            using (StreamWriter s_writer = new StreamWriter(outputh, append, encoding))
            {
                foreach (var _info in dictionary)
                {
                    s_writer.WriteLine($"{_info.Key},{_info.Value}");
                }
            }
        }
    }
}
