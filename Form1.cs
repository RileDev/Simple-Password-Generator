using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace passwordGenerator
{
    public partial class Form1 : Form
    {
        private Random _rand;
        private char[] _char = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '=', '_', '+', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private StringBuilder _passBuilder;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _rand = new Random();
            nudCharLenght.Minimum = 6;
            nudCharLenght.Maximum = 20;
            btnGenerate.Text = "Generate";
            btnSave.Text = "Save password";
            this.Text = "P@ssw0rd Generator by: R1lE";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string password = generatePassword();
            txtPassword.Text = password;
        }

        private string generatePassword()
        {
            string password;
            _passBuilder = new StringBuilder();

            for (int i = 0; i < nudCharLenght.Value; i++)
            {
                _passBuilder.Append(_char[_rand.Next(_char.Length)]);
            }

            password = _passBuilder.ToString();
            return password;
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPassword.Text))
            {
                Clipboard.SetText(txtPassword.Text);
                MessageBox.Show("Password copied!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPassword.Text))
            {
                string dir = Directory.GetCurrentDirectory();
                string fileName = Path.Combine(dir, "passwords.txt");
                if (!File.Exists(fileName))
                {
                    File.CreateText(fileName);
                }
                else
                {
                    writePassword(fileName);
                }

            }
        }

        private void writePassword(string fileName)
        {
            int numOfLines = File.ReadAllLines(fileName).Length;
            if (!File.ReadAllLines(fileName).Contains($"Password no.{numOfLines - 1} - {txtPassword.Text}"))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fileName, Enabled))
                    {
                        sw.WriteLine($"Password no.{numOfLines} - {txtPassword.Text}");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show($"Password: {txtPassword.Text} has already saved!");
            }
            
        }
    }
}
