using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Test_IHSMarkit.arf_.arf_ex;
using Test_IHSMarkit.arf_.arf_parallel;
    
namespace Test_IHSMarkit.arf_
{
    public class arf_FileUploader
    {
        public arf_FileUploader()
        { }

        public List<String> Download(String pathFileWithURI, Encoding encoding, Int32 countThread = 1)
        {   
            List<String> listPathsURI = new List<String>();
            using (StreamReader _reader = new StreamReader(pathFileWithURI, encoding))
            {
                String uri;
                while ((uri = _reader.ReadLine()) != null)                
                    listPathsURI.Add(uri);        
            }

            String appRoot = AppDomain.CurrentDomain.BaseDirectory;            
            List<String> listPathsDownFiles = new List<String>();

            if (countThread == 1)
            {
                WebClient webClient = new WebClient();
                foreach (String uri in listPathsURI)
                {
                    var buff = uri.Split('\\');
                    var nameFile = "arf_new" + buff[buff.Length - 1];
                    try
                    {
                        webClient.DownloadFile(uri, nameFile);
                    }
                    catch (WebException webException)
                    {
                        throw new arf_Exception($"\t-> An error occurred while downloading data. <-\n");
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    listPathsDownFiles.Add(appRoot + nameFile);
                }
            }
            else
            {
                void dwn(String uri)
                {
                    WebClient webClient = new WebClient();
                    var buff = uri.Split('\\');
                    var nameFile = "arf_new" + buff[buff.Length - 1];
                    try
                    {
                        webClient.DownloadFile(uri, nameFile);
                    }
                    catch (WebException webException)
                    {
                        throw new arf_Exception($"\t-> An error occurred while downloading data. <-\n");
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    lock (l_k)
                        listPathsDownFiles.Add(appRoot + nameFile);
                }

                arf_Distributor<String> distributor = new arf_Distributor<String>(dwn, listPathsURI);
                distributor.Start(countThread, true);
            }
            return listPathsDownFiles;
        }
        private Object l_k = new Object();
    }
}
