using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class CheckerBoard : Form
    {
        private List<GomokuInfo> lstGomoku = new List<GomokuInfo>();

        private Pen pen;
        private Brush wBrush;
        private Brush bBrush;
        private bool isBlackTurn = true;

        public CheckerBoard()
        {
            InitializeComponent();

            this.MouseDown += CheckerBoard_MouseDown;

            pen = new Pen(Color.Black);
            wBrush = new SolidBrush(Color.White);
            bBrush = new SolidBrush(Color.Black);
        }

        #region 바둑알 그리기
        private void CheckerBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int x, y;

            x = e.X;
            y = e.Y;

            GomokuInfo gomoku = GetCloseGomoku(x ,y);

            if (gomoku.IsExist) return;

            drawGoStone(gomoku);
        }

        private void drawGoStone(GomokuInfo gomoku)
        {
            Graphics g = this.CreateGraphics();

            Rectangle r = new Rectangle(gomoku.X - 10, gomoku.Y - 10,
                GomokuInfo.GoStoneSize, GomokuInfo.GoStoneSize);

            gomoku.IsExist = true;

            if (isBlackTurn)
            {
                g.FillEllipse(bBrush, r);
                gomoku.Stone = Stone.Black;
            }
            else
            {
                g.FillEllipse(wBrush, r);
                gomoku.Stone = Stone.White;
            }
            isBlackTurn = !isBlackTurn;
        }

        private GomokuInfo GetCloseGomoku(int x, int y)
        {
            int minY = int.MaxValue;
            int closeY = lstGomoku[0].Y;

            for (int i = 0; i < 19; i++)
            {
                int distance = Math.Abs(lstGomoku[i].Y - y);
                if(minY > distance)
                {
                    minY = distance;
                    closeY = lstGomoku[i].Y;
                }
            }

            List<GomokuInfo> lstTemp = lstGomoku.Where(item => item.Y == closeY).ToList();
            int minX = int.MaxValue;

            GomokuInfo closeGomoKu = null;

            for (int i = 0; i < 19; i++)
            {
                int distance = Math.Abs(lstTemp[i].X - x);
                if (minX > distance)
                {
                    minX = distance;
                    closeGomoKu = lstTemp[i];
                }
            }

            return closeGomoKu;
        }
        #endregion
        #region 바둑판 그리기
        protected override void OnPaint(PaintEventArgs e)
        {
            drawCheckerBoard();

            base.OnPaint(e);
        }

        private void drawCheckerBoard()
        {
            List<GomokuInfo> lstTemp = new List<GomokuInfo>();
            Graphics g = this.CreateGraphics();

            for (int i = 0; i < 76; i += 4)
            {
                int y = 40 + i * GomokuInfo.DrawingScaleSize;
                g.DrawLine(pen, 40, y, 616, y);

                GomokuInfo gomoku = new GomokuInfo();
                gomoku.Y = y;

                lstTemp.Add(gomoku);
            }

            for (int i = 0; i < 76; i += 4)
            {
                int x = 40 + i * GomokuInfo.DrawingScaleSize;
                g.DrawLine(pen, x, 40, x, 40 + 72 * GomokuInfo.DrawingScaleSize);

                for(int j = 0; j < 19; j++)
                {
                    GomokuInfo gomoku = lstTemp[j];

                    GomokuInfo temp = gomoku.Clone() as GomokuInfo;
                    temp.X = x;
                    lstGomoku.Add(temp);
                }
            }

            drawStarPoint(g);
        }

        private void drawStarPoint(Graphics g)
        {
            for (int i = 12; i <= 60; i += 24)
            {
                for (int y = 16; y <= 64; y += 24)
                {
                    Rectangle r = new Rectangle(40 + GomokuInfo.DrawingScaleSize * i - GomokuInfo.StarPointSize / 2,
                        10 + GomokuInfo.DrawingScaleSize * y - GomokuInfo.StarPointSize / 2, GomokuInfo.StarPointSize, GomokuInfo.StarPointSize);
                    g.FillEllipse(bBrush, r);
                }
            }
        }
        #endregion
    }
}
