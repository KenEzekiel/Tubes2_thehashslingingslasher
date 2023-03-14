using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    abstract class SearchAlgorithm
    {
        
        public List<char> ListOfMoves = new List<char>();

        public abstract void insertNode(Block node);
        public abstract void startSearch(Block node);

        public void insertMove(char a) {
            this.ListOfMoves.Add(a);
        }
    }
}