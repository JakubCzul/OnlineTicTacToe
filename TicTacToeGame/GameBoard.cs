using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TicTacToeGame
{
    public partial class GameBoard : Form
    {
        public GameBoard(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageRecevier.DoWork += MessageRecevier_DoWork;
            CheckForIllegalCrossThreadCalls = false;

            if (isHost)
            {
                PlayerChar = 'X';
                OpponentChar = 'O';
                server = new TcpListener(System.Net.IPAddress.Any, 0);
                server.Start();
                socket = server.AcceptSocket();
            }
            else 
            {
                PlayerChar = 'O';
                OpponentChar = 'X';
                try 
                {
                    client = new TcpClient(ip, 0);
                    socket = client.Client;
                    MessageRecevier.RunWorkerAsync();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void MessageRecevier_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        String[] gameBoard = new string[9];
        int currentTurn = 0;
        private char PlayerChar;
        private char OpponentChar;
        private Socket socket;
        private BackgroundWorker MessageRecevier = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;

        public String returnSymbol(int turn)
        {
            if (turn % 2 == 0)
            {
                return "O";
            }
            else
            {
                return "X";
            }
        }


        private bool CheckState()
        {
            if (btn1.Text == btn2.Text && btn2.Text == btn3.Text && btn3.Text != "")
            {
                if (btn1.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }
            if (btn4.Text == btn5.Text && btn5.Text == btn6.Text && btn6.Text != "")
            {
                if (btn4.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }
            if (btn7.Text == btn8.Text && btn8.Text == btn9.Text && btn9.Text != "")
            {
                if (btn7.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }

            if (btn1.Text == btn4.Text && btn4.Text == btn7.Text && btn7.Text != "")
            {
                if (btn1.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }
            if (btn2.Text == btn5.Text && btn5.Text == btn8.Text && btn8.Text != "")
            {
                if (btn2.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }
            if (btn3.Text == btn6.Text && btn6.Text == btn9.Text && btn9.Text != "")
            {
                if (btn3.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }

            if (btn1.Text == btn5.Text && btn5.Text == btn9.Text && btn9.Text != "")
            {
                if (btn1.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }
            if (btn3.Text == btn5.Text && btn5.Text == btn7.Text && btn7.Text != "")
            {
                if (btn3.Text[0] == PlayerChar)
                {
                    MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You lost!", "Good luck next time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }
            return false;
        }


        public bool CheckButtons()
        {
            
        }


        public void checkForWinner()
        {
            for (int i = 0; i < 8; i++)
            {
                String combination = "";

                switch(i)
                {
                    case 0:
                        combination = gameBoard[0] + gameBoard[4] + gameBoard[8];
                        break;
                    case 1:
                        combination = gameBoard[2] + gameBoard[4] + gameBoard[6];
                        break;
                    case 2:
                        combination = gameBoard[0] + gameBoard[1] + gameBoard[2];
                        break;
                    case 3:
                        combination = gameBoard[3] + gameBoard[4] + gameBoard[5];
                        break;
                    case 4:
                        combination = gameBoard[6] + gameBoard[7] + gameBoard[8];
                        break;
                    case 5:
                        combination = gameBoard[0] + gameBoard[3] + gameBoard[6];
                        break;
                    case 6:
                        combination = gameBoard[1] + gameBoard[4] + gameBoard[7];
                        break;
                    case 7:
                        combination = gameBoard[2] + gameBoard[5] + gameBoard[8];
                        break;
                }

                if (combination.Equals("OOO"))
                {
                    MessageBox.Show("O has won the game!","We have a winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } 
                else if (combination.Equals("XXX"))
                {
                    MessageBox.Show("X has won the game!", "We have a winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                checkDraw();
            }
        }

        public void reset()
        {
            foreach (Control control in Controls)
            {
                if (control is Button btn)
                {
                    btn.Text = "";
                    btn.Enabled = true;
                }
            }
            gameBoard = new string[9];
            currentTurn = 0;
        }

        public void checkDraw()
        {
            int counter = 0;
            for (int i = 0; i < gameBoard.Length; i++)
            {
                if (gameBoard[i] != null)
                {
                    counter++;
                }

                if (counter == 9)
                {
                    MessageBox.Show("It's draw!", "No winner in this match", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }


        private void btn1_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[0] = returnSymbol(currentTurn);
            btn1.Text= gameBoard[0];
            btn1.Enabled= false;
            checkForWinner();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[1] = returnSymbol(currentTurn);
            btn2.Text = gameBoard[1];
            btn2.Enabled = false;
            checkForWinner();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[2] = returnSymbol(currentTurn);
            btn3.Text = gameBoard[2];
            btn3.Enabled = false;
            checkForWinner();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[3] = returnSymbol(currentTurn);
            btn4.Text = gameBoard[3];
            btn4.Enabled = false;
            checkForWinner();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[4] = returnSymbol(currentTurn);
            btn5.Text = gameBoard[4];
            btn5.Enabled = false;
            checkForWinner();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[5] = returnSymbol(currentTurn);
            btn6.Text = gameBoard[5];
            btn6.Enabled = false;
            checkForWinner();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[6] = returnSymbol(currentTurn);
            btn7.Text = gameBoard[6];
            btn7.Enabled = false;
            checkForWinner();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[7] = returnSymbol(currentTurn);
            btn8.Text = gameBoard[7];
            btn8.Enabled = false;
            checkForWinner();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            currentTurn++;
            gameBoard[8] = returnSymbol(currentTurn);
            btn9.Text = gameBoard[8];
            btn9.Enabled = false;
            checkForWinner();
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void ReceiveMove()
        {
            byte[] buffer = new byte[1];
            socket.Receive(buffer);
            int index = 0;
            foreach (Control control in Controls)
            {
                if (control is Button btn && index < 9)
                {
                    if (buffer[index] == 1)
                    {
                        btn.Text = OpponentChar.ToString();
                    }
                    index++;
                }
            }

        }
    }
}
