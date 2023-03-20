using Blocks;
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

        public int numOfTreasure = 0;

        public DFS(int numOfTreasure)
        {
            this.numOfTreasure = numOfTreasure;
        }

        public override void insertNode(Block n, char a) {
            (char move, Block node) temp = (a, n);
            this.NodeMoves.Push(temp);
        }

        public override void insertChild(Block n, char lastMove, ref bool notDeadend) {
            // Priority : L U R D
            if (n.hasL && !(lastMove == 'R')) {
                insertNode(n.getL(), 'L');
                notDeadend = true;
            }
            if (n.hasU && !(lastMove == 'D')) {
                insertNode(n.getU(), 'U');
                notDeadend = true;
            }
            if (n.hasR && !(lastMove == 'L')) {
                insertNode(n.getR(), 'R');
                notDeadend = true;
            }
            if (n.hasD && !(lastMove == 'U')) {
                insertNode(n.getD(), 'D');
                notDeadend = true;
            }
        }

        public (char move, Block node) getChild() {
            return this.NodeMoves.Pop();
        }
        public override string startSearch(Block n, bool TSP) {
            // DFS with IDS, initial depth limit = 5
            string moves = "";
            char lastMove = 'S';
            moves = Search(n, ref lastMove, moves, TSP);
            // tinggal return moves
            // TODO: reset matrix abis ini
            if (Treasure.getTreasureCount() != Treasure.getTreasureTaken()) {
                Console.WriteLine("No path found");
                return "";
            }
            return moves;
        }

        public void reverseMove(ref string currentMoves, char lastMove) {
                if (lastMove == 'L') {
                    currentMoves += "R";
                }
                if (lastMove == 'U') {
                    currentMoves += "D";
                }
                if (lastMove == 'R') {
                    currentMoves += "L";
                }
                if (lastMove == 'D') {
                    currentMoves += "U";
                }
        }

        public string Search(Block node, ref char lastMove, string moves, bool TSP) {

            string currentMoves = moves;
            

            if (Treasure.getTreasureCount() == Treasure.getTreasureTaken())
            {
                moves = currentMoves;
                return currentMoves;
            } else {
                node.step();
                // di step nya treasure, tambahin num of treasure++
                // terus nanti get num of gotten treasurenya, bandingin sama total treasure
                // tambahin currentMove. terus kasih ke block ID
                currentMoves += lastMove;
                this.BlockIDMovesMapping[node.getID()] = currentMoves;
            
            
            // cari child nya yang bukan parentnya
            bool notDeadend = false;
            insertChild(node, lastMove,ref notDeadend);

            // kalo TSP nanti pas treasure countnya dah sesuai, tambahin start sebagai treasure terakhir (beda module aja)
            // Udah di taken, tinggal ditambahin treasure count, kalau misal udah sama yauda stop, kaloga mulai balik ke start kalo TSP

            // if (treasure udah keambil semua) { kalo TSP, backtrack (reverse moves nya ato cari Start sbg Treasure terakhir, boleh, jangan lupa aja block ID nya di reset semua, kalau ga yauda beresin aja langsung return)}

            if (notDeadend) {
                
                // lanjut
                (char move, Block node) Child;
                char nextMove;
                Block nextBlock;
                if (lastMove == 'S') {
                    
                    for (int i = 0; i < node.getNumOfChild(); i++) {
                        // Search untuk semua child yang dimiliki oleh node yang sedang diinjak
                        Child = getChild();
                        nextMove = Child.move;
                        nextBlock = Child.node;
                        if (BlockIDMovesMapping.ContainsKey(nextBlock.ID) && currentMoves.StartsWith(BlockIDMovesMapping[nextBlock.ID])) {
                            // Block yang akan dikunjungi sudah pernah dikunjungi oleh track yang sama
                            return currentMoves;
                        } else {
                            currentMoves = Search(nextBlock, ref nextMove, currentMoves, TSP);
                        }
                    }
                
                if (TSP || !(Treasure.getTreasureCount() == Treasure.getTreasureTaken())) {
                    reverseMove(ref currentMoves, lastMove);
                }
                } else {
                    for (int i = 0; i < node.getNumOfChild() - 1; i++) {
                        // Search untuk semua child yang dimiliki oleh node yang sedang diinjak
                        Child = getChild();
                        nextMove = Child.move;
                        nextBlock = Child.node;
                        if (BlockIDMovesMapping.ContainsKey(nextBlock.ID) && currentMoves.StartsWith(BlockIDMovesMapping[nextBlock.ID])) {
                            // Block yang akan dikunjungi sudah pernah dikunjungi oleh track yang sama
                            // return currentMoves;
                        } else {
                            currentMoves = Search(nextBlock, ref nextMove, currentMoves, TSP);
                            
                        }  
                    }
                    if (TSP || !(Treasure.getTreasureCount() == Treasure.getTreasureTaken())) {
                        reverseMove(ref currentMoves, lastMove);
                    }
                }
                
                return currentMoves;
                
            } else {
                // deadend, backtrack
                if (TSP || !(Treasure.getTreasureCount() == Treasure.getTreasureTaken())) {
                    reverseMove(ref currentMoves, lastMove);
                    if (lastMove == 'L') {
                        lastMove = 'R';
                    }
                    else if (lastMove == 'U') {
                        lastMove = 'D';
                    }
                    else if (lastMove == 'R') {
                        lastMove = 'L';
                    }
                    else if (lastMove == 'D') {
                        lastMove = 'U';
                    }
                }
                
                return currentMoves;
            }
            }
        }
    }
}