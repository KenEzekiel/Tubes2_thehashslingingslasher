using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    class DFS : SearchAlgorithm
    {
        public Stack<Block> NodeMoves = new Stack<Block>();

        public void insertNode(Block node) {
            this.NodeMoves.Push(node);
        }
        public void startSearch(Block node) {
            // DFS with IDS, initial depth limit = 5
            int initialDepth = 5;
            IDS(node, 0, initialDepth);

        }

        public void IDS(Block node, int currentDepth, int maxDepth) {
            
        }
    }
}
