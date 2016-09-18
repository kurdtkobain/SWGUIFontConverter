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

namespace SWGUITextStyleGenerator
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
            ofd.Filter = "SWG Font Include|*.inc";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string f in ofd.FileNames)
                {
                    string tmpfname = Path.GetFileNameWithoutExtension(f);
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = tmpfname;
                    row.Cells[1].Value = f;
                    dataGridView1.Rows.Add(row);
                    string tmpfamilyname = tmpfname.Remove(tmpfname.Length - 3);
                    if (!comboBox1.Items.Contains(tmpfamilyname))
                    {
                        comboBox1.Items.Add(tmpfamilyname);
                    }
                    if (!comboBox2.Items.Contains(tmpfamilyname))
                    {
                        comboBox2.Items.Add(tmpfamilyname);
                    }
                    if (!comboBox3.Items.Contains(tmpfamilyname))
                    {
                        comboBox3.Items.Add(tmpfamilyname);
                    }
                    comboBox1.SelectedIndex = 0;
                    comboBox2.SelectedIndex = 0;
                    comboBox3.SelectedIndex = 0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                MessageBox.Show("One or more fields are blank!","Error");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("	<Namespace Name='TextStyleManager'>\n<TextStyleManager\nFontFaces='default,bold,fixed,aurabesh'\nFontFaces_en_aurabesh='aurabesh'\nFontFaces_en_bold='verdana_bold'\nFontFaces_en_default='verdana'\nFontFaces_en_fixed='lucida_console'\nFontFaces_j1_aurabesh='aurabesh'\nFontFaces_j1_bold='tteditfont'\nFontFaces_j1_default='tteditfont'\nFontFaces_j1_fixed='tteditfont'\nFontFaces_j2_aurabesh='aurabesh'\nFontFaces_j2_bold='arial_unicode_ms'\nFontFaces_j2_default='arial_unicode_ms'\nFontFaces_j2_fixed='arial_unicode_ms'\nFontFaces_j3_aurabesh='aurabesh'\nFontFaces_j3_bold='swgsogeip4'\nFontFaces_j3_default='swgsogeip4'\nFontFaces_j3_fixed='swgsogeip4'\nFontFaces_j4_aurabesh='aurabesh'\nFontFaces_j4_bold='swggothicp'\nFontFaces_j4_default='swggothicp'\nFontFaces_j4_fixed='swggothicp'\nFontFaces_j5_aurabesh='aurabesh'\nFontFaces_j5_bold='swgdfgotp2'\nFontFaces_j5_default='swgdfgotp2'\nFontFaces_j5_fixed='swgdfgotp2'\n");
            sb.Append("FontFaces_" + textBox1.Text + "_aurabesh='aurabesh'\n");
            sb.Append("FontFaces_" + textBox1.Text + "_bold='" + comboBox1.SelectedItem.ToString() + "'\n");
            sb.Append("FontFaces_" + textBox1.Text + "_default='" + comboBox2.SelectedItem.ToString() + "'\n");
            sb.Append("FontFaces_" + textBox1.Text + "_fixed='" + comboBox3.SelectedItem.ToString() + "'\n");
            sb.Append("FontFaces_ja_aurabesh='aurabesh'\nFontFaces_ja_bold='tteditfont'\nFontFaces_ja_default='tteditfont'\nFontFaces_ja_fixed='tteditfont'\nFontLocale='en'\nFonts_en='font/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc\nfont/lucida_console_12.inc\nfont/lucida_console_14.inc\nfont/verdana_12.inc\nfont/verdana_13.inc\nfont/verdana_14.inc\nfont/verdana_16.inc\nfont/verdana_17.inc\nfont/verdana_18.inc\nfont/verdana_20.inc\nfont/verdana_22.inc\nfont/verdana_bold_12.inc\nfont/verdana_bold_13.inc\nfont/verdana_bold_14.inc\nfont/verdana_bold_16.inc\nfont/verdana_bold_17.inc\nfont/verdana_bold_18.inc\nfont/verdana_bold_20.inc\nfont/verdana_bold_22.inc\nfont/verdana_bold_24.inc'\nFonts_j1='font/ja/tteditfont_20.inc\nfont/ja/tteditfont_14.inc\nfont/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\nFonts_j2='font/ja/arial_unicode_ms_20.inc\nfont/ja/arial_unicode_ms_14.inc\nfont/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\nFonts_j3='font/ja/swgsogeip4_20.inc\nfont/ja/swgsogeip4_14.inc\nfont/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\nFonts_j4='font/ja/swggothicp_20.inc\nfont/ja/swggothicp_14.inc\nfont/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\nFonts_j5='font/ja/swgdfgotp2_20.inc\nfont/ja/swgdfgotp2_14.inc\nfont/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\n");
            sb.Append("Fonts_" + textBox1.Text + "='");
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells[0].Value != null && r.Cells[0].Value.ToString() != " " && r.Cells[0].Value.ToString() != "")
                {
                    sb.Append("font/" + textBox1.Text + "/" + r.Cells[0].Value + ".inc\n");
                }
            }
            sb.Append("font/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\nFonts_ja='font/ja/tteditfont_20.inc\nfont/ja/tteditfont_14.inc\nfont/aurabesh_12.inc\nfont/aurabesh_18.inc\nfont/aurabesh_24.inc'\n/>\n</Namespace>");
            System.IO.StreamWriter file = new System.IO.StreamWriter(textBox2.Text);
            file.Write(sb.ToString());
            file.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath + "\\ui_textstylemanager.inc";
            }
        }
    }
}
