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


        public GameBoard(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;

            if (isHost)
            {
                PlayerChar = 'X';
                OpponentChar = 'O';
                server = new TcpListener(System.Net.IPAddress.Any, 5732);
                server.Start();
                socket = server.AcceptSocket();
            }
            else
            {
                PlayerChar = 'O';
                OpponentChar = 'X';
                try
                {
                    client = new TcpClient(ip, 5732);
                    socket = client.Client;
                    MessageReceiver.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
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
            lblTurn.Text = "Your Trun!";
            if (!CheckState())
            {
                UnfreezeBoard();
            }
        }


        private bool CheckState()
        {
            if (CheckHorizontal() || CheckVertical() || CheckDiagonal() || CheckDraw())
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
                MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
        }


        private bool CheckDiagonal()
        {
            if ((btn1.Text == btn5.Text && btn5.Text == btn9.Text && btn9.Text != "") ||
                (btn3.Text == btn5.Text && btn5.Text == btn7.Text && btn7.Text != ""))
            {
                MessageBox.Show("You won!", "Well played", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    btn.Text = "";
                    btn.Enabled = true;
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
            byte[] num = { 1 };
            socket.Send(num);
            btn1.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            byte[] num = { 2 };
            socket.Send(num);
            btn2.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            byte[] num = { 3 };
            socket.Send(num);
            btn3.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            byte[] num = { 4 };
            socket.Send(num);
            btn4.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            byte[] num = { 5 };
            socket.Send(num);
            btn5.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            byte[] num = { 6 };
            socket.Send(num);
            btn6.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            byte[] num = { 7 };
            socket.Send(num);
            btn7.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            byte[] num = { 8 };
            socket.Send(num);
            btn8.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            byte[] num = { 9 };
            socket.Send(num);
            btn9.Text = PlayerChar.ToString();
            MessageReceiver.RunWorkerAsync();
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


        private void GameBoard_Load(object sender, EventArgs e)
        {

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
    }
}
