using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void input_keyDown(object sender, KeyEventArgs e) { 
        }
        private void input_keyPress(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int ch = e.KeyValue;
            TextBox tb2=(TextBox)Controls.Find("textBox2", false).First();
            bool isShiftPressed = e.Shift;
            //tb2.AppendText(e.KeyValue.ToString()+"   ");
            int pos = textBox.SelectionStart;
            //tb2.AppendText(pos + "|");
            switch (ch)
            {
                case 222://"
                    if (isShiftPressed)
                    {
                        textBox.SelectedText += "\"";
                    }
                    else
                    {
                        textBox.SelectedText += "'";
                    }
                    break;
                case 219://{
                    if (isShiftPressed)
                    {
                        textBox.SelectedText += "}";
                    }
                    else
                    {
                        textBox.SelectedText += "]";
                    }
                    break;
                case 188://<
                    if (isShiftPressed)
                    {
                        textBox.SelectedText += ">";
                    }
                    break;
                case 57://(
                    if (isShiftPressed)
                    {
                        textBox.SelectedText += ")";
                    }
                    break;
            }
            textBox.Select(pos, 0);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filepath = openFileDialog.FileName;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openfileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string filepath = openFileDialog.FileName;
            TextBox textBox = (TextBox)Controls.Find("programInput", false).First();
            textBox.Text = System.IO.File.ReadAllText(filepath);
        }

        private void saveFileBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            TextBox textBox = (TextBox)Controls.Find("programInput",false).First();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, textBox.Text); 
            }
        }

        private void 开始执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TextBox textBox=(TextBox)Controls.Find("programInput", false).First();
            Lex lex = new Lex("");
            lex.test();
        }
    }
}
