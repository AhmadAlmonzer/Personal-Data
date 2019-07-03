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

namespace Personal_Data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();  
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text.Trim() == "" || txtName.Text.Trim() == "" || txtAddress.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter all the information");
                    return;
                }
                 
                StreamReader sr = new StreamReader("Data.txt");
                string strCheck = sr.ReadToEnd();
                sr.Close();

                if (strCheck.Contains(txtId.Text + ";"))
                {

                    MessageBox.Show("this ID is alraedy used ; Please Enter another one");
                    txtId.Focus();
                    txtId.SelectAll();
                }
                else
                {
                    StreamWriter sw = new StreamWriter("Data.txt", true);
                    string strPerson = txtId.Text + ";"
                                     + txtName.Text + ";"
                                     + txtAddress.Text;
                    sw.WriteLine(strPerson);
                    sw.Close();

                    if (!Directory.Exists("img"))
                    {
                        Directory.CreateDirectory("img");
                    }
                    PicPerson.Image.Save("img/" + txtId.Text +".jpg");
                    MessageBox.Show("Data added");
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            c.Text = "";
                        }
                        PicPerson.Image = new PictureBox().Image;
                        txtId.Focus();

                    }


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text.Trim() != "")
                {
                    StreamReader sr = new StreamReader("Data.txt");
                    string line = "";
                    bool Found = false;
                    do
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            string[] arrData = line.Split(';');
                            if (arrData[0] == txtId.Text)
                            {
                                txtId.Text = arrData[0];
                                txtName.Text = arrData[1];
                                txtAddress.Text = arrData[2];
                                string pic = "img/" + arrData[0] + ".jpg";
                                PicPerson.Image = Image.FromFile(pic);
                                Found = true;
                                break;
                            }
                        }
                    } while (line != null);
                    sr.Close();

                    if (!Found)
                    {
                        MessageBox.Show("this ID is not found");
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Text = "";
                            }


                        }
                        txtId.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("please Enter ID");
                    txtId.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            Form frmDataShow = new Form();
            TextBox txtDataShow = new TextBox();
            frmDataShow.StartPosition = this.StartPosition;
            frmDataShow.Size = this.Size;
            frmDataShow.Text = "All Data";
            txtDataShow.Multiline = true;
            txtDataShow.Dock = DockStyle.Fill;
            frmDataShow.Controls.Add(txtDataShow);
            
            try
            {
                StreamReader sr = new StreamReader("Data.txt");
                string strDataShow = sr.ReadToEnd();
                txtDataShow.Text = strDataShow;
                sr.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frmDataShow.ShowDialog();
        }

        private void BtnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Images|*.jpg;*.png;*.gif;*.bmp";
            if (of.ShowDialog()==DialogResult.OK)
            {
                PicPerson.Image = Image.FromFile(of.FileName);
            }
        }

        private void btnShowAllWithImages_Click(object sender, EventArgs e)
        {
            Form frmDataShow = new Form();
            
            frmDataShow.StartPosition = this.StartPosition;
            frmDataShow.Size = this.Size;
            frmDataShow.Text = "All Data With Images";
            frmDataShow.Height += 300;
            frmDataShow.AutoScroll = true;
            int myTop = 10;

            try
            {
                StreamReader sr = new StreamReader("Data.txt");
                string line = "";
                do
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        TextBox txt = new TextBox();
                        PictureBox pic = new PictureBox();
                        txt.Width = 300;
                        txt.Top = myTop;
                        txt.Text = line;
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.Left = 305;
                        pic.Top = myTop;
                        pic.Size = new Size(150, 150);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        string imgPath = "img/" + line.Split(';')[0] + ".jpg";
                        if(File.Exists(imgPath))
                        
                            pic.Image = Image.FromFile(imgPath);
                        frmDataShow.Controls.Add(txt);
                        frmDataShow.Controls.Add(pic);
                        
                        myTop += 155;


                    }
                } while (line != null);

                sr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frmDataShow.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDT.Text = DateTime.Now.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
