using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Test_IHSMarkit.arf_.arf_ex;

namespace Test_IHSMarkit.arf_
{
    public class arf_SearchEngine
    {
        public arf_SearchEngine()
        { }
        
        public List<String> GetAllWays(String path)
        {
            List<String> listPathsDownFiles = new List<String>();

            void _read(String newPath)
            {
                String[] s_Directories;
                String[] s_Files;

                try
                {
                    s_Directories = Directory.GetDirectories(newPath);
                    s_Files = Directory.GetFiles(newPath);
                }
                catch(UnauthorizedAccessException exception)
                {
                    throw new arf_Exception("\t-> The caller does not have the required permission. <-");
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
                

                if (s_Directories.Length == 0 && s_Files.Length == 0)
                    return;

                foreach (var path_file_txt in s_Files)
                    listPathsDownFiles.Add(path_file_txt);

                foreach (var nextPuth in s_Directories)
                    _read(nextPuth);

                return;
            }

            _read(path);

            return listPathsDownFiles;    
        }
    }
}
