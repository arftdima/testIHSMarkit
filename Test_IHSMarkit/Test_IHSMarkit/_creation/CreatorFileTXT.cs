using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_IHSMarkit.Interface;
using System.IO;
using Test_IHSMarkit._validation;
using System.Net;
using Test_IHSMarkit.arf_;

namespace Test_IHSMarkit._creation
{
    public class CreatorFileTXT : ICreating
    {
        public CreatorFileTXT()
        { }

        public void Create()
        { }

        public void Create(string[] args, string outpath)
        {
            String mode = args[0];
            String path = args[1];

            arf_Dictionary dictionary = new arf_Dictionary();
            List<String> listPathsFiles = null;

            if (mode == Resource.modeFileSystem)
            {
                arf_SearchEngine searchEngine = new arf_SearchEngine();

                listPathsFiles = searchEngine.GetAllWays(path);

                dictionary.AddDate(listPathsFiles, false, Encoding.Default, Int32.Parse(Resource.numberThreads));
                dictionary.WriteToFile(outpath, false, Encoding.Default);
            }
            else //mode == Resource.modeHTTP
            {
                arf_FileUploader fileUploader = new arf_FileUploader();

                listPathsFiles = fileUploader.Download(path, Encoding.Default, Int32.Parse(Resource.numberThreads));

                dictionary.AddDate(listPathsFiles, true, Encoding.Default, Int32.Parse(Resource.numberThreads));
                dictionary.WriteToFile(outpath, false, Encoding.Default);
            }
        }
    }
}
