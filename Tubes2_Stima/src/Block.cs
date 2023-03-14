using System;
using System.Drawing;

namespace Blocks
{
    abstract class Block
    {
        public abstract bool canStep();
        public abstract void step();
        public abstract Color getColor();
        public abstract string getInfo();
        public Block L;
        public Block R;
        public Block U;
        public Block D;

        public int ID;

        public bool hasL = false;
        public bool hasR = false;
        public bool hasU = false;
        public bool hasD = false;
        public bool hasID = false;
        public int numOfChild = 0;

        public void setL(Block L) { this.L = L; this.hasL = true; this.numOfChild++; }
        public void setR(Block R) { this.R = R; this.hasR = true; this.numOfChild++; }
        public void setU(Block U) { this.U = U; this.hasU = true; this.numOfChild++; }
        public void setD(Block D) { this.D = D; this.hasD = true; this.numOfChild++; }
        public void setID(int ID) { this.ID = ID; this.hasID = true; }

        // Getter, but has to be validated if hasX first
        public Block getL() { return this.L; }
        public Block getR() { return this.R; }
        public Block getU() { return this.U; }
        public Block getD() { return this.D; }
        public int getID() { return this.ID; }

        public bool hasChild() { return this.hasL && this.hasR && this.hasU && this.hasD;}

        public int getNumOfChild() { return this.numOfChild; }

        
    }

    class Basic : Block
    {
        protected int stepCount;
        protected Color baseColor;

        public Basic()
        {
            this.stepCount = 0;
            this.baseColor = new Color(0xFF8A2BE2);
        }

        public int getStepCount()
        {
            return this.stepCount;
        }

        public override void step()
        {
            this.stepCount++;
        }

        public override bool canStep()
        {
            return true;
        }

        public override string ToString()
        {
            return "B" + stepCount.ToString();
        }

        public override Color getColor()
        {
            byte b = new byte();
            b = (byte)(stepCount * 50);
            return new Color(baseColor.Red, baseColor.Green, baseColor.Blue, b);
        }

        public override string getInfo()
        {
            return "";
        }
        
    }

    class Tembok : Block
    {
        private Color baseColor;

        public Tembok()
        {
            this.baseColor = new Color(0xFF000000);
        }

        public override bool canStep()
        {
            return false;
        }

        public override void step()
        {
            throw new Exception("Cannot be stepped");
        }

        public override string ToString()
        {
            return "X-";
        }

        public override Color getColor()
        {
            return baseColor;
        }

        public override string getInfo()
        {
            return "";
        }
    }

    class Start : Basic
    {
        public override string ToString()
        {
            return "S" + stepCount.ToString();
        }

        public override string getInfo()
        {
            return "Start";
        }
    }

    class Treasure : Basic
    {
        private bool taken;

        public Treasure()
        {
            taken = false;
        }

        public override void step()
        {
            taken = true;
            this.stepCount++;
        }

        public override string ToString()
        {
            return "T" + stepCount.ToString();
        }

        public override string getInfo()
        {
            return "Treasure";
        }

        public bool isTaken()
        {
            return taken;
        }
    }
}