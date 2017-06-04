using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
    public partial class StockManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var filename = csvFile.FileName.Split('_');

            var ticker = filename[0];
            var name = filename[1];

            var filestream = csvFile.FileContent;
            var begin = DateTime.Now;
            var reader = new StreamReader(filestream);
            var csvData = reader.ReadToEnd();
            TextBox1.Text = $"{(DateTime.Now - begin).TotalMilliseconds}";
            
            

        }
    }
}