using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using VerifyUsers;

namespace Instagram_Data_Grabber
{
    public partial class LoginForm : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "Username")
            {
                MessageBox.Show("Please, Enter your Username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            else if (txtPass.Text == "Password")
            {
                MessageBox.Show("Please, Enter your Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                try
                {
                    Registration rg = new Registration();
                    rg.CheckInfo(txtUser, txtPass);

                    if (rg.Report == "Active")
                    {
                        var mf = new MainForm();
                        mf.Show();
                        this.Hide();
                    }

                    else if (rg.Report == "Deactive")
                    {
                        MessageBox.Show("Your license is expired!!", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else
                    {
                        MessageBox.Show("Username or Password is incorrect.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("Please check your connection!", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "Username")
            {
                txtUser.Text = "";
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "Username";
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Password")
            {
                txtPass.Text = "";
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Password";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = btnExit;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.softkini.com/contact/");
        }
        
    }
}
