using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_IHSMarkit.Interface;

namespace Test_IHSMarkit._creation
{
    public class Creator
    {
        private ICreating creator { get; set; }

        public Creator()
        { }
        public Creator(ICreating creator)
        {
            this.creator = creator;
        }

        public void Create()
            => creator.Create();

        public void Create(String[] args, String outpath)
            => creator.Create(args, outpath);
    }
}
