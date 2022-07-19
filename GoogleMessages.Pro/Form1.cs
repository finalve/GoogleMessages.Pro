using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System;
namespace GoogleMessages.Pro
{
    public partial class Form1 : Form
    {
        IWebDriver driver = new ChromeDriver();
        List<string> phones = new List<string>();
        bool isAuth = false;
        public Form1()
        {
            InitializeComponent();
            driver.Navigate().GoToUrl("https://messages.google.com/web/authentication");
            using (QRSCAN qrform = new QRSCAN(driver)) 
            {
                if (qrform.ShowDialog() == DialogResult.OK)
                {
                    isAuth = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!isAuth)
            {
               driver.Quit();
               Application.Exit();
            }
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                phones = File.ReadAllLines(openFileDialog1.FileName).ToList();
                phones.ForEach((_phone) => {
                    listView2.Items.Add(_phone);
                });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (phones.Count() < 1)
            {
                msg("Phones's Null");
                return;
            }
           listView1.Items.Clear();
           new Action(() => {
               driver.Navigate().GoToUrl("https://messages.google.com/web/conversations/new?mode=add-people");
               Thread.Sleep(2000);
               foreach (var _phone in phones)
               {
                   IWebElement input = driver.FindElement(By.TagName("input"));
                   input.SendKeys(_phone);
                   input.SendKeys(OpenQA.Selenium.Keys.Enter);
                   msg($"add number {_phone}");
                   Thread.Sleep(650);
               }

               IWebElement Next = driver.FindElement(By.ClassName("next-button"));
               Next.Click();
               Thread.Sleep(1000);

               IWebElement _message = driver.FindElement(By.TagName("textarea"));
               msg("Write Message");
               _message.SendKeys(textBox1.Text.Replace(System.Environment.NewLine, OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Enter));

               Thread.Sleep(1000);

               _message.SendKeys(OpenQA.Selenium.Keys.Enter);
               msg("Sending Already");
           })();
        }

        void msg(string _msg)
        {
            listView1.Items.Insert(0,_msg);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            driver.Quit();
        }
    }
}