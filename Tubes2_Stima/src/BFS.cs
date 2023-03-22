using Blocks;
using Matrices;
using Positions;
using System.Collections.Generic;
using System;

namespace Tubes2_Stima.src
{
    class BFS : SearchAlgorithm
    {
        private Matrix map;
        public BFS(Matrix map) { this.map = map; }


        public void InsertNode(ref List<Position> list, Position p)
        {
            list.Add(p);
        }
        public void InsertChild(List<Position> path, Position currNode, ref Queue<List<Position>> posQueue)
        {
            if (this.map.GetBlock(currNode).hasL)
            {
                Position temp = currNode.getLPos();
                if (!((Basic)this.map.GetBlock(temp)).isStepped())
                {
                    List<Position> newPath = new List<Position>(path);
                    InsertNode(ref newPath, temp);
                    posQueue.Enqueue(new List<Position>(newPath));
                }
            }
            if (this.map.GetBlock(currNode).hasU)
            {
                Position temp = currNode.getUPos();
                if (!((Basic)this.map.GetBlock(temp)).isStepped())
                {
                    List<Position> newPath = new List<Position>(path);
                    InsertNode(ref newPath, temp);
                    posQueue.Enqueue(new List<Position>(newPath));
                }
            }
            if (this.map.GetBlock(currNode).hasR)
            {
                Position temp = currNode.getRPos();
                if (!((Basic)this.map.GetBlock(temp)).isStepped())
                {
                    List<Position> newPath = new List<Position>(path);
                    InsertNode(ref newPath, temp);
                    posQueue.Enqueue(new List<Position>(newPath));
                }
            }
            if (this.map.GetBlock(currNode).hasD)
            {
                Position temp = currNode.getDPos();
                if (!((Basic)this.map.GetBlock(temp)).isStepped())
                {
                    List<Position> newPath = new List<Position>(path);
                    InsertNode(ref newPath, temp);
                    posQueue.Enqueue(new List<Position>(newPath));
                }
            }

        }

        public static string PosToString(List<Position> moves)
        {
            string movesDir = "";
            for (int i = 0; i < moves.Count - 1; i++)
            {
                movesDir += moves[i].getDirTo(moves[i + 1]);
            }
            return movesDir;
        }

        public static void PrintQueue(Queue<List<Position>> q)
        {
            Console.Write("{");
            foreach (List<Position> l in q)
            {
                Console.Write("[");
                foreach (Position p in l)
                {
                    Console.Write(p);
                }
                Console.Write("] ");
            }
            Console.Write("}");
            Console.Write("\n");
        }

        public static void PrintList(List<Position> l)
        {
            Console.Write("[");
            foreach (Position p in l)
            {
                Console.Write(p);
            }
            Console.Write("] ");
            Console.Write("\n");
        }

        public static void ChangeList(List<Position> prev, List<Position> curr, ref List<Position> search)
        {
            if (prev.Count >= 2)
            {
                int lastEq = 0;
                for (int i = 0; i < prev.Count; i++)
                {
                    if (!prev[i].isEqual(curr[i]))
                    {
                        break;
                    }
                    else
                    {
                        lastEq = i;
                    }
                }
                for (int i = prev.Count - 2; i >= lastEq; i--)
                {
                    search.Add(prev[i]);
                }
                for (int i = lastEq + 1; i < curr.Count; i++)
                {
                    search.Add(curr[i]);
                }
            }
            else
            {
                search.Add(curr[curr.Count - 1]);
            }
        }

        public Position SubBFS(Position startNode, ref string result, ref string search, ref bool took)
        {
            Queue<List<Position>> posQueue = new Queue<List<Position>>();

            List<Position> posList = new List<Position>();
            posList.Add(startNode);
            posQueue.Enqueue(new List<Position>(posList));

            List<Position> prev = new List<Position>();
            List<Position> searchPos = new List<Position>();
            Position nextPos = new Position();

            while (posQueue.Count > 0)
            {
                List<Position> path = new List<Position>(posQueue.Dequeue());
                Position currNode = new Position(path[path.Count - 1]);
                nextPos = new Position(currNode);
                List<Position> next = path;
                ChangeList(prev, next, ref searchPos);

                if (this.map.GetBlock(currNode).isTreasure() && !((Treasure)this.map.GetBlock(currNode)).isTaken())
                {
                    result += PosToString(path);
                    search += PosToString(searchPos);
                    took = true;
                    this.map.GetBlock (currNode).step();
                    return currNode;
                }
                if (this.map.GetBlock(currNode).canStep() && !((Basic)this.map.GetBlock(currNode)).isStepped())
                {
                    InsertChild(path, currNode, ref posQueue);
                    this.map.GetBlock(currNode).step();
                }
                prev = next;
            }
            search += PosToString(searchPos);
            took = false;
            return nextPos;
        }

        public Position SubBFSTSP(Position startNode, ref string result, ref string search)
        {
            Queue<List<Position>> posQueue = new Queue<List<Position>>();

            List<Position> posList = new List<Position>();
            posList.Add(startNode);
            posQueue.Enqueue(new List<Position>(posList));

            List<Position> prev = new List<Position>();
            List<Position> searchPos = new List<Position>();

            while (posQueue.Count > 0)
            {
                List<Position> path = new List<Position>(posQueue.Dequeue());
                Position currNode = new Position(path[path.Count - 1]);
                List<Position> next = new List<Position>(path);
                ChangeList(prev, next, ref searchPos);

                if (currNode.isEqual(this.map.GetStartPos()))
                {
                    search += PosToString(searchPos);
                    result += PosToString(path);
                    this.map.GetBlock(currNode).step();
                    return currNode;
                }
                if (this.map.GetBlock(currNode).canStep() && !((Basic)this.map.GetBlock(currNode)).isStepped())
                {
                    InsertChild(path, currNode, ref posQueue);
                    this.map.GetBlock(currNode).step();
                }
                prev = next;
            }
            search += PosToString(searchPos);
            return new Position();
        }

        public string Search(bool tsp, ref string search)
        {
            string moves = "";
            bool took = false;
            Position next = SubBFS(this.map.GetStartPos(), ref moves, ref search, ref took);

            while (took && !Treasure.isAllTaken())
            {
                map.resetMatrixStep();
                next = new Position(SubBFS(next, ref moves, ref search, ref took));
            }

            if (tsp)
            {
                map.resetMatrixStep();
                SubBFSTSP(next, ref moves, ref search);
            }

            return moves;
        }
    }
}
