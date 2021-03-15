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

namespace Pago_de_Propiedades
{
    public partial class Form1 : Form
    {
        List<Propietarios> pro = new List<Propietarios>();

        Boolean encontrar_pro = false;
        int pr = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                leer_propietarios();
                Form2 f2 = new Form2();
                Propietarios p = new Propietarios();
                p.Dpi = textBox1.Text;
                repetidos();
                if (encontrar_pro)
                {
                    f2.dpi = textBox1.Text;
                    f2.name = pr[Pr].Nombre;
                    f2.surname = pr[Pr].Apellido;
                    textBox1.Clear();
                    hallarPr = false;
                    Pr = 0;
                }
                else
                {
                    f2.dpi = textBox1.Text;
                    f2.textBox2.Enabled = true;
                    f2.textBox2.Focus();
                    f2.textBox3.Enabled = true;
                    textBox1.Clear();
                    Pr = 0;
                }
                f2.Show();
                f2.button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Porfavor introduzca el Número de DPI");
            }


        }

        void leer_propietarios()
        {
            OpenFileDialog op = new OpenFileDialog();
            string fileName = "Propietarios.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Propietarios p = new Propietarios();
                p.Dpi = reader.ReadLine();
                p.Nombre = reader.ReadLine();
                p.Apellido = reader.ReadLine();
                pro.Add(p);
            }
            reader.Close();

        }

        void repetidos()
        {
            while (encontrar_pro == false && pr < pro.Count)
            {
                if (pro[pr].Dpi.CompareTo(textBox1.Text) == 0)
                {
                    encontrar_pro = true;
                }
                else
                {
                    pr++;
                }
            }
        }

    }
}
