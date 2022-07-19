using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleMessages.Pro
{
    public partial class QRSCAN : Form
    {
        IWebDriver driver;
        public QRSCAN(IWebDriver _driver)
        {
            InitializeComponent();
            driver = _driver;
            auth();
        }

        async void auth()
        {
            await Task.Run(() =>
            {
                while (driver.Url != "https://messages.google.com/web/conversations")
                {
                    Thread.Sleep(1000);
                }
                DialogResult = DialogResult.OK;
            });
        }
    }
}
