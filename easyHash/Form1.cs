using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace easyHash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public string ComputeChecksum(string str, RadioButton radioButton, RadioButton rx)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] checkSum;
            if (radioButtonX == radioButton5)
            {
                using (FileStream fs = System.IO.File.OpenRead(str))
                {
                    byte[] fileData = new byte[fs.Length];
                    fs.Read(fileData, 0, (int) fs.Length);
                    if (radioButton == radioButton1)
                        checkSum = md5.ComputeHash(fileData);
                    else if (radioButton == radioButton2)
                        checkSum = sha1.ComputeHash(fileData);
                    else
                        checkSum = sha256.ComputeHash(fileData);
                }
            }
            else
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
                if (radioButton == radioButton1)
                    checkSum = md5.ComputeHash(data);
                else if (radioButton == radioButton2)
                    checkSum = sha1.ComputeHash(data);
                else
                    checkSum = sha256.ComputeHash(data);
            }
            string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
            return result;
        }

        private RadioButton radioButton;
        private RadioButton radioButtonX;
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                MessageBox.Show("Вы выбрали " + radioButton.Text);
            }        
        }
        private void radioButtonX_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonX = (RadioButton)sender;
            if (radioButtonX == radioButton4)
                textBox1.Enabled = true;
            else
                textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (radioButtonX == radioButton5)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = "Result:" + ComputeChecksum(ofd.FileName, radioButton, radioButtonX);
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                label1.Text = "Result:" + ComputeChecksum(textBox1.Text, radioButton, radioButtonX);
            }
        }
    }
}