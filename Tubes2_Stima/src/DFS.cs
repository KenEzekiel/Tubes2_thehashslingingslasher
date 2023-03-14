using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    class DFS : SearchAlgorithm
    {
        public Stack<(char move, Block node)> NodeMoves = new Stack<(char move, Block node)>();

        public Dictionary<int, string> BlockIDMovesMapping = new Dictionary<int, string>();

        public override void insertNode(Block n, char a) {
            (char move, Block node) temp = (a, n);
            this.NodeMoves.Push(temp);
        }

        public abstract void insertChild(Block n, char lastMove, bool notDeadend) {
            // Priority : L U R D
            if (n.hasL && !(lastMove == 'R')) {
                (char move, Block node) tempL = ('L', n.getL());
                insertNode(tempL);
                notDeadend = true;
            }
            if (n.hasU && !(lastMove == 'D')) {
                (char move, Block node) tempU = ('U', n.getU());
                insertNode(tempU);
                notDeadend = true;
            }
            if (n.hasR && !(lastMove == 'L')) {
                (char move, Block node) tempR = ('R', n.getR());
                insertNode(tempR);
                notDeadend = true;
            }
            if (n.hasD && !(lastMove == 'U')) {
                (char move, Block node) tempD = ('D', n.getD());
                insertNode(tempD);
                notDeadend = true;
            }
        }

        public (char move, Block node) getChild() {
            return this.NodeMoves.Pop();
        }
        public override void startSearch(Block n, List<char> ListOfMoves) {
            // DFS with IDS, initial depth limit = 5
            int initialDepth = 5;
            string moves = "";
            char lastMove = 'S';
            Search(n, lastMove, moves);
            // tinggal return moves
        }

        public void Search(Block node, char lastMove, string moves) {

            string currentMoves = moves;
            
            if (node.getInfo() == "Treasure") {
                if (!node.hasTaken()) {
                    // treasure count + 1
                }
            }
            node.step();
            // tambahin currentMove. terus kasih ke block ID
            currentMoves += lastMove;
            this.BlockIDMovesMapping[node.getID] = currentMoves;
            // cari child nya yang bukan parentnya
            bool notDeadend = false;
            insertChild(node, lastMove, notDeadend);

            // kalo TSP nanti pas treasure countnya dah sesuai, tambahin start sebagai treasure terakhir (beda module aja)
            // Udah di taken, tinggal ditambahin treasure count, kalau misal udah sama yauda stop, kaloga mulai balik ke start kalo TSP

            // if (treasure udah keambil semua) { kalo TSP, backtrack (reverse moves nya ato cari Start sbg Treasure terakhir, boleh, jangan lupa aja block ID nya di reset semua, kalau ga yauda beresin aja langsung return)}

            if (notDeadend) {
                
                // lanjut
                (char move, Block node) Child = getChild();
                char nextMove = Child.move;
                Block nextBlock = Child.node;
                if (currentMoves.StartsWith(BlockIDMovesMapping[nextBlock.ID])) {
                    // Block yang akan dikunjungi sudah pernah dikunjungi oleh track yang sama
                    return;
                } else {
                    for (int i = 0; i < node.getNumOfChild() - 1; i++) {
                        // Search untuk semua child yang dimiliki oleh node yang sedang diinjak
                        Search(nextBlock, nextMove, currentMoves);
                    }
                    
                }
                // masih belum balance?
                
            } else {
                // deadend, backtrack
                if (lastMove == 'L') {
                    moves += "R";
                }
                if (lastMove == 'U') {
                    moves += "D";
                }
                if (lastMove == 'R') {
                    moves += "L";
                }
                if (lastMove == 'D') {
                    moves += "U";
                }
                return;
            }
            
        }
    }
}
