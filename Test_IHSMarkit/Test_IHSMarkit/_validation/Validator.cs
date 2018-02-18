using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_IHSMarkit.Interface;

namespace Test_IHSMarkit._validation
{
    public class Validator
    {
        private IValidation validator { get; set; }

        public Validator()
        { }
        public Validator(IValidation validator)
        {
            this.validator = validator;
        }

        public void isValid()
            => validator.isValid();

        public void isValid(String[] args)
            => validator.isValid(args);
    }
}
