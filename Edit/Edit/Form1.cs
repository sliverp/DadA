﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Edit.Error;
namespace Edit
{
    public partial class Form1 : Form
    {
        public static String CurrentFileName;
        public static TextBox output;
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
            try
            {
                string filepath = openFileDialog.FileName;
              
            }
            catch (Exception)
            {
                //nothing
            }
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openfileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\PC\\Desktop";
            openFileDialog.Filter = "DadA源文件(*.dada)|*.dada";
            openFileDialog.ShowDialog();
            try
            {
                string filepath = openFileDialog.FileName;
                TextBox textBox = (TextBox)Controls.Find("programInput", false).First();
                textBox.Text = System.IO.File.ReadAllText(filepath);
                Form1.CurrentFileName = openFileDialog.SafeFileName;
            }
            catch (Exception)
            {
                //nothing
            }
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
            TextBox textBox=(TextBox)Controls.Find("programInput", false).First();
            Form1.output = (TextBox)Controls.Find("textBox2", false).First();
            Form1.output.Text = "";
            ComplierAndRunner complier = new ComplierAndRunner(textBox.Text);
            Thread thread = new Thread(new ThreadStart(complier.programStart));
            thread.Start(); 
        }
    }
}
