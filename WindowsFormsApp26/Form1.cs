using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing;

namespace WindowsFormsApp26
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.ScrollBars = ScrollBars.Vertical;
            SetTextBox1Text = new DelegateSetText((s) => { textBox1.Text = s; });           
        }

        string Encrypt(string text)
        { //string to binary            
            StringBuilder binary = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                binary.Append(Convert.ToString(text[i], 2).PadLeft(8, '0'));
            }

            return binary.ToString();
        }

        string Binary()
        {
            return Encrypt(textBox1.Text);
        }

        string Bin_str(string slovo)
        { //binary to string            
            string temp = null;
            if (slovo.Length % 8 != 0)
            {
                MessageBox.Show("Кол-во цифр не кратно 8 (Вероятно, вы использовали русские буквы)");
                return "";
            } 
            
            for (int i = 0; i < slovo.Length; i += 8)
            {
                temp += (char)Convert.ToByte(slovo.Substring(i, 8), 2);
            }
            //textBox1.Text = temp;
            return temp;
        }

        string Replace()
        {
            //удаление каждого 2ого символа в строке
            string str = textBox1.Text;
            string result = "";
            for (int i = 0; i < str.Length; i += 2)
            {
                result += str[i];

            }
            return result;
        }

        private void carbonFiberButton10_Click(object sender, EventArgs e)
        {          
            textBox1.Text = Binary();
        }

        private void carbonFiberButton12_Click(object sender, EventArgs e)
        {
            if (carbonFiberRadioButton2.Checked)
            {
                textBox4.Text = GenerateRandomKey(19);
            }
            else
            {
                textBox3.Text = GenerateRandomKey(19);
                textBox4.Text = GenerateRandomKey(19);
            }          
        }

        static Random rnd = new Random();
        static string alphabet = "abcdefghijklmnoqrstuvwxyz12345"; 

        string GenerateRandomKey(int length)
        {
            StringBuilder key = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                key.Append(alphabet[rnd.Next(0, alphabet.Length - 1)]);

            }
            return key.ToString();
        }


        public void carbonFiberButton9_Click(object sender, EventArgs e)
        {     
            textBox1.Text = Bin_str(textBox1.Text);
        }

        delegate void DelegateSetText(string text);
        DelegateSetText SetTextBox1Text = null;

        private void carbonFiberButton11_Click_1(object sender, EventArgs e)
        {
            var b = sender as Button;

            Thread thread = new Thread((s) =>
            {
                EcryptTreadParams p = (EcryptTreadParams)s;
                if (p.checked1 && !string.IsNullOrWhiteSpace(p.text1))
                {
                    p.text2 = PairConcat(Encrypt(p.text1), Encrypt(p.text2));
                    p.text2 = Bin_str(p.text2);
                }
                else if (p.checked2 && !string.IsNullOrWhiteSpace(p.text1) && !string.IsNullOrWhiteSpace(p.text3))
                {
                    //запись элементов двух ключей
                    p.text2 = PairConcat(Encrypt(p.text1), Binary());
                    p.text2 = PairConcat(Encrypt(p.text3), p.text2);
                    //перевод в строку
                    p.text2 = Bin_str(p.text2);
                }
                else
                {
                    if (p.checked2 || p.checked1)
                    {
                        MessageBox.Show("Press \"random\" button!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Select method!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                textBox1.Invoke(SetTextBox1Text, p.text2);
            });
            thread.Start(new EcryptTreadParams(textBox4.Text, textBox1.Text, textBox3.Text, carbonFiberRadioButton2.Checked, carbonFiberRadioButton1.Checked));
        }


        static char GetNextChar(string Encrypt, ref int lastIndex)
        {
            if (++lastIndex >= Encrypt.Length)
                lastIndex = 0;
            return Encrypt[lastIndex];
        }


        static char GetChar(string Papanya, ref int lastIndex)
        {
            if (++lastIndex >= Papanya.Length)
                lastIndex = 0;
            return Papanya[lastIndex];
        }

        public static string PairConcat(string Encrypt, string Binary)
        {
            StringBuilder result = new StringBuilder();
            int index = -1;
            for (int i = 0; i < Binary.Length; i++)
            {
                result.Append(Binary[i]);
                result.Append(GetNextChar(Encrypt, ref index));
            }

            return result.ToString();
        }

        private void carbonFiberButton15_Click(object sender, EventArgs e)
        {
            if (carbonFiberRadioButton2.Checked)
            {              
                textBox1.Text = Binary();              
                textBox1.Text = Replace();               
                textBox1.Text = Bin_str(textBox1.Text);

            }
            else if (carbonFiberRadioButton1.Checked)
            {
                textBox1.Text = Binary();
                textBox1.Text = Replace(); textBox1.Text = Replace();
                textBox1.Text = Bin_str(textBox1.Text);
            }
        }

        private void carbonFiberControlButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void carbonFiberButton13_Click(object sender, EventArgs e)
        {
            textBox4.Clear(); textBox3.Clear();
        }

        private void carbonFiberButton2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear();
        }

        private void carbonFiberButton5_Click(object sender, EventArgs e)
        { //открытие и вывод содержания файла в textbox          
            OpenFileDialog ofd = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
            textBox2.Text = openFileDialog1.FileName;

        }

        private void carbonFiberButton4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Write smth!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Clipboard.SetText(textBox1.Text);
                MessageBox.Show("Copied!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void carbonFiberButton3_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void carbonFiberButton1_Click(object sender, EventArgs e)
        { //сохранение файлов
            SaveFileDialog sfd = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, textBox1.Text);
            MessageBox.Show("Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void carbonFiberButton14_Click(object sender, EventArgs e)
        { //о программе         
            Form f2 = new Form2();
            f2.Show();
        }
      
        private void carbonFiberButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите метод шифрования!\r\n"
               + "Нажмите на кнопку \"random\"\r\n"
               + "Используйте \"Encrypt\" для шифрования\r\n"
               + "Используйте \"Decrypt\"для дешифрования\r\n","", MessageBoxButtons.OK);
        }

        bool isMooving = false;
        Point lastPoint;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMooving = true;
            lastPoint = e.Location;
        }

        bool res = false;
        private void Form1_Mouse(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                if (!res) 
                {
                    MessageBox.Show("Don't use \"p\" and null, please!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    res = true;
                }
            }            
        }

        bool mmm = false;
        private void MMMMM(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (!mmm)
                {
                    MessageBox.Show("Don't use russian language, please!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    mmm = true;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMooving)
                return;

            int offSetX = e.Location.X - lastPoint.X;
            int offSetY = e.Location.Y - lastPoint.Y;

            Left = Left + offSetX;
            Top = Top + offSetY;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMooving = false;
        }            
    }
}




    
