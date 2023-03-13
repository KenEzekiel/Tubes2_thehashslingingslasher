using System;
using Blocks;
using Players;
using SkiaSharp;
using System.IO;

namespace Matrices
{
    public class Matrix
    {
        private int nCol, nRow;
        private Block[,] mat;

        public Matrix(int rows, int cols, Player p)
        {
            mat = new Block[rows, cols];
            nRow = rows;
            nCol = cols;
        }

        public Matrix(string path, Player p)
        {
            string[] rows = File.ReadAllLines(path);
            nRow = rows.Length;
            nCol = rows[0].Length / 2 + 1;
            mat = new Block[nRow, nCol];
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    char c = rows[i][j * 2];
                    switch (c)
                    {
                        case 'T':
                            mat[i, j] = new Treasure();
                            break;
                        case 'K':
                            mat[i, j] = new Start();
                            p.setX(j);
                            p.setY(i);
                            break;
                        case 'X':
                            mat[i, j] = new Tembok();
                            break;
                        case 'R':
                            mat[i, j] = new Basic();
                            break;
                        case ' ':
                            continue;
                        default:
                            throw new Exception("Invalid characters detected");
                    }
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
                Console.WriteLine(p);
                this.stepAt(p.getX(), p.getY());
                p.move(dir);
            }
            Console.WriteLine(p);
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

            SKImageInfo img = new SKImageInfo(minWidth, minHeight);
            SKSurface sfc = SKSurface.Create(img);
            SKCanvas cvs = sfc.Canvas;

            var basePaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = new SKColor(0xFF000000)
            };

            SKRect baseRect = SKRect.Create(0, 0, minWidth, minHeight);
            cvs.DrawRect(baseRect, basePaint);

            var textPaint = new SKPaint
            {
                IsAntialias = true,
                TextSize = 32.0f,
                TextAlign = SKTextAlign.Center,
                Color = new SKColor(0xFF000000)
            };

            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    var baseBlockPaint = new SKPaint
                    {
                        IsAntialias = true,
                        Style = SKPaintStyle.Fill,
                        Color = new SKColor(0xFFFFFFFF)
                    };

                    var blockPaint = new SKPaint
                    {
                        IsAntialias = true,
                        Style = SKPaintStyle.Fill,
                        Color = mat[i, j].getColor()
                    };

                    SKRect baseBlockRect = SKRect.Create(currX, currY, squareSize, squareSize);
                    cvs.DrawRect(baseBlockRect, baseBlockPaint);

                    SKRect blockRect = SKRect.Create(currX, currY, squareSize, squareSize);
                    cvs.DrawRect(blockRect, blockPaint);

                    cvs.DrawText(mat[i, j].getInfo(), currX + squareSize / 2, currY + squareSize / 2 + textPaint.TextSize / 2, textPaint);

                    currX += (pad + squareSize);
                }
                currX = sidePad;
                currY += (pad + squareSize);
            }

            using (var image = sfc.Snapshot())
            using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
            using (var stream = File.OpenWrite(path))
            {
                data.SaveTo(stream);
            }
        }
    }
}

