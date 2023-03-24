using System;
using System.Drawing;

namespace Blocks
{
    abstract public class Block
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

        public abstract bool isTreasure();
        public abstract bool isTembok();
    }

    class Basic : Block
    {
        protected int stepCount;
        protected Color baseColor;

        public Basic()
        {
            this.stepCount = 0;
            this.baseColor = ColorTranslator.FromHtml("#FF0000");
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
            int alpha = stepCount * 40 % 255;
            return Color.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
        }

        public override string getInfo()
        {
            return "";
        }

        public void resetStep() {
            this.stepCount = 0;
        }

        public override bool isTreasure() { return false; }

        public bool isStepped() { return stepCount != 0; }

        public override bool isTembok() { return false; }
    }

    class Tembok : Block
    {
        private Color baseColor;

        public Tembok()
        {
            this.baseColor = Color.FromName("Black");
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

        public override bool isTreasure() { return false; }

        public override bool isTembok() { return true; }
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

        public override bool isTreasure() { return false; }
    }

    class Treasure : Basic
    {
        private bool taken;
        static private int treasureCount = 0;
        static private int treasureTaken = 0;

        public Treasure()
        {
            taken = false;
            treasureCount++;
        }

        public override void step()
        {
            
            if (!taken) {
                taken = true;
                treasureTaken++;
            }
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

        static public bool isAllTaken()
        {
            return treasureTaken == treasureCount;
        }

        static public int getTreasureCount()
        { 
            return treasureCount;
        }

        static public int getTreasureTaken() 
        {
            return treasureTaken;
        }

        public override bool isTreasure() { return true; }

        public void resetTaken() { taken = false; }

        public static void resetTreasureTaken() { treasureTaken = 0; }

        public static void resetTreasure()
        {
            // only called if map is initialized
            resetTreasureTaken();
            treasureCount = 0;
        }
    }
}