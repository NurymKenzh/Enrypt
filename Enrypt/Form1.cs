using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDo_Click(object sender, EventArgs e)
        {
            textBoxEncrypt.Text = Encrypt(textBoxWord.Text);
            textBoxDecrypt.Text = Decrypt(textBoxEncrypt.Text);
        }

        public static String Encrypt(String encryptstr)
        {
            try
            {
                byte[] data = ASCIIEncoding.ASCII.GetBytes(encryptstr);
                byte[] rgbKey = ASCIIEncoding.ASCII.GetBytes("vY7q-3Os");
                byte[] rgbIV = ASCIIEncoding.ASCII.GetBytes("_1pCq7Yw");

                MemoryStream memoryStream = new MemoryStream(1024);
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoServiceProvider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                byte[] result = new byte[(int) memoryStream.Position];
                memoryStream.Position = 0;
                memoryStream.Read(result, 0, result.Length);
                cryptoStream.Close();
                cryptoStream.Dispose();
                return Convert.ToBase64String(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static String Decrypt(String decryptstr)
        {
            string decrypted = string.Empty;
            try
            {
                byte[] data = System.Convert.FromBase64String(decryptstr);
                byte[] rgbKey = System.Text.ASCIIEncoding.ASCII.GetBytes("vY7q-3Os");
                byte[] rgbIV = System.Text.ASCIIEncoding.ASCII.GetBytes("_1pCq7Yw");

                MemoryStream memoryStream = new MemoryStream(data.Length);
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                ICryptoTransform x = desCryptoServiceProvider.CreateDecryptor(rgbKey, rgbIV);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, x, CryptoStreamMode.Read);
                memoryStream.Write(data, 0, data.Length);

                memoryStream.Position = 0;
                decrypted = new StreamReader(cryptoStream).ReadToEnd();
                cryptoStream.Close();
                memoryStream.Dispose();
                return decrypted;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
