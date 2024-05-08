using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Reflection.Emit;

namespace TicTacToeGame
{
    public partial class GameBoard : Form
    {

        private char PlayerChar;
        private char OpponentChar;
        private Socket socket;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;
        private static bool playerTurn = false;


        public GameBoard(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;
            btnRESET.Enabled = false;
            try
            {
                if (isHost)
                {
                    PlayerChar = 'X';
                    OpponentChar = 'O';
                    server = new TcpListener(System.Net.IPAddress.Any, 8080);
                    server.Start();
                    socket = server.AcceptSocket();
                }
                else
                {
                    if (ip == null)
                    {
                        MessageBox.Show("IP address cannot be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    PlayerChar = 'O';
                    OpponentChar = 'X';
                    client = new TcpClient();
                    client.Connect(ip, 8080);
                    socket = client.Client;
                    MessageReceiver.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }


        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            if (CheckState())
            {
                return;
            }
            FreezeBoard();
            lblTurn.Text = "Opponent's Turn!";
            ReceiveMove();
            lblTurn.Text = "Your Turn!";
            if (!CheckState())
            {
                UnfreezeBoard();
            }
        }


        private bool CheckState()
        {
            playerTurn = !playerTurn;
            if (CheckHorizontal() || CheckVertical() || CheckDiagonal())
            {
                if (playerTurn == true) 
                {
                    FreezeBoard();
                    MessageBox.Show($"The winner is {PlayerChar} "); 
                }
                else 
                {
                    FreezeBoard(); 
                    MessageBox.Show($"The winner is {OpponentChar} "); 
                }
                btnRESET.Enabled = true;
                return true;
            }
            else if (CheckDraw())
            {
                return true;
            }
            return false;
        }


        private bool CheckHorizontal()
        {
            if ((btn1.Text == btn2.Text && btn2.Text == btn3.Text && btn3.Text != "") ||
                (btn4.Text == btn5.Text && btn5.Text == btn6.Text && btn6.Text != "") ||
                (btn7.Text == btn8.Text && btn8.Text == btn9.Text && btn9.Text != ""))
            {
                return true;
            }
            return false;
        }

        private bool CheckVertical()
        {
            if ((btn1.Text == btn4.Text && btn4.Text == btn7.Text && btn7.Text != "") ||
                (btn2.Text == btn5.Text && btn5.Text == btn8.Text && btn8.Text != "") ||
                (btn3.Text == btn6.Text && btn6.Text == btn9.Text && btn9.Text != ""))
            {
                return true;
            }
            return false;
        }


        private bool CheckDiagonal()
        {
            if ((btn1.Text == btn5.Text && btn5.Text == btn9.Text && btn9.Text != "") ||
                (btn3.Text == btn5.Text && btn5.Text == btn7.Text && btn7.Text != ""))
            {
                return true;
            }
            return false;
        }


        private bool CheckDraw()
        {
            if (btn1.Text != "" && btn2.Text != "" && btn3.Text != "" &&
                btn4.Text != "" && btn5.Text != "" && btn6.Text != "" &&
                btn7.Text != "" && btn8.Text != "" && btn9.Text != "")
            {
                MessageBox.Show("It's draw!", "No winners", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
        }


        public void UnfreezeBoard()
        {
            foreach (Control control in Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = true;
                    if(btn.Text != "")
                    {
                        btn.Enabled = false;
                    }
                }
            }
        }


        public void FreezeBoard()
        {
            foreach (Control control in Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = false;

                }
            }
        }


        private void btn1_Click(object sender, EventArgs e)
        {
            if (btn1.Text == "")
            {
                byte[] num = { 1 };
                socket.Send(num);
                btn1.Text = PlayerChar.ToString();
                btn1.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (btn2.Text == "")
            {
                byte[] num = { 2 };
                socket.Send(num);
                btn2.Text = PlayerChar.ToString();
                btn2.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (btn3.Text == "")
            {
                byte[] num = { 3 };
                socket.Send(num);
                btn3.Text = PlayerChar.ToString();
                btn3.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (btn4.Text == "")
            {
                byte[] num = { 4 };
                socket.Send(num);
                btn4.Text = PlayerChar.ToString();
                btn4.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (btn5.Text == "")
            {
                byte[] num = { 5 };
                socket.Send(num);
                btn5.Text = PlayerChar.ToString();
                btn5.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (btn6.Text == "")
            {
                byte[] num = { 6 };
                socket.Send(num);
                btn6.Text = PlayerChar.ToString();
                btn6.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (btn7.Text == "")
            {
                byte[] num = { 7 };
                socket.Send(num);
                btn7.Text = PlayerChar.ToString();
                btn7.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (btn8.Text == "")
            {
                byte[] num = { 8 };
                socket.Send(num);
                btn8.Text = PlayerChar.ToString();
                btn8.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (btn9.Text == "")
            {
                byte[] num = { 9 };
                socket.Send(num);
                btn9.Text = PlayerChar.ToString();
                btn9.Enabled = false;
                MessageReceiver.RunWorkerAsync();
            }
        }

        private void ReceiveMove()
        {
            byte[] buffer = new byte[1];
            socket.Receive(buffer);
            int index = buffer[0];
            UpdateButton(index);
            UnfreezeBoard();
        }

        private void UpdateButton(int index)
        {
            Control control = Controls.Find($"btn{index}", true).FirstOrDefault();
            if (control is Button btn)
            {
                btn.Text = OpponentChar.ToString();
                btn.Enabled = false;
            }
        }

        private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageReceiver.WorkerSupportsCancellation = true;
            MessageReceiver.CancelAsync();
            if (server != null)
            {
                server.Stop();
            }
        }

        private void btnRESET_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is Button btn)
                {
                    if (btn.Text == "Reset")
                    {
                        btnRESET.Enabled = false;
                    }
                    else
                    {
                        btn.Enabled = true;
                        btn.Text = "";
                    }
                }
            }

            UnfreezeBoard();
            lblTurn.Text = "Your Turn!";
            MessageReceiver.RunWorkerAsync();

        }
    }
}
