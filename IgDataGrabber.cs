using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Instagram_Data_Grabber
{
    class IgDataGrabber
    {
        public void IGDGrab(ListBox listBox2, DataGridView IGDV, Label label14, Button button14)
        {
            ListBox.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
            CheckBox.CheckForIllegalCrossThreadCalls = false;
            ComboBox.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            //options.AddArgument("--headless");
            //options1.AddArgument("--user-agent=Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
            var driver = new ChromeDriver(service, options);



            try
            {

                int count = listBox2.Items.Count;
                for (int i = 1; i <= count; i++)
                {
                    driver.Navigate().GoToUrl("http://instagram.com/" + listBox2.Text);


                    int n = IGDV.Rows.Add();


                    //Username
                    IGDV.Rows[n].Cells[0].Value = listBox2.Text;

                    try
                    {
                        //followers
                        string followers = driver.FindElement(By.XPath("(//span[@class='g47SY '])[2]")).Text;
                        IGDV.Rows[n].Cells[1].Value = followers;
                    }



                    catch
                    {
                        IGDV.Rows[n].Cells[1].Value = "Not Found";
                    }


                    try
                    {
                        //Following
                        string following = driver.FindElement(By.XPath("(//span[@class='g47SY '])[3]")).Text;
                        IGDV.Rows[n].Cells[2].Value = following;
                    }


                    catch
                    {

                        IGDV.Rows[n].Cells[2].Value = "Not Found";
                    }

                    try
                    {

                        //Total Posts
                        string posts = driver.FindElement(By.XPath("(//span[@class='g47SY '])[1]")).Text;
                        IGDV.Rows[n].Cells[3].Value = posts;


                    }

                    catch
                    {

                        IGDV.Rows[n].Cells[3].Value = "Not Found";
                    }


                    //Bio
                    try
                    {

                        string bio = driver.FindElement(By.XPath(".//*[@id='react-root']/section/main/div/header/section/div[2]")).Text;
                        IGDV.Rows[n].Cells[4].Value = bio;
                    }
                    catch
                    {
                        IGDV.Rows[n].Cells[4].Value = "NO BIO";
                    }

                    //Website
                    try
                    {
                        string website = driver.FindElement(By.XPath(".//*[@id='react-root']/section/main/div/header/section/div[2]/a")).Text;
                        IGDV.Rows[n].Cells[5].Value = website;
                    }
                    catch
                    {
                        IGDV.Rows[n].Cells[5].Value = "NO WEBSITE";
                    }

                    //Verified
                    try
                    {

                        string verify = driver.FindElement(By.XPath(".//*[@id='react-root']/section/main/div/header/section/div[1]/span")).Text;
                        IGDV.Rows[n].Cells[6].Value = "YES";

                    }
                    catch
                    {
                        IGDV.Rows[n].Cells[6].Value = "NO";
                    }

                    //Private

                    try
                    {
                        string prvt = driver.FindElement(By.ClassName("PJXu4")).Text;


                        IGDV.Rows[n].Cells[7].Value = "NO";
                    }
                    catch (NoSuchElementException)
                    {

                        IGDV.Rows[n].Cells[7].Value = "YES";
                    }


                    label14.Text = i.ToString();

                    IGDV.FirstDisplayedScrollingRowIndex = IGDV.RowCount - 1;


                    //Change URL
                    if (listBox2.SelectedIndex < listBox2.Items.Count - 1)
                    {
                        listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            driver.Quit();
            button14.Text = "RUN";
            button14.BackColor = Color.FromArgb(92, 181, 96);
        }
    }
}
