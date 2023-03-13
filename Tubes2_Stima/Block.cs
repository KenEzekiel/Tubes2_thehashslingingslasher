using System;
using SkiaSharp;
using static SkiaSharp.SKColor;

namespace Blocks
{
    abstract class Block
    {
        public abstract bool canStep();
        public abstract void step();
        public abstract SKColor getColor();
        public abstract string getInfo();
    }

    class Basic : Block
    {
        protected int stepCount;
        protected SKColor baseColor;

        public Basic()
        {
            this.stepCount = 0;
            this.baseColor = new SKColor(0xFF8A2BE2);
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
            return "B" + stepCount.ToString() + getColor().ToString();
        }

        public override SKColor getColor()
        {
            byte b = new byte();
            b = (byte)(stepCount * 50);
            return new SKColor(baseColor.Red, baseColor.Green, baseColor.Blue, b);
        }

        public override string getInfo()
        {
            return "";
        }
    }

    class Tembok : Block
    {
        private SKColor baseColor;

        public Tembok()
        {
            this.baseColor = new SKColor(0xFF000000);
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

        public override SKColor getColor()
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