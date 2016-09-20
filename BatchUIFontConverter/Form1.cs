using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Drawing.BitmapFont;

namespace BatchUIFontConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap Font Generator File|*.fnt";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string f in ofd.FileNames)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = f;
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells[0].Value != null && r.Cells[0].Value.ToString() != " " && r.Cells[0].Value.ToString() != "")
                {
                    convertFile(r.Cells[0].Value.ToString(), textBox1.Text);
                }

            }
        }

        void convertFile(string inFile, string outDir)
        {
            BitmapFont bmf = BitmapFontLoader.LoadFontFromFile(inFile);
            StringBuilder sb = new StringBuilder();
            string tmpname = bmf.FamilyName.ToLower();
            if (bmf.Bold)
            {
                tmpname = bmf.FamilyName.ToLower() + "_bold";
            }
            else if (bmf.Italic)
            {
                tmpname = bmf.FamilyName.ToLower() + "_italic";
            }
            sb.Append("<textstyle name='" + tmpname + "_" + bmf.FontSize + "' leading='" + bmf.FontSize + "'>\n");
            foreach (Character o in bmf.Characters.Values)
            {
                sb.Append("<fontcharacter name=" + ((int)o.Char).ToString("x4") + " code=" + (int)o.Char + " advancePre=" + o.Offset.X + " advance=" + o.XAdvance + " sourcefile='font/" + tmpname + "_" + bmf.FontSize + "_");
                if ((bmf.Pages.Count() - 1) >= 100)
                {
                    sb.Append(o.TexturePage.ToString("D3"));
                }
                else if ((bmf.Pages.Count() - 1) >= 10)
                {
                    sb.Append(o.TexturePage.ToString("D2"));
                }
                else if ((bmf.Pages.Count() - 1) <= 9)
                {
                    sb.Append(o.TexturePage.ToString("D1"));
                }
                sb.Append("' sourcerect='" + o.Bounds.Left + "," + o.Bounds.Top + "," + o.Bounds.Right + "," + o.Bounds.Bottom + "'/>\n");
            }
            sb.Append("</textstyle>");
            System.IO.StreamWriter file = new System.IO.StreamWriter(textBox1.Text + "/" + Path.GetFileNameWithoutExtension(inFile) + ".inc");
            file.Write(sb.ToString());
            file.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }
    }
}
