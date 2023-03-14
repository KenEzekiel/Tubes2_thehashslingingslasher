using Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    class BFS : SearchAlgorithm
    {
        public Queue<(char move, Block node)> NodeMoves = new Queue<(char move, Block node)>();

        public override void insertChild(Block n, char lastMove, bool notDeadend)
        {
            // Priority : L U R D
            if (n.hasL && !(lastMove == 'R'))
            {
                insertNode(n.getL(), 'L');
                notDeadend = true;
            }
            if (n.hasU && !(lastMove == 'D'))
            {
                insertNode(n.getU(), 'U');
                notDeadend = true;
            }
            if (n.hasR && !(lastMove == 'L'))
            {
                insertNode(n.getR(), 'R');
                notDeadend = true;
            }
            if (n.hasD && !(lastMove == 'U'))
            {
                insertNode(n.getD(), 'D');
                notDeadend = true;
            }
        }

        public override void insertNode(Block n, char a)
        {
            (char move, Block node) temp = (a, n);
            this.NodeMoves.Enqueue(temp);
        }
        public override void startSearch(Block node)
        {

        }
    }
}
