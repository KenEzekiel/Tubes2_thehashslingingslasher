using System;
using Blocks;
using Positions;
using System.Drawing;
using System.IO;

namespace Matrices
{
    public class Matrix
    {
        private int nCol, nRow;
        private Block[,] mat;
        private Position start;
        public static int NumOfSteppableNodes = 0;
        private const string assetsPath = "../../assets/";

        public Matrix(int rows, int cols)
        {
            mat = new Block[rows, cols];
            nRow = rows;
            nCol = cols;
            start = new Position();
        }

        public Matrix(string path)
        {
            Treasure.resetTreasure();
            NumOfSteppableNodes = 0;
            string[] rows = File.ReadAllLines(path);
            nRow = rows.Length;
            nCol = rows[0].Length / 2 + 1;
            mat = new Block[nRow, nCol];
            start = new Position();
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
                            NumOfSteppableNodes++;
                            break;
                        case 'K':
                            mat[i, j] = new Start();
                            mat[i, j].setID(id);
                            id++;
                            start.setI(i);
                            start.setJ(j);
                            NumOfSteppableNodes++;
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
                            NumOfSteppableNodes++;
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

        public Block GetBlock(int i, int j) { return mat[i, j]; }

        public Block GetBlock(Position p) { return mat[p.getI(), p.getJ()]; }

        public Block GetStart() { return mat[start.getI(), start.getJ()]; }

        public Position GetStartPos() { return start; }

        public void getNeighbours(int i, int j)
        {
            // Get all the neighbours of block i, j
            // Left
            if (j > 0)
            {
                // If can step, then not tembok
                if (this.mat[i, j - 1].canStep())
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

        public void resetMatrixStep() {
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++) {
                    if (this.mat[i, j].canStep())
                        ((Basic)this.mat[i, j]).resetStep();
                }
            }

        }

        public void resetEverything()
        {
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    if (this.mat[i, j].canStep())
                    {
                        ((Basic)this.mat[i, j]).resetStep();
                        if (this.mat[i, j].isTreasure())
                        {
                            ((Treasure)this.mat[i, j]).resetTaken();
                        }
                    }
                }
            }
            Treasure.resetTreasureTaken();
        }

        public int GetNumOfSteppedNodes() {
            int NumOfSteppedNodes = 0;
            for (int i = 0; i < nRow; i++) {
                for (int j = 0; j < nCol; j++) {
                    if (this.mat[i,j].canStep() && ((Basic)this.mat[i, j]).getStepCount() > 0) {
                        NumOfSteppedNodes++;
                    }
                }
            }
            return NumOfSteppedNodes;
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

        public void stepAt(int i, int j) { mat[i, j].step(); }

        public void stepAt(Position p) { mat[p.getI(), p.getJ()].step(); }

        public void walk(string walkPath)
        {
            resetEverything();
            Position currPos = new Position(this.start);
            foreach (char dir in walkPath)
            {
                this.stepAt(currPos);
                currPos.move(dir);
            }
            this.stepAt(currPos);
        }

        // VISUALIZATION
        const int squareSize = 200;
        const int pad = 20;
        const int sidePad = 20;

        public void animateWalk(string folderPath, string walkPath, string playerPath)
        {
            resetEverything();

            int minHeight, minWidth;

            minHeight = nRow * squareSize + (nRow - 1) * pad + 2 * sidePad;
            minWidth = nCol * squareSize + (nCol - 1) * pad + 2 * sidePad;

            Bitmap image = new Bitmap(minWidth, minHeight);
            Graphics graphic = Graphics.FromImage(image);

            visualize(ref graphic);

            Position curr = new Position(this.start);
            stepAt(curr);
            rerenderPtr(ref graphic, curr, playerPath);
            Position prev = new Position(curr);
            image.Save(folderPath + (1).ToString() + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

            for (int i = 0; i < walkPath.Length; i++)
            {
                rerenderAt(ref graphic, prev, false);
                curr.move(walkPath[i]);
                stepAt(curr);
                rerenderPtr(ref graphic, curr, playerPath);
                prev.setI(curr.getI());
                prev.setJ(curr.getJ());
                image.Save(folderPath + (i + 2).ToString() + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public void rerenderPtr(ref Graphics graphic, Position p, string playerPath)
        {
            int i, j, currX, currY;
            i = p.getI();
            j = p.getJ();
            currX = sidePad + j * (pad + squareSize);
            currY = sidePad + i * (pad + squareSize);

            Image player = resizeImage(Image.FromFile(assetsPath + playerPath), new Size(squareSize, squareSize));
            graphic.DrawImage(player, new Point(currX, currY));
        }

        public void rerenderAt(ref Graphics graphic, Position p, bool alphaOn)
        {
            int i = p.getI();
            int j = p.getJ();

            Image baseBlock = resizeImage(Image.FromFile(assetsPath + "baseblock.png"), new Size(squareSize, squareSize));
            Image closedTreasure = resizeImage(Image.FromFile(assetsPath + "chest-closed.png"), new Size(squareSize, squareSize));
            Image openedTreasure = resizeImage(Image.FromFile(assetsPath + "chest-opened.png"), new Size(squareSize, squareSize));
            Image startBlock = resizeImage(Image.FromFile(assetsPath + "start.png"), new Size(squareSize, squareSize));

            if (!this.mat[i, j].isTembok())
            {
                Point currPoint = new Point(sidePad + j * (pad + squareSize), sidePad + i * (pad + squareSize));

                graphic.DrawImage(baseBlock, currPoint);

                SolidBrush stepBrush;
                if (((Basic)this.mat[i, j]).isStepped())
                {
                    if (alphaOn)
                    {
                        stepBrush = new SolidBrush(this.mat[i, j].getColor());
                    } else
                    {
                        stepBrush = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
                    }

                    Rectangle stepRect = new Rectangle(currPoint, new Size(squareSize, squareSize));
                    graphic.FillRectangle(stepBrush, stepRect);
                }
                
                if (this.mat[i, j].isTreasure())
                {
                    if (((Treasure)this.mat[i, j]).isTaken())
                    {
                        graphic.DrawImage(openedTreasure, currPoint);
                    }
                    else
                    {
                        graphic.DrawImage(closedTreasure, currPoint);
                    }
                }
                else if (i == this.start.getI() && j == this.start.getJ())
                {
                    graphic.DrawImage(startBlock, currPoint);
                }
            }
        }

        public void visualize(string path)
        {
            int minHeight = nRow * squareSize + (nRow - 1) * pad + 2 * sidePad;
            int minWidth = nCol * squareSize + (nCol - 1) * pad + 2 * sidePad;

            Bitmap image = new Bitmap(minWidth, minHeight);
            Graphics graphic = Graphics.FromImage(image);

            visualize(ref graphic);

            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void visualize(ref Graphics graphic)
        {
            int minHeight = nRow * squareSize + (nRow - 1) * pad + 2 * sidePad;
            int minWidth = nCol * squareSize + (nCol - 1) * pad + 2 * sidePad;

            Image bg = new Bitmap(assetsPath + "background.png");
            TextureBrush tBrush = new TextureBrush(bg);
            graphic.FillRectangle(tBrush, new Rectangle(0, 0, minWidth, minHeight));

            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    rerenderAt(ref graphic, new Position(i, j), true);
                }
            }
        }

        public void visualizeAll(string folderPath, string route, string search)
        {
            // clean directory
            Directory.CreateDirectory(folderPath);

            DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            // select player randomly
            string[] playersPath = { "gary.png", "mrkrabs.png", "patrick.png", "plankton.png", "sandy.png", "squidward.png" };
            Random rnd = new Random();
            string player = playersPath[rnd.Next(playersPath.Length)];

            // visualize kosong(belom diapa-apain, 1 gambar)
            resetEverything();
            visualize(folderPath + "0.jpeg");

            // visualize search (len(search) + 1 gambar)
            if (search.Length < 200)
            {
                animateWalk(folderPath, search, player);
            }
            else
            {
                string temp = search[0].ToString();
                animateWalk(folderPath, temp, player);
            }


            // visualize route (1 gambar)
            walk(route);
            visualize(folderPath + (search.Length + 2).ToString() + ".jpeg");
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
    }
}
