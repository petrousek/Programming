using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLifeWindow
{
    public partial class Form1 : Form
    {
        static int xSize = 30;
        static int ySize = 30;
        static int[,] map = new int[xSize, ySize];
        public static Boolean loaded = false;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Generate();
            Thread t1 = new Thread(Game);
            t1.Start();


        }

        public void Game()
        {


            map[15, 14] = 1;
            map[15, 15] = 1;
            map[15, 16] = 1;
            map[14, 15] = 1;
            map[16, 14] = 1;


            while (true)
            {
                ShowMap();

                int[,] map2 = new int[xSize, ySize];


                for (int y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        int countN = HelloNeighbor(x, y);


                        if (map[x, y] > 0)
                        {
                            if (countN < 2 || countN > 3)
                            {
                                map2[x, y] = 0;
                            }
                            else
                            {
                                map2[x, y] = 1;
                            }

                        }
                        else
                        {
                            if (countN == 3)
                            {
                                map2[x, y] = 1;
                            }
                        }
                    }
                }
                map = map2;

                System.Threading.Thread.Sleep(1000);


            }




        }



        public static int HelloNeighbor(int x, int y)
        {
            int count = 0;
            for (int fy = (y - 1); fy <= (y + 1); fy++)
            {
                for (int fx = (x - 1); fx <= (x + 1); fx++)
                {
                    if (fx < 0 || fy < 0 || fx >= xSize || fy >= ySize || (fx == x && fy == y))
                    {
                        continue;
                    }
                    count = count + map[fx, fy];
                }
            }

            return count;
        }

        public void ShowMap()
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    try
                    {
                        if (map[x, y] == 1)
                        {
                            board[x, y].Style.BackColor = Color.White;
                        }
                        else
                        {
                            board[x, y].Style.BackColor = Color.Black;
                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {

                    }
                }
                Console.WriteLine();
            }
        }


        private void Generate()
        {
            board.BackgroundColor = Color.Black;
            board.DefaultCellStyle.BackColor = Color.Black;
            for (int i = 0; i < 30; i++)
            {
                board.Rows.Add();
            }


            foreach (DataGridViewColumn c in board.Columns)
            {
                c.Width = board.Width / board.Columns.Count;

            }

            foreach (DataGridViewRow r in board.Rows)
            {
                r.Height = board.Height / board.Rows.Count;

            }


            loaded = true;

        }
    }
}
