using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Instagram_Data_Grabber
{
    class ExtrctLink
    {
        public void ExtractLink(ListBox listBox1, Label label5, ListView ExtLink, Button button11)
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


                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                //options1.AddArgument("--headless");
                options.AddArgument("no-sandbox");
                //options1.AddArgument("--user-agent=Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                var driver = new ChromeDriver(service, options);

                int count = listBox1.Items.Count;
                for (int i = 0; i <= count; i++)
                {
                    try
                    {
                        driver.Navigate().GoToUrl(listBox1.Text);
                        var name = driver.FindElement(By.ClassName("e1e1d")).Text;
                        ExtLink.Items.Add(name);
                        label5.Text = i.ToString();
                    }
                    catch 
                    {
                        //
                    }

                    //Change URL
                    if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                    {
                        listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    }

                }

                driver.Quit();
                button11.Text = "RUN";
                button11.BackColor = Color.FromArgb(92, 181, 96);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
