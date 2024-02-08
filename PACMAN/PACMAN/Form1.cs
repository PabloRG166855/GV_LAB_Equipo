using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Text;
using System.Threading;
using System.IO;
using PACMAN;
using System.Reflection.Emit;

namespace PACMAN
{
    public partial class Form1 : Form
    {
        Bitmap bitmap1;
        Bitmap layer1, layer2, layer3, layer4;
        Bitmap bmp;
        bool right, hold = true;
        static Graphics g;
        Canvas canvas;
        int count = 0;
        int countLivesLeft = 3;
        int speedPacman = 5;
        byte[,] level = Mapa.map0;
        byte[,] leve2 = Mapa.map1;
        byte[,] leve3 = Mapa.map2;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void restart(byte[,] currentMap, byte[,] newMap)
        {
            for (int x = 0; x < currentMap.GetLength(1); x++)
            {
                for (int y = 0; y < currentMap.GetLength(1); y++)
                {
                    currentMap[y, x] = newMap[y, x];
                }
            }
            //Resetear SCORE y dibujar de nuevo el mapa

            DrawMap(currentMap);

        }

        private void activarComerGhost()
        {


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "comerFantasmas" && x.Visible == true)
                    {
                        if (pictureBox1.Bounds.IntersectsWith(x.Bounds))
                        {

                            ghostPink.BackColor = Color.FromArgb(35, 35, 36);
                            ghostOrange.BackColor = Color.FromArgb(35, 35, 36);
                            ghostRed.BackColor = Color.FromArgb(35, 35, 36);
                            ghostBlue.BackColor = Color.FromArgb(35, 35, 36);

                            ghostPink.Image = Resource1.deadGhost;
                            ghostRed.Image = Resource1.deadGhost;
                            ghostOrange.Image = Resource1.deadGhost;
                            ghostBlue.Image = Resource1.deadGhost;

                            x.Visible = false;

                        }

                    }
                }

            }
        }

        private void eliminarFantasmas()
        {
            foreach (Control p in this.Controls)
            {
                if (p is PictureBox)
                {
                    if ((string)p.Tag == "tagPhantom" && p.Visible == true && p.BackColor == Color.FromArgb(35, 35, 36))
                    {
                        if (pictureBox1.Bounds.IntersectsWith(p.Bounds))
                        {
                            p.Visible = false;

                        }

                    }
                }

            }
        }

        private void coinCount()
        {


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "iconMoneda" && x.Visible == true)
                    {
                        if (pictureBox1.Bounds.IntersectsWith(x.Bounds))
                        {

                            count += 1;
                            x.Visible = false;

                            labelCount.Text = count.ToString();


                        }


                    }
                }

            }

            /*
            int count = 0;
            int result = 0;

            if (pictureBox1.Bounds.IntersectsWith())
            {
                count++;
               labelCount.Text = count.ToString();
            }

            
            */
            //return result;
        }

        private void menosVidas()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "tagPhantom")
                    {
                        if (pictureBox1.Bounds.IntersectsWith(x.Bounds) && picBoxLive3.Visible == true)
                        {

                            picBoxLive3.Visible = false;

                        }
                        if (pictureBox1.Bounds.IntersectsWith(x.Bounds) && picBoxLive3.Visible == false && picBoxLive2.Visible == true)
                        {

                            picBoxLive2.Visible = false;

                        }
                        if (pictureBox1.Bounds.IntersectsWith(x.Bounds) && picBoxLive3.Visible == false && picBoxLive2.Visible == false)
                        {

                            picBoxLive1.Visible = false;

                        }

                    }
                }

            }
        }
        private void livesCount()
        {


            List<PictureBox> pictureBoxes = new List<PictureBox>
                {
                    ghostPink,
                    ghostOrange,
                    ghostBlue,
                    ghostRed
                };

            foreach (PictureBox pictureBox in pictureBoxes)
            {


                if (pictureBox1.Bounds.IntersectsWith(pictureBox.Bounds))
                {

                    quitarVida();

                }
            }

            /*                   if (pictureBox1.Bounds.IntersectsWith(ghostRed.Bounds))
                               {

                                   countLivesLeft -=1;
                                   picBoxLive3.Visible = false;


                               }

                               label4LivesLieft.Text = countLivesLeft.ToString();
                   */


        }

        private void quitarVida()
        {
            countLivesLeft--;
            label4LivesLieft.Text = countLivesLeft.ToString();

            List<PictureBox> pictureBoxesLives = new List<PictureBox>
                {
                picBoxLive1,
                    picBoxLive2,
                    picBoxLive3
                };

            foreach (PictureBox pictureBox in pictureBoxesLives)
            {
                pictureBox.Visible = false;

            }
        }

        private void countCereza()
        {
            if (pictureBox1.Bounds.IntersectsWith(pictureBox97Cereza.Bounds) && pictureBox97Cereza.Visible == true)
            {
                count += 100;
                pictureBox97Cereza.Visible = false;
            }

        }

        private void LoadImages()
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>();


            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    pictureBoxes.Add((PictureBox)control);
                    if ((string)control.Tag == "iconMoneda")
                    {
                        control.BackgroundImage = Resource1.coin;
                        control.BackgroundImageLayout = ImageLayout.Zoom;
                        control.BackColor = Color.FromArgb(35, 35, 35);
                        control.Size = new Size(15, 15);
                    }

                }
            }
        }


        /*
        private void LoadImages()
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>
                {
                

                };


            foreach (PictureBox pictureBox in pictureBoxes)
            {
                
                if (pictureBox is PictureBox)
                {
                    if ((string)pictureBox.Tag == "iconMoneda")
                    {
                        pictureBox.Image = Resource1.coin;
                    }
                }
            }
        }
        */
        public Form1()
        {
            InitializeComponent();
            //pictureBox1.Image = Resource1.pacman;
            bmp = new Bitmap(350, 350);
            g = Graphics.FromImage(bmp);
            PCT_CANVAS.Image = bmp;
            canvas = new Canvas(bmp);



            //DrawMap();
            LoadImages();
            pictureBox97Cereza.BackColor = Color.FromArgb(35, 35, 35);

            ghostPink.BackColor = Color.FromArgb(35, 35, 35);
            ghostBlue.BackColor = Color.FromArgb(35, 35, 35);
            ghostRed.BackColor = Color.FromArgb(35, 35, 35);
            ghostOrange.BackColor = Color.FromArgb(35, 35, 35);

            ghostPink.Image = Resource1.pink_guy;
            ghostRed.Image = Resource1.red_guy;
            ghostBlue.Image = Resource1.blue_guy;
            ghostOrange.Image = Resource1.yellow_guy;

            //.Image = Resource1.coin;
            /* byte[,] arregloB = new byte[,] { {0,0}, {0,0} };

             byte capacity = Mapa.map0[10,10];

             var image = new Bitmap(new MemoryStream(10));

             pictureBox1.Image = image;

             if (pictureBox1.ClientRectangle.IntersectsWith(new Rectangle(0, 0, 10, 10)))
             {
                 pictureBox1.BackColor = Color.FromArgb(55, 55, 55);
             }
            */
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13Dead_Click(object sender, EventArgs e)
        {

        }

        private void mostrarGanaste()
        {

            if (pictureBox1.Bounds.IntersectsWith(pictureBoxActivaWin.Bounds))
            {
                labelWon.Visible = true;
            }



        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            int x = pictureBox1.Location.X / 10;
            int y = pictureBox1.Location.Y / 10;


            switch (keyData)
            {

                case Keys.Left:





                    // if (Mapa.map0[--x, y] == 1)
                    //{

                    //pictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    pictureBox1.Left -= speedPacman;
                    pictureBox1.Image = Resource1.left;

                    if (pacmInterAzul() == true)
                    {
                        pictureBox1.Left += speedPacman;
                    }
                    //}

                    break;

                case Keys.Right:

                    // if (Mapa.map0[++x, y] == 1)
                    //{
                    pictureBox1.Left += speedPacman;
                    pictureBox1.Image = Resource1.right;
                    //}

                    if (pacmInterAzul() == true)
                    {
                        pictureBox1.Left -= speedPacman;
                    }

                    break;

                case Keys.Up:

                    //if (Mapa.map0[x, ++y] == 1)
                    //{
                    pictureBox1.Top -= speedPacman;
                    pictureBox1.Image = Resource1.Up;
                    //}

                    if (pacmInterAzul() == true)
                    {
                        pictureBox1.Top += speedPacman;
                    }


                    break;

                case Keys.Down:
                    // if (Mapa.map0[x, --y] == 1)
                    //{
                    pictureBox1.Top += speedPacman;
                    pictureBox1.Image = Resource1.down;
                    //}

                    if (pacmInterAzul() == true)
                    {
                        pictureBox1.Top -= speedPacman;
                    }

                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DrawMap(byte[,] map)
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[y, x] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(35, 35, 35)), x * 10, y * 10, 10, 10);
                        //int punto1 = Mapa.map0[x, y] = 1;

                        //Console.WriteLine(punto1);
                        //moneda.Image = Resource1.coin;
                        pictureBox1.BackColor = Color.FromArgb(55, 55, 55);
                    }
                    else if (map[y, x] == 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(39, 39, 225)), x * 10, y * 10, 10, 10);

                        //g.DrawImage(moneda.Image = Resource1.coin, y, x, 20, 20);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(222, 0, 0)), x * 10, y * 10, 10, 10);
                        // g.DrawRectangle(Pens.Red, x * 10, y * 10, 10, 10);

                    }

                }

            }
            PCT_CANVAS.Invalidate();

        }

        private void PCT_CANVAS_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {



        }

        private void buttonNivel2_Click(object sender, EventArgs e)
        {
            pictureBox17.Visible = true;

            restart(level, Mapa.map1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            restart(level, Mapa.map2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            restart(level, Mapa.map0);
        }

        /*
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Right & hold)
            {
                right = true;
                hold = false;
                
                pictureBox1.Image = Resource1.pacman;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Right & !hold)
            {
                right = false;
                hold = true;
                ghostRed.Image = Resource1.ghost1;
            }
        }
        */

        private void pacmanIntersectaRojo()
        {
            if (pictureBox1.Bounds.IntersectsWith(ghostRed.Bounds) && ghostRed.BackColor == Color.FromArgb(35, 35, 35))
            {
                pictureBox1.Image = Resource1.dead;
                countLivesLeft = countLivesLeft - 1;
                //pictureBox13Dead.Visible= true;
                label1.Visible = true;

            }
            else
            {
                label1.Visible = false;
            }
        }

        private Boolean pacmInterAzul()
        {

            bool choco = false;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "pictBoxAzul")
                    {
                        x.BackColor = Color.FromArgb(0, 0, 0);
                        if (pictureBox1.Bounds.IntersectsWith(x.Bounds))
                        {

                            //pictureBox1.BackColor = Color.FromArgb(35, 35, 35);
                            choco = true;
                        }
                    }
                }
            }
            return choco;
        }

        private void pacmanIntersecta()
        {


            if (pictureBox1.Bounds.IntersectsWith(pictureBox58ActivaCereza.Bounds) && pictureBox58ActivaCereza.Visible == true)
            {
                pictureBox97Cereza.Visible = true;
            }

            if (pictureBox1.Bounds.IntersectsWith(pictureBox13ActiveCereza2.Bounds) && pictureBox13ActiveCereza2.Visible == true)
            {
                pictureBox97Cereza.Visible = true;
            }

            if (pictureBox1.Bounds.IntersectsWith(pictureBox91ActiveCereza3.Bounds) && pictureBox91ActiveCereza3.Visible == true)
            {
                pictureBox97Cereza.Visible = true;
            }

            if (pictureBox1.Bounds.IntersectsWith(pictureBox66ActiveCereza4.Bounds) && pictureBox66ActiveCereza4.Visible == true)
            {
                pictureBox97Cereza.Visible = true;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;

            //x = x + 10;

            if (x >= 480) //derecha y sale izquierda
            {
                if (y == 190)
                {
                    x = 150;
                }

            }

            if (x <= 130)  //izquierda y sale derecha
            {
                if (y >= 189)
                {
                    x = 480;
                }
            }

            if (x > 300 && x < 323)  //arriba y sale abajo
            {
                if (y <= 50)
                {
                    y = 370;
                }
            }

            if (x > 300 && x < 323)  //abajo y sale arriba
            {
                if (y >= 390)
                {
                    y = 50;
                }
            }

            pacmanIntersecta();
            pacmanIntersectaRojo();
            //labelCount.Text = coinCount().ToString();
            coinCount();
            countCereza();
            //livesCount();
            activarComerGhost();
            menosVidas();
            eliminarFantasmas();
            pacmInterAzul();
            mostrarGanaste();

            Point punto = new Point(x, y);
            pictureBox1.Location = punto;


        }
    }
}
