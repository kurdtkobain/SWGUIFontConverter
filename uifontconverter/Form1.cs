﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Drawing.BitmapFont;

namespace SWGUIFontConverter
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
            ofd.DefaultExt = ".fnt";
            ofd.Filter = "Bitmap Font Generator File|*.fnt";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                textBox2.Text = System.IO.Path.ChangeExtension(ofd.FileName, "inc");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".inc";
            sfd.Filter = "SWG Font Include File|*.inc";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = sfd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BitmapFont bmf = BitmapFontLoader.LoadFontFromFile(textBox1.Text);
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
            if (bmf.Characters.Count > 99)
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
            System.IO.StreamWriter file = new System.IO.StreamWriter(textBox2.Text);
            file.Write(sb.ToString());
            file.Close();
        }
    }
}
