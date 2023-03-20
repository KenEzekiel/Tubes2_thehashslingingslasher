using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    class TSPBFS : BFS
    {
        bool stopNow = false;
        public override void insertChild(Block n, char lastMove, bool notDeadend)
        {
            // Priority : L U R D
            while (!stopNow)
            {
                n.getInfo();
                if (n.nStart)
                {
                    notDeadend = false;
                    stopNow = true;
                    break;
                }
                if ( !(lastMove == 'R'))
                {
                    insertNode(n.getL(), 'L');
                    notDeadend = true;
                }
                if (!(lastMove == 'D'))
                {
                    insertNode(n.getU(), 'U');
                    notDeadend = true;
                }
                if (!(lastMove == 'L'))
                {
                    insertNode(n.getR(), 'R');
                    notDeadend = true;
                }
                if (!(lastMove == 'U'))
                {
                    insertNode(n.getD(), 'D');
                    notDeadend = true;
                }
            }

        }
    }
}
