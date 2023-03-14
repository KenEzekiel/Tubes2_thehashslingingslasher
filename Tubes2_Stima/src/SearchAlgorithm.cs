using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    abstract class SearchAlgorithm
    {
        
        

        public abstract void insertNode(Block n, char a);
        public abstract void startSearch(Block n);
        public abstract void insertChild(Block n);

    }
}