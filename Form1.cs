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
}
