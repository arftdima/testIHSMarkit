using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_IHSMarkit._creation;
using Test_IHSMarkit._validation;
using Test_IHSMarkit.arf_.arf_ex;

     

namespace Test_IHSMarkit
{
    public class main
    {
        public static void Main(String[] args)
        {
            Validator validator = new Validator(new ArgumetsValid());
            Creator creator = new Creator(new CreatorFileTXT());

            try
            {
                validator.isValid(args);
                creator.Create(args, Resource.outputPath);
            }
            catch(arf_Exception exception)
            {
                Console.WriteLine($"my ex: {exception.Message}");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message); 
            }
        }
    }
}
