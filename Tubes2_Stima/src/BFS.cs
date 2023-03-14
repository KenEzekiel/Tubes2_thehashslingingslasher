using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    class BFS : SearchAlgorithm
    {
        public Queue<Block> NodeMoves = new Queue<Block>();

        public void insertNode(Block node) {
            this.NodeMoves.Enqueue(node);
        }
        public void startSearch(Block node);
    }
}
