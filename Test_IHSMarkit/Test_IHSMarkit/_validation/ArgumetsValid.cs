using System;
using System.IO;
using Test_IHSMarkit.Interface;
using Test_IHSMarkit.arf_.arf_ex;
namespace Test_IHSMarkit._validation
{
    public class ArgumetsValid : IValidation
    {
        public ArgumetsValid()
        { }

        public void isValid()
        { }

        public void isValid(string[] args)
        {
            if (args.Length != 2)
                throw new arf_Exception("\t-> Invalid number of arguments. <-");

            String mode = args[0];
            String path = args[1];

            if (mode == Resource.modeHTTP)
            {
                if (!File.Exists(path))
                    throw new arf_Exception("\t-> File not found. <-");
            }
            else if (mode == Resource.modeFileSystem)
            {
                if (!Directory.Exists(path))
                    throw new arf_Exception("\t-> Directory not found. <-");
            }
            else
                throw new arf_Exception("\t-> Invalid mode. <-");

            //Console.WriteLine("всё ок");
        }
    }
}
