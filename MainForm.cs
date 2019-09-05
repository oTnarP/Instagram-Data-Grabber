using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Instagram_Data_Grabber
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        Thread task1;
        Thread task2;
        Thread task3;
        Thread task4;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            pLinks.Columns[0].Width = Width - 4 - SystemInformation.VerticalScrollBarWidth;
            ExtLink.Columns[0].Width = Width - 4 - SystemInformation.VerticalScrollBarWidth;
            button6.BackColor = Color.FromArgb(92, 181, 96);
            button7.BackColor = Color.FromArgb(252, 120, 103);
            button9.BackColor = Color.FromArgb(92, 181, 96);
            button11.BackColor = Color.FromArgb(92, 181, 96);
            button14.BackColor = Color.FromArgb(92, 181, 96);
            button17.BackColor = Color.FromArgb(92, 181, 96);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;

            IGDV.Visible = true;
            pLinks.Visible = false;
            ExtLink.Visible = false;
            hashTagDG.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

            IGDV.Visible = false;
            pLinks.Visible = true;
            ExtLink.Visible = false;
            hashTagDG.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;

            IGDV.Visible = false;
            pLinks.Visible = false;
            ExtLink.Visible = true;
            hashTagDG.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            hashTagDG.Visible = true;
            IGDV.Visible = false;
            pLinks.Visible = false;
            ExtLink.Visible = false;

        }

        private void button6_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

            if (txtSearchKey.Text == "" || txtCount.Text == "")
            {
                MessageBox.Show("Please fill all details.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (button9.Text == "RUN")
                {
                    try
                    {

                        PostLink pl = new PostLink();

                        task1 = new Thread(() => pl.PostLnk(pLinks, txtSearchKey, label4, txtCount, checkBox1, checkBox2, button9)) { IsBackground = true };

                        task1.Start();
                        button9.Text = "STOP";
                        button9.BackColor = Color.FromArgb(252, 120, 103);
                    }
                    catch (NoSuchElementException)
                    {
                        MessageBox.Show("Please check your internet connection.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }

                else
                {
                    task1.Abort();
                    button9.Text = "RUN";
                    button9.BackColor = Color.FromArgb(92, 181, 96);
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            button6.Visible = false;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            checkBox1.Checked = false;
            button6.Visible = true;
            button7.Visible = false;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    sfd.FileName = "Post Links";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (TextWriter tw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                        {
                            foreach (ListViewItem item in pLinks.Items)
                            {
                                await tw.WriteLineAsync(item.SubItems[0].Text + "\t");
                            }

                            MessageBox.Show("Post Links have been saved successfully.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    sfd.FileName = "UserName List";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (TextWriter tw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                        {
                            foreach (ListViewItem item in ExtLink.Items)
                            {
                                await tw.WriteLineAsync(item.SubItems[0].Text + "\t");
                            }

                            MessageBox.Show("Extracted Links have been saved successfully.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save as excel file";
                sfd.FileName = "Users Data";
                sfd.Filter = "Excel Document|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wn = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)Excel.ActiveSheet;
                    Excel.Visible = true;
                    Excel.Columns.ColumnWidth = 20;

                    ws.Cells[1, 1] = "Username";
                    ws.Cells[1, 2] = "Followers";
                    ws.Cells[1, 3] = "Following";
                    ws.Cells[1, 4] = "Uploads";
                    ws.Cells[1, 5] = "Bio";
                    ws.Cells[1, 6] = "Website";
                    ws.Cells[1, 7] = "Verified";
                    ws.Cells[1, 8] = "Private";

                    for (int j = 0; j < IGDV.RowCount - 1; j++)
                    {
                        for (int i = 0; i <= IGDV.ColumnCount - 1; i++)
                        {
                            ws.Cells[j + 2, i + 1] = IGDV.Rows[j].Cells[i].Value.ToString();
                        }
                    }

                    MessageBox.Show("User Details have been saved successfully.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Excel.Quit();
                }
            }

            else if(tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save as excel file";
                sfd.FileName = "Hashtags Data";
                sfd.Filter = "Excel Document|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wn = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)Excel.ActiveSheet;
                    Excel.Visible = true;
                    Excel.Columns.ColumnWidth = 20;

                    ws.Cells[1, 1] = "Hashtag List";
                    ws.Cells[1, 2] = "Total Post";

                    for (int j = 0; j < hashTagDG.RowCount - 1; j++)
                    {
                        for (int i = 0; i <= hashTagDG.ColumnCount - 1; i++)
                        {
                            ws.Cells[j + 2, i + 1] = hashTagDG.Rows[j].Cells[i].Value.ToString();
                        }
                    }

                    MessageBox.Show("Hashtags Details have been saved successfully.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    Excel.Quit();
                }
            }

            else
            {
                //
            }
        }
        

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();

            f.Title = "Browse Text Files";
            f.Filter = "txt files (*.txt)|*.txt";

            if (f.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();

                List<string> lines = new List<string>();
                using (StreamReader r = new StreamReader(f.OpenFile()))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        listBox1.SelectedIndex = 0;
                        label9.Text = f.SafeFileName;
                        label7.Text = listBox1.Items.Count.ToString();

                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (label9.Text == "No File Selected")
            {
                MessageBox.Show("Please browse the Post Links", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (button11.Text == "RUN")
                {
                    ExtrctLink el = new ExtrctLink();
                    task2 = new Thread(() => el.ExtractLink(listBox1, label5, ExtLink, button11)) { IsBackground = true };
                    task2.Start();
                    button11.Text = "STOP";
                    button11.BackColor = Color.FromArgb(252, 120, 103);
                }

                else
                {
                    try
                    {
                        task2.Abort();
                        textBox2.Text = "";
                        button11.Text = "RUN";
                        button11.BackColor = Color.FromArgb(92, 181, 96);
                    }
                    catch
                    {
                        //
                    }
                }
            }
            

        }
        private void button15_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();

            f.Title = "Browse Text Files";
            f.Filter = "txt files (*.txt)|*.txt";

            if (f.ShowDialog() == DialogResult.OK)
            {
                listBox2.Items.Clear();

                List<string> lines = new List<string>();
                using (StreamReader r = new StreamReader(f.OpenFile()))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        listBox2.Items.Add(line);
                        listBox2.SelectedIndex = 0;
                        label13.Text = f.SafeFileName;
                        label11.Text = listBox2.Items.Count.ToString();

                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (label13.Text == "No File Selected")
            {
                MessageBox.Show("Please browse the IG usernames.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (button14.Text == "RUN")
                {
                    IgDataGrabber igd = new IgDataGrabber();

                    task3 = new Thread(() => igd.IGDGrab(listBox2, IGDV, label14, button14)) { IsBackground = true };
                    task3.Start();
                    button14.Text = "STOP";
                    button14.BackColor = Color.FromArgb(252, 120, 103);
                }

                else
                {
                    task3.Abort();
                    button14.Text = "RUN";
                    textBox4.Text = "";
                    button14.BackColor = Color.FromArgb(92, 181, 96);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (IGDV.Visible == true)
            {
                IGDV.Rows.Clear();
                IGDV.Refresh();
            }

            if(ExtLink.Visible == true)
            {
                ExtLink.Items.Clear();
            }

            if (pLinks.Visible == true)
            {
                pLinks.Items.Clear();
            }

            if(hashTagDG.Visible == true)
            {
                hashTagDG.Rows.Clear();
                hashTagDG.Refresh();
            }

            label13.Text = "No File Selected";
            txtSearchKey.Text = "";
            txtCount.Text = "";
            label9.Text = "No File Selected";
            label7.Text = "0";
            label5.Text = "0";
            label4.Text = "0";
            label20.Text = "No File Selected";
            label18.Text = "0";
            label21.Text = "0";
            label11.Text = "0";
            label14.Text = "0";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();

            f.Title = "Browse Text Files";
            f.Filter = "txt files (*.txt)|*.txt";

            if (f.ShowDialog() == DialogResult.OK)
            {
                listBox3.Items.Clear();

                List<string> lines = new List<string>();
                using (StreamReader r = new StreamReader(f.OpenFile()))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        listBox3.Items.Add(line);
                        listBox3.SelectedIndex = 0;
                        label20.Text = f.SafeFileName;
                        label18.Text = listBox3.Items.Count.ToString();

                    }
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (label20.Text == "No File Selected")
            {
                MessageBox.Show("Please browse the Hashtags.", "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (button17.Text == "RUN")
                {
                    HashTagExp ht = new HashTagExp();
                    task4 = new Thread(() => ht.Hashtex(listBox3, hashTagDG, label21, button17)) { IsBackground = true };
                    task4.Start();
                    button17.Text = "STOP";
                    button17.BackColor = Color.FromArgb(252, 120, 103);
                }

                else
                {
                    task4.Abort();
                    textBox1.Text = "";
                    button17.Text = "RUN";
                    button17.BackColor = Color.FromArgb(92, 181, 96);
                }
            }
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.softkini.com");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
