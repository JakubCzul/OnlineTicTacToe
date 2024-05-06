using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class ConnectWindow : Form
    {
        public ConnectWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            GameBoard newGame = new GameBoard(false, txtIP.Text);
            
            Visible = false;
            if (!newGame.IsDisposed)
            {
                newGame.ShowDialog();
            }
            Visible = true;
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            GameBoard newGame = new GameBoard(true);
            Visible = false;
            if (!newGame.IsDisposed) 
            {
                newGame.ShowDialog();
            }
            Visible = true;
        }
    }
}
