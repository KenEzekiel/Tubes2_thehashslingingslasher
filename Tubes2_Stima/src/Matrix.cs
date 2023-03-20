using System;
using Blocks;
using Players;
using System.Drawing;
using System.IO;
using System.Drawing.Text;

namespace Matrices
{
    public class Matrix
    {
        private int nCol, nRow;
        private Block[,] mat;

        public Matrix(int rows, int cols, ref Player p)
        {
            mat = new Block[rows, cols];
            nRow = rows;
            nCol = cols;
        }

        public Block GetBlock(int x, int y)
        {
            return mat[x, y];
        }

        public Matrix(string path, ref Player p)
        {
            string[] rows = File.ReadAllLines(path);
            nRow = rows.Length;
            nCol = rows[0].Length / 2 + 1;
            mat = new Block[nRow, nCol];
            int id = 1;
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    char c = rows[i][j * 2];
                    switch (c)
                    {
                        case 'T':
                            mat[i, j] = new Treasure();
                            mat[i, j].setID(id);
                            id++;
                            break;
                        case 'K':
                            mat[i, j] = new Start();
                            mat[i, j].setID(id);
                            id++;
                            p.setX(j);
                            p.setY(i);
                            break;
                        case 'X':
                            mat[i, j] = new Tembok();
                            mat[i, j].setID(id);
                            id++;
                            break;
                        case 'R':
                            mat[i, j] = new Basic();
                            mat[i, j].setID(id);
                            id++;
                            break;
                        case ' ':
                            continue;
                        default:
                            throw new Exception("Invalid characters detected");
                    }
                }
            }
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    getNeighbours(i, j);
                }
            }
        }

        public void getNeighbours(int i, int j)
        {
            // Get all the neighbours of block i, j
            // Left
            if (j > 0)
            {
                // If can step, then not tembok
                if (this.mat[i, j-1].canStep())
                {
                    this.mat[i, j].setL(this.mat[i, j - 1]);
                }
                
            }
            // Right
            if (j < nCol - 1)
            {
                // If can step, then not tembok
                if (this.mat[i, j + 1].canStep())
                {
                    this.mat[i, j].setR(this.mat[i, j + 1]);
                }
            }
            // Up
            if (i > 0)
            {
                // If can step, then not tembok
                if (this.mat[i - 1, j].canStep())
                {
                    this.mat[i, j].setU(this.mat[i - 1, j]);
                }
            }
            // Down
            if (i < nRow - 1)
            {
                // If can step, then not tembok
                if (this.mat[i + 1, j].canStep())
                {
                    this.mat[i, j].setD(this.mat[i + 1, j]);
                }
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    res += mat[i, j].ToString();
                    if (j != nCol - 1)
                    {
                        res += " ";
                    }
                }
                if (i != nRow - 1)
                {
                    res += "\n";
                }
            }
            return res;
        }

        public void stepAt(int x, int y)
        {
            mat[y, x].step();
        }

        public void walk(Player p, char[] walkPath)
        {
            foreach (char dir in walkPath)
            {
                this.stepAt(p.getX(), p.getY());
                p.move(dir);
            }
            this.stepAt(p.getX(), p.getY());
        }

        public void visualize(string path)
        {
            int squareSize, pad, minHeight, minWidth, sidePad, currX, currY;
            squareSize = 200;
            pad = 20;
            sidePad = 20;
            minHeight = nRow * squareSize + (nRow - 1) * pad + 2 * sidePad;
            minWidth = nCol * squareSize + (nCol - 1) * pad + 2 * sidePad;
            currX = sidePad;
            currY = sidePad;

            Bitmap image = new Bitmap(minWidth, minHeight);
            Graphics graphic = Graphics.FromImage(image);

            SolidBrush baseBrush = new SolidBrush(Color.Black);

            Rectangle baseRect = new Rectangle(0, 0, minWidth, minHeight);
            graphic.FillRectangle(baseBrush, baseRect);

            int fontSize = 32;
            var fontFamily = new FontFamily("Arial");
            var font = new Font(fontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            var textBrush = new SolidBrush(Color.Black);
            graphic.TextRenderingHint = TextRenderingHint.AntiAlias;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {

                    SolidBrush baseBlockBrush = new SolidBrush(Color.White);

                    SolidBrush blockBrush = new SolidBrush(mat[i, j].getColor());

                    Rectangle baseBlockRect = new Rectangle(currX, currY, squareSize, squareSize);
                    graphic.FillRectangle(baseBlockBrush, baseBlockRect);

                    Rectangle blockRect = new Rectangle(currX, currY, squareSize, squareSize);
                    graphic.FillRectangle(blockBrush, blockRect);

                    graphic.DrawString(mat[i,j].getInfo(), font, textBrush, blockRect, stringFormat);
                    currX += (pad + squareSize);
                }
                currX = sidePad;
                currY += (pad + squareSize);
            }

            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            image.Dispose();
        }
    }
}

