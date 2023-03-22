using System;

namespace Positions
{
    public class Position
    {
        // i baris, j kolom

        private int i, j;

        public Position() { i = -1; j = -1; }

        public Position(int _i, int _j) { i = _i; j = _j; }

        public Position(Position p) { this.i = p.i; this.j = p.j; }

        public int getI() { return i; }

        public int getJ() { return j; }

        public void setI(int _i) { i = _i; }

        public void setJ(int _j) { j = _j; }

        public void move(char direction)
        {
            switch (direction)
            {
                case 'R':
                    j++;
                    break;
                case 'L':
                    j--;
                    break;
                case 'U':
                    i--;
                    break;
                case 'D':
                    i++;
                    break;
            }
        }

        public bool isValidPos() { return i != -1 && j != -1; }

        public string getDirTo(Position other)
        {
            if (other.j - this.j == 1)
            {
                return "R";
            }
            else if (other.j - this.j == -1)
            {
                return "L";
            }
            else if (other.i - this.i == 1)
            {
                return "D";
            }
            else if (other.i - this.i == -1)
            {
                return "U";
            }
            else
            {
                throw new Exception("Invalid moves");
            }
        }

        public bool isEqual(Position other) { return this.i == other.i && this.j == other.j; }

        public override string ToString() { return "(" + i.ToString() + ", " + j.ToString() + ")"; }
    }
}