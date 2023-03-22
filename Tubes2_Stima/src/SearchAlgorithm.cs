using Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    abstract public class SearchAlgorithm
    {
        public abstract void insertNode(Block n, char a);
        public abstract string startSearch(Block n, bool TSP);
        public abstract void insertChild(Block n, char lastMove, ref bool notDeadend);

    }
}