using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Instagram_Data_Grabber
{
    class PostLink
    {
        public void PostLnk(ListView pLinks, TextBox txtSearchKey, Label label4, TextBox count, CheckBox check1, CheckBox check2, Button button9)
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

                if (check1.Checked)
                {

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

                    var options = new ChromeOptions();
                    options.AddArgument("no-sandbox");
                    options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                    //options.AddArgument("--headless");
                    //options1.AddArgument("--user-agent=Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                    var driver = new ChromeDriver(service, options);

                    driver.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" + txtSearchKey.Text);



                    int i = 1;

                    while (i <= int.Parse(count.Text))
                    {



                        foreach (var item in driver.FindElements(By.TagName("a")))
                        {

                            for (int k = 0; k < 5; k++)
                            {
                                Actions actions = new Actions(driver);
                                actions.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                            }


                            pLinks.Items.Add(item.GetAttribute("href"));
                            i++;
                            label4.Text = i.ToString();

                            try
                            {

                                for (int n = pLinks.Items.Count - 1; n >= 0; --n)
                                {
                                    string removelistitem = "https://www.instagram.com/p/";
                                    if (!pLinks.Items[n].ToString().Contains(removelistitem))
                                    {
                                        pLinks.Items.RemoveAt(n);
                                    }
                                }

                            }
                            catch
                            {

                                //
                            }
                        }

                    }

                    driver.Quit();
                    label4.Text = pLinks.Items.Count.ToString();
                    button9.Text = "RUN";
                    button9.BackColor = Color.FromArgb(92, 181, 96);
                }

                else if (check2.Checked)
                {
                    ChromeDriverService service1 = ChromeDriverService.CreateDefaultService();
                    service1.HideCommandPromptWindow = true;

                    var options1 = new ChromeOptions();
                    options1.AddArgument("no-sandbox");
                    options1.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                    //options1.AddArgument("--headless");
                    //options1.AddArgument("--user-agent=Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                    var driver = new ChromeDriver(service1, options1);

                    driver.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" + txtSearchKey.Text);




                    int i = 0;

                    while (i <= int.Parse(count.Text))
                    {
                        ListViewItem lvi = new ListViewItem();

                        foreach (var item in driver.FindElements(By.TagName("a")))
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                Actions actions = new Actions(driver);
                                actions.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                            }


                            try
                            {
                                pLinks.Items.Add(item.GetAttribute("href"));
                                i++;
                                label4.Text = i.ToString();

                                for (int n = pLinks.Items.Count - 1; n >= 0; --n)
                                {
                                    string removelistitem = "https://www.instagram.com/p/";
                                    if (!pLinks.Items[n].ToString().Contains(removelistitem))
                                    {
                                        pLinks.Items.RemoveAt(n);
                                    }
                                }

                            }
                            catch
                            {

                                //
                            }
                        }

                    }

                    driver.Quit();
                    label4.Text = pLinks.Items.Count.ToString();
                    button9.Text = "RUN";
                    button9.BackColor = Color.FromArgb(92, 181, 96);
                    button9.TabStop = false;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Important Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
