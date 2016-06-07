using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication1{
    public partial class Form1 : Form{
        public Form1(){
            InitializeComponent();
        }

        static string result;

        static string tmp = @"C:\UK POWER\uk_power_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
        static string header = @"Reference,Affiliate tracking ID,Date,Supplier,Title,First Name,Last Name,Postcode,Fuel,Campaign,Referrer code,Gas Status,Elec Status";
        static System.Globalization.DateTimeFormatInfo monthInfo = new System.Globalization.DateTimeFormatInfo();     
        
        private void button1_Click(object sender, EventArgs e){            
            string[] lines = this.richTextBox1.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            ArrayList formattedArr = new ArrayList();
            formattedArr = reformatArr(lines);
            foreach (string tabs in formattedArr) {
                string[] fields = tabs.Split('\t');
                string[] parts = fields[2].Split('/');
                string dt = parts[0]+" "+monthInfo.GetMonthName( Int32.Parse( parts[1]) )+" "+parts[2];
                string[] names = fields[4].Split(' ');
                result = result + fields[0] + ",";
                result = result + fields[1] + ",";
                result = result + dt        + ",";
                result = result + fields[3] + ",";
                result = result + names[0]  + ",";
                result = result + names[1]  + ",";
                result = result + names[2]  + ",";
                result = result + fields[5] + ",";
                result = result + fields[6] + ",";
                result = result + fields[7] + ",";
                result = result + fields[8] + ",";
                result = result + fields[9] + ",";
                result = result + fields[10];
                /*--------------------------------------*/
                var subParts = result.Split(',');
                ListViewItem lvi = new ListViewItem(subParts[0]);
                foreach (string listRows in subParts.Skip(1)) {
                    lvi.SubItems.Add(listRows);
                }
                listView1.Items.Add(lvi);                
                richTextBox2.Text = richTextBox2.Text + result+Environment.NewLine;
                result = "";                
            }            
        }

        private void button2_Click(object sender, EventArgs e){
            System.IO.Directory.CreateDirectory(@"c:\UK POWER\");            
            System.IO.File.WriteAllText(tmp, richTextBox2.Text);
            System.Diagnostics.Process.Start(@"C:\UK POWER\");            
        }

        public System.Collections.ArrayList reformatArr(string[] lines) {
            ArrayList returnThis = new ArrayList();
            for (int i = 0; i < lines.Length;i++ ) {
                if (!lines[i].Contains("Reference")) {
                    returnThis.Add(lines[i]);
                }
            }
            return returnThis;
        }

        private void Form1_Load(object sender, EventArgs e){            
            string[] Rcolumns = header.Split(',');
            listView1.View = View.Details;
            for (int i = 0; i < Rcolumns.Length; i++) {
                listView1.Columns.Add(Rcolumns[i]).Width = 81;                
            }
            richTextBox2.Text = header + Environment.NewLine;
        }
    }
}
