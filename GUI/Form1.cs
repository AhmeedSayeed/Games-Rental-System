using GamesHub;
using GameRentalSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesHub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 signup = new Form2();
            signup.Show();   
            this.Hide();    
        }


        private void Form1_Load(object sender, EventArgs e)
        { }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();   
            this.Hide();    
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
