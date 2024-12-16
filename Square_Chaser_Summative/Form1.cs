using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

//Devon Subject, Mr. T, December 10th, 2024
//Square Chaser Game Engine
namespace Square_Chaser_Summative
{
    public partial class Form1 : Form
    {
        //Global Variables

        //Players and Squares
        Rectangle player1 = new Rectangle(10, 10, 20, 20);
        Rectangle player2 = new Rectangle(500, 10, 20, 20);
        Rectangle whiteSquare = new Rectangle(272, 204, 5, 5);
        Rectangle yellowSquare = new Rectangle(292, 204, 5, 5);

        //Score Variables
        int player1Score = 0;
        int player2Score = 0;

        //Speed Variables
        int player1Speed = 5;
        int player2Speed = 5;

        //Booleans
        bool wPressed = false;
        bool sPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        //Brushes
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        //Random Coordinates Generators
        Random coordinateRandomizer = new Random();
        

        //SoundPlayers
        SoundPlayer blipPlayer = new SoundPlayer(Properties.Resources.Blip);
        SoundPlayer whooshPlayer = new SoundPlayer(Properties.Resources.Whoosh);
        SoundPlayer winPlayer = new SoundPlayer(Properties.Resources.Win);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Player Score Labels
            p1ScoreLabel.Text = $"{player1Score}";
            p2ScoreLabel.Text = $"{player2Score}";

            //Players, Squares
            e.Graphics.FillRectangle(greenBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, whiteSquare);
            e.Graphics.FillRectangle(yellowBrush, yellowSquare);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //When Keys are Up
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //When Keys are Down/Pressed
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
            }
        }

        private void gametimer_Tick(object sender, EventArgs e)
        {
            //Game Mechanics

            //Player(s) Movements
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
            }
            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }
            if (aPressed == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }
            if (dPressed == true && player1.X < this.Width - player1.Width)
            {
                player1.X += player1Speed;
            }
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
            }
            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }
            if (leftPressed == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }
            if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2Speed;
            }

            //Collisions with Squares
            //Integers
            
            

            //Adding Points, Moving Squares
            if (player1.IntersectsWith(whiteSquare))
            {
                blipPlayer.Play();
                player1Score++;

                int whiteXCoordinate = coordinateRandomizer.Next(0, 563);
                int whiteYCoordinate = coordinateRandomizer.Next(0, 408);
                whiteSquare.X = whiteXCoordinate;
                whiteSquare.Y = whiteYCoordinate;
            }
            else if (player2.IntersectsWith(whiteSquare))
            {
                blipPlayer.Play();
                player2Score++;

                int whiteXCoordinate = coordinateRandomizer.Next(0, 563);
                int whiteYCoordinate = coordinateRandomizer.Next(0, 408);
                whiteSquare.X = whiteXCoordinate;
                whiteSquare.Y = whiteYCoordinate;
            }

            //Temporary Speed Increase, Moving Squares
            if (player1.IntersectsWith(yellowSquare))
            {
                whooshPlayer.Play();
                player1Speed = 15;

                int yellowXCoordinate = coordinateRandomizer.Next(0, 563);
                int yellowYCoordinate = coordinateRandomizer.Next(0, 408);
                yellowSquare.X = yellowXCoordinate;
                yellowSquare.Y = yellowYCoordinate;
                player1SpeedTimer.Enabled = true;

            }
            else if (player2.IntersectsWith(yellowSquare))
            {
                whooshPlayer.Play();
                player2Speed = 15;

                int yellowXCoordinate = coordinateRandomizer.Next(0, 563);
                int yellowYCoordinate = coordinateRandomizer.Next(0, 408);
                yellowSquare.X = yellowXCoordinate;
                yellowSquare.Y = yellowYCoordinate;
                player2SpeedTimer.Enabled = true;

            }

            //When Player(s) Score = 5 (Max)
            if (player1Score == 5)
            {
                winPlayer.Play();
                gametimer.Enabled = false;
                winnerLabel.Visible = true;
                winnerLabel.Text = "Player 1 Wins!";
                Refresh();

                Thread.Sleep(5000);
                Application.Exit();
            }
            else if (player2Score == 5)
            {
                winPlayer.Play();
                gametimer.Enabled = false;
                winnerLabel.Visible = true;
                winnerLabel.Text = "Player 2 Wins!";
                Refresh();

                Thread.Sleep(5000);
                Application.Exit();
            }

            Refresh();
        }

        private void player1SpeedTimer_Tick(object sender, EventArgs e)
        {
            //End Speed Boost
            player1SpeedTimer.Enabled = false;
            player1Speed = 5;
        }

        private void player2SpeedTimer_Tick(object sender, EventArgs e)
        {
            //End Speed Boost
            player2SpeedTimer.Enabled = false;
            player2Speed = 5;
        }
    }
}