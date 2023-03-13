using System;

namespace Players
{
    public class Player
    {
        private int x, y;

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int _x)
        {
            x = _x;
        }

        public void setY(int _y)
        {
            y = _y;
        }

        public void move(char direction)
        {
            switch (direction)
            {
                case 'R':
                    x++;
                    break;
                case 'L':
                    x--;
                    break;
                case 'U':
                    y--;
                    break;
                case 'D':
                    y++;
                    break;
            }
        }

        public override string ToString()
        {
            return "Position: " + x.ToString() + " " + y.ToString();
        }
    }
}