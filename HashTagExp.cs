using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Instagram_Data_Grabber
{
    class HashTagExp
    {
        public void Hashtex(ListBox listBox3, DataGridView hashTagDG, Label label21, Button button17)
        {
            ListBox.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
            CheckBox.CheckForIllegalCrossThreadCalls = false;
            ComboBox.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            ListView.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;


            try
            {


                ChromeDriverService service1 = ChromeDriverService.CreateDefaultService();
                service1.HideCommandPromptWindow = true;

                var options1 = new ChromeOptions();
                options1.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                //options1.AddArgument("--headless");
                options1.AddArgument("no-sandbox");
                //options1.AddArgument("--user-agent=Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                var driver = new ChromeDriver(service1, options1);

                int count = listBox3.Items.Count;

                for (int i = 0; i <= count; i++)
                {
                    driver.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" + listBox3.Text);



                    int n = hashTagDG.Rows.Add();


                    //Username
                    hashTagDG.Rows[n].Cells[0].Value = listBox3.Text;


                    try
                    {
                        //followers
                        string postCount = driver.FindElement(By.ClassName("g47SY")).Text;
                        hashTagDG.Rows[n].Cells[1].Value = postCount;
                    }

                    catch
                    {
                        hashTagDG.Rows[n].Cells[1].Value = "Not Found";
                    }



                    label21.Text = i.ToString();

                    //Change URL
                    if (listBox3.SelectedIndex < listBox3.Items.Count - 1)
                    {
                        listBox3.SelectedIndex = listBox3.SelectedIndex + 1;
                    }


                }

                button17.Text = "RUN";
                button17.BackColor = Color.FromArgb(92, 181, 96);
                driver.Quit();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
