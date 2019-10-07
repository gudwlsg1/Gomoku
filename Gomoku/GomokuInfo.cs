using System;
using System.Drawing;

namespace Gomoku
{
    public enum Stone
    {
        None,
        Black,
        White
    }

    public class GomokuInfo : ICloneable
    {
        public static int GoStoneSize { get => 20; }
        public static int StarPointSize { get => 16; }
        public static int DrawingScaleSize { get => 8; }

        public int X { get; set; }
        public int Y { get; set; }

        private bool isExist = false;
        public bool IsExist
        {
            get => isExist;
            set => isExist = value;
        }

        private Stone stone = Stone.None;
        public Stone Stone
        {
            get => stone;
            set => stone = value;
        }

        public object Clone()
        {
            GomokuInfo gomoku = new GomokuInfo();
            gomoku.X = this.X;
            gomoku.Y = this.Y;
            gomoku.IsExist = this.IsExist;

            return gomoku;
        }
    }
}
