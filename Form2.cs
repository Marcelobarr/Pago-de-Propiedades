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
    public partial class Form2 : Form
    {
        List<Propietarios> prop = new List<Propietarios>();
        List<Propiedades> pds = new List<Propiedades>();
        List<Usuario_pro> u_pro = new List<Usuario_pro>();
        List<Cant_propiedades> cant_p = new List<Cant_propiedades>();
        List<Mayor_cuota> mayor = new List<Mayor_cuota>();

        int cont1 = 1;

        public string dpi;
        public string name;
        public string surname;

        Boolean encontrar_prod = false;
        int cont_prod = 0;

        Boolean encontrar_pro = false;
        int cont_pro = 0;

        Boolean encontrar_upro = false;
        int cont_upro = 0;

        Boolean encontrar_cant = false;
        int cont_cant = 0;

        Boolean encontar_may = false;
        int cont_may = 0;



        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                Cant_propiedades c = new Cant_propiedades();
                repetidos_prop();
                if (encontrar_pro)
                {
                    repetidos_cant();
                    if (encontrar_cant)
                    {
                        c.Nombre = textBox2.Text;
                        c.Apellido = textBox3.Text;
                        cant_p[cont_cant].Cantidad += 1;
                        c.Cantidad = cant_p[cont_cant].Cantidad;
                        cant_p.Add(c);
                        cant_p.RemoveAt(cont_cant);
                        escribir_cant();
                        encontrar_cant = false;
                        cont_cant = 0;
                    }
                    else
                    {
                        cont_cant = 0;
                    }

                    encontrar_pro = false;
                    cont_pro = 0;
                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                }
                else
                {
                    Propietarios p = new Propietarios();
                    p.Dpi = textBox1.Text;
                    p.Nombre = textBox2.Text;
                    p.Apellido = textBox3.Text;
                    prop.Add(p);
                    escribir_propietarios();
                    c.Nombre = textBox2.Text;
                    c.Apellido = textBox3.Text;
                    c.Cantidad = cont1;
                    cant_p.Add(c);
                    escribir_cant();
                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    cont_pro = 0;
                }
                textBox4.Focus();
                button2.Enabled = true;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text) && Convert.ToDouble(textBox5.Text) > 0)
            {
                Propiedades p = new Propiedades();
                Usuario_pro us = new Usuario_pro();
                Mayor_cuota m = new Mayor_cuota();
                p.Numero_casa = textBox4.Text;
                repetidos_us();
                if (encontrar_upro)
                {
                    MessageBox.Show("El número de casa introducido ya no está disponible");
                    textBox4.Clear();
                    encontrar_upro = false;
                    cont_upro = 0;
                }
                else
                {
                    p.Dpi = textBox1.Text;
                    p.Mantenimiento = Convert.ToDouble(textBox5.Text);
                    pds.Add(p);
                    escribir_propiedades();
                    us.Nombre = textBox2.Text;
                    us.Apellido = textBox3.Text;
                    us.No = textBox4.Text;
                    us.Mantenimiento = Convert.ToDouble(textBox5.Text);
                    u_pro.Add(us);
                    escribir_usprop();
                    repetidos_may();
                    if (encontar_may)
                    {
                        m.Nombre = textBox2.Text;
                        m.Apellido = textBox3.Text;
                        m.Mantenimiento = mayor[cont_may].Mantenimiento + Convert.ToDouble(textBox5.Text);
                        mayor.Add(m);
                        mayor.RemoveAt(cont_may);
                        escribir_may();
                        encontar_may = false;
                        cont_may = 0;
                    }
                    else
                    {
                        m.Nombre = textBox2.Text;
                        m.Apellido = textBox3.Text;
                        m.Mantenimiento = Convert.ToDouble(textBox5.Text);
                        mayor.Add(m);
                        escribir_may();
                        cont_may = 0;
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = u_pro;
                    dataGridView1.Refresh();
                    MessageBox.Show("Propiedad asignada exitósamente");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    button2.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            mayor1();
            mayorM();
            menorM();
            mayorT();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = dpi;
            textBox2.Text = name;
            textBox3.Text = surname;
            textBox2.Focus();
            leer_propietarios();
            leer_propiedades();
            leer_usprop();
            leer_cant();
            leer_may();
            if (cant_p.Count >= 1)
            {
                mayor1();
            }
            if (pds.Count >= 3)
            {
                mayorM();
                menorM();
            }

            if (mayor.Count >= 1)
            {
                mayorT();
            }
        }

        void mayor1()
        {
            if (cant_p.Count >= 1)
            {
                textBox6.Clear();
                cant_p = cant_p.OrderByDescending(c => c.Cantidad).ToList();
                textBox6.AppendText(cant_p[0].Nombre + " " + cant_p[0].Apellido);
            }
        }

        void mayorM()
        {
            if (pds.Count >= 3)
            {
                listBox1.Items.Clear();
                pds = pds.OrderByDescending(p => p.Mantenimiento).ToList();
                listBox1.Items.Insert(0, pds[0].Mantenimiento);
                listBox1.Items.Insert(1, pds[1].Mantenimiento);
                listBox1.Items.Insert(2, pds[2].Mantenimiento);
            }
        }

        void menorM()
        {
            if (pds.Count >= 3)
            {
                listBox2.Items.Clear();
                pds = pds.OrderBy(p => p.Mantenimiento).ToList();
                listBox2.Items.Insert(0, pds[0].Mantenimiento);
                listBox2.Items.Insert(1, pds[1].Mantenimiento);
                listBox2.Items.Insert(2, pds[2].Mantenimiento);
            }
        }

        void mayorT()
        {
            if (mayor.Count >= 1)
            {
                label12.Text.Remove(0);
                mayor = mayor.OrderByDescending(m => m.Mantenimiento).ToList();
                label12.Text = mayor[0].Nombre + " " + mayor[0].Apellido + " es el propietario con la cuota de mantenimiento más alta con " +
                    mayor[0].Mantenimiento;
            }
        }

        void escribir_propietarios()
        {
            FileStream stream = new FileStream("Propietarios.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            foreach (var p in prop)
            {
                write.WriteLine(p.Dpi);
                write.WriteLine(p.Nombre);
                write.WriteLine(p.Apellido);
            }
            write.Close();
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
                prop.Add(p);
            }
            reader.Close();
        }

        void escribir_propiedades()
        {
            FileStream stream = new FileStream("propiedades.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            foreach (var p in pds)
            {
                write.WriteLine(p.Dpi);
                write.WriteLine(p.Numero_casa);
                write.WriteLine(p.Mantenimiento);
            }
            write.Close();
        }

        void leer_propiedades()
        {
            OpenFileDialog op = new OpenFileDialog();
            string fileName = "Propiedades.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(stream);
            while (read.Peek() > -1)
            {
                Propiedades p = new Propiedades();
                p.Dpi = read.ReadLine();
                p.Numero_casa = read.ReadLine();
                p.Mantenimiento = Convert.ToDouble(read.ReadLine());
                pds.Add(p);
            }
            read.Close();
        }

        void escribir_usprop()
        {
            FileStream stream = new FileStream("Usuarios.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            foreach (var u in u_pro)
            {
                write.WriteLine(u.Nombre);
                write.WriteLine(u.Apellido);
                write.WriteLine(u.No);
                write.WriteLine(u.Mantenimiento);
            }
            write.Close();
        }

        void leer_usprop()
        {
            OpenFileDialog op = new OpenFileDialog();
            string fileName = "Usuarios.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Usuario_pro u = new Usuario_pro();
                u.Nombre = reader.ReadLine();
                u.Apellido = reader.ReadLine();
                u.No = reader.ReadLine();
                u.Mantenimiento = Convert.ToDouble(reader.ReadLine());
                u_pro.Add(u);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = u_pro;
                dataGridView1.Refresh();
            }
            reader.Close();
        }

        void repetidos_prod()
        {
            while (encontrar_prod == false && cont_prod < pds.Count)
            {
                if (pds[cont_prod].Numero_casa.CompareTo(textBox4.Text) == 0)
                {
                    encontrar_prod = true;
                }
                else
                {
                    cont_prod++;
                }
            }
        }

        void repetidos_prop()
        {
            while (encontrar_pro == false && cont_pro < prop.Count)
            {
                if (prop[cont_pro].Dpi.CompareTo(textBox1.Text) == 0)
                {
                    encontrar_pro = true;
                }
                else
                {
                    cont_pro++;
                }
            }
        }

        void repetidos_us()
        {
            while (encontrar_upro == false && cont_upro < u_pro.Count)
            {
                if (u_pro[cont_upro].No.CompareTo(textBox4.Text) == 0)
                {
                    encontrar_upro = true;
                }
                else
                {
                    cont_upro++;
                }
            }
        }

        void repetidos_cant()
        {
            while (encontrar_cant == false && cont_cant < cant_p.Count)
            {
                if (cant_p[cont_cant].Nombre.CompareTo(textBox2.Text) == 0 && cant_p[cont_cant].Apellido.CompareTo(textBox3.Text) == 0)
                {
                    encontrar_cant = true;
                }
                else
                {
                    cont_cant++;
                }
            }
        }

        void escribir_cant()
        {
            FileStream stream = new FileStream("Cantidad.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            foreach (var c in cant_p)
            {
                write.WriteLine(c.Nombre);
                write.WriteLine(c.Apellido);
                write.WriteLine(c.Cantidad);
            }
            write.Close();
        }

        void leer_cant()
        {
            OpenFileDialog op = new OpenFileDialog();
            string fileName = "Cantidad.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(stream);
            while (read.Peek() > -1)
            {
                Cant_propiedades c = new Cant_propiedades();
                c.Nombre = read.ReadLine();
                c.Apellido = read.ReadLine();
                c.Cantidad = Convert.ToInt32(read.ReadLine());
                cant_p.Add(c);
            }
            read.Close();
        }

        void escribir_may()
        {
            FileStream stream = new FileStream("Mayor.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            foreach (var m in mayor)
            {
                write.WriteLine(m.Nombre);
                write.WriteLine(m.Apellido);
                write.WriteLine(m.Mantenimiento);
            }
            write.Close();
        }

        void leer_may()
        {
            OpenFileDialog op = new OpenFileDialog();
            string fileName = "Mayor.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(stream);
            while (read.Peek() > -1)
            {
                Mayor_cuota m = new Mayor_cuota();
                m.Nombre = read.ReadLine();
                m.Apellido = read.ReadLine();
                m.Mantenimiento = Convert.ToDouble(read.ReadLine());
                mayor.Add(m);
            }
            read.Close();
        }

        void repetidos_may()
        {
            while (encontar_may == false && cont_may < mayor.Count)
            {
                if (mayor[cont_may].Nombre.CompareTo(textBox2.Text) == 0 && mayor[cont_may].Apellido.CompareTo(textBox3.Text) == 0)
                {
                    encontar_may = true;
                }
                else
                {
                    cont_may++;
                }
            }
        }

    }
}
