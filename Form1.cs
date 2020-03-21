using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPongForm
{
    public partial class GameForm : Form
    {
        public int speed_left = 4;
        public int speed_top = 4;
        public int points = 0;

        public GameForm()
        {
            InitializeComponent();


            timer1.Enabled = true;
            Cursor.Hide();

            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            //sets raquet position
            racket.Top = playGround.Bottom - (playGround.Bottom / 10);

            //sets position of gameOver label
            gameOver_label.Left = (playGround.Width / 2) - (gameOver_label.Width / 2);
            gameOver_label.Top = (playGround.Height / 2) - (gameOver_label.Height / 2);

            //hides gameOver label when the game starts
            gameOver_label.Visible = false;
        }
        
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            //sets position of raquet to match the cursor's
            racket.Left = Cursor.Position.X - (racket.Width / 2);

            //handles ball movement
            ball.Left += speed_left;
            ball.Top += speed_top;

            //handles collosion of ball with racket
            if (ball.Bottom >= racket.Top && ball.Bottom <= racket.Bottom && ball.Left >= racket.Left && ball.Right <= racket.Right)
            {
                speed_top += 2; //increases speed
                speed_left += 2;
                speed_top = -speed_top; //changes direction of ball.
                points += 1; //one point increase when racket strikes ball
                points_label.Text = points.ToString(); //points matches text in label

                //changes the color whenever a point is gained
                Random r = new Random();
                playGround.BackColor = Color.FromArgb(r.Next(150, 255), r.Next(150, 255), r.Next(150, 255));
            }

            if (ball.Left <= playGround.Left)
            {
                speed_left = -speed_left;
            }

            if (ball.Right >= playGround.Right)
            {
                speed_left = -speed_left;
            }
            if (ball.Top <= playGround.Top)
            {
                speed_top = -speed_top;
            }
            if (ball.Bottom >= playGround.Left)
            {
                // this means the ball is out of bounds
                timer1.Enabled = false;
                gameOver_label.Visible = true;
            }
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); } //escape key closes the game

            if (e.KeyCode == Keys.F1) //F1 reloads the game
            {
                ball.Top = 50;
                ball.Left = 50;
                speed_left = 4;
                speed_top = 4;
                points_label.Text = "0";
                timer1.Enabled = true; //timer restarts
                gameOver_label.Visible = false;
                playGround.BackColor = Color.White; //color is reset to white
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
