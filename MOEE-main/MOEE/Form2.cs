using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEE
{
    public partial class Form2 : Form
    {
        static public USBControl PinguinoBoard;
        private MainMenu PinguinoFormMenu;
        private bool isClicked = false;
        const int b00000001 = 1; // mascaras para leer el 1o bit 
        const int b00000010 = 2;
        const int b00000100 = 4;
        const int b00001000 = 8;
        const int b00010000 = 16;
        const int b00100000 = 32;
        const int b01000000 = 64;
        const int b10000000 = 128;
        bool PuertoA0;// radio
        bool PuertoA7;// led
        bool PuertoA6;// polea
        public Form2()
        {
            InitializeComponent();
            //crear una referncia del objeto USB
            //Debe enviar identificador  del vendedor (Vendor ID -->VID)
            //El VID de microchip es: 0x04D8
            //Tambien se debe enviar el identificador  del Producto (product ID --->PID)
            //el PID de este producto es: 0x003F que ha sido tomado de una tarjeta desarrollada
            //por Microchip en la cual se incluye el  PIC18F45K50

            PinguinoBoard = new USBControl(0x04D8, 0x003E);

            //se agregaa un evento para manejar la comunicacion USB.
            PinguinoBoard.usbEvent += new USBControl.usbEventsHandler(usbEventReceiver);

            PinguinoBoard.findTargetDevice();

            PinguinoFormMenu = new MainMenu();

            MenuItem File = PinguinoFormMenu.MenuItems.Add("&File");
            File.MenuItems.Add(new MenuItem("&Exit",
                new EventHandler(this.FileExit_clicked),
                Shortcut.CtrlX));

            MenuItem Tools = PinguinoFormMenu.MenuItems.Add("&Tools");
            Tools.MenuItems.Add(new MenuItem("&Otput control",
                new EventHandler(this.OutputControl_clicked),
                Shortcut.CtrlU));

            this.Menu = PinguinoFormMenu;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void usbEventReceiver(Object o, EventArgs e)
        {
            //verifica el estado del dispositivo USB y lko actualiza
            if (PinguinoBoard.isDeviceAttached)
            {
                //El dispositivo esta conectado actualmente
                //actualiza el estado del label
                this.toolStripStatusLabel1.Text = "conectado";
            }
            else
            {
                this.toolStripStatusLabel1.Text = "desconectado";
            }
        }
        private void debugCollectionTimer_Tick(object sender, EventArgs e)
        {
            String debugString;
            //Solamente si el dispositivo esta conectado se obtienen datos de depuracion
            if (PinguinoBoard.isDeviceAttached)
            {
                debugString = PinguinoBoard.CollectDebug();

                //si la variable debugstring no esta vacia  se muetra  los datos en  la cajua de texto
                if (debugString != string.Empty)
                {
                    this.txtDebugOutput.AppendText(debugString);
                }
            }
            else
            {
                //limpia la caja de texto
                this.txtDebugOutput.Clear();
            }
        }
        private void chkEnabledDebug_CheckedChanged(object sender, EventArgs e)
        {
            debugCollectionTimer.Enabled = chkEnabledDebug.Checked;
        }


        private void FileExit_clicked(object sender, EventArgs e)
        {
            this.Close();

        }

        private void OutputControl_clicked(object sender, EventArgs e)
        {
            MessageBox.Show("Output control",
                "Menu Output control creado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form Informacion = new Form3();
            Informacion.ShowDialog();
        }
        private void electric(object sender, EventArgs e)
        {
            // string USBControl;
            /*
            if ( = "A")
            {
                pictureBox1.Show(); 
                pictureBox2.Hide();
                pictureBox3.Hide();
                pictureBox4.Hide();
            }
            */
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            String debugString;
            //Solamente si el dispositivo esta conectado se obtienen datos de depuracion
            if (PinguinoBoard.isDeviceAttached)
            {
                debugString = PinguinoBoard.CollectDebug();

                //si la variable debugstring no esta vacia  se muetra  los datos en  la cajua de texto
                if (debugString != string.Empty)
                {
                    this.txtDebugOutput.AppendText(debugString);
                }
            }
            else
            {
                //limpia la caja de texto
                this.txtDebugOutput.Clear();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {


            if (PinguinoBoard.isDeviceAttached)
            {
                byte porta = 0, portb = 0, portc = 0;
                byte portd = 0, porte = 0;

                Form2.PinguinoBoard.inputRead(ref porta, ref portb,
                       ref portc, ref portd, ref porte);
                PuertoA0 = Convert.ToBoolean(portb & b00000001);
                // PuertoA7 = Convert.ToBoolean(porta & b00000010);
                // PuertoA6 = Convert.ToBoolean(portc & b01000000);
                if (PuertoA0 == true)

                {

                    pictureBox1.Show();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                    pictureBox4.Hide();

                }
                else
                {
                    pictureBox1.Hide();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                    pictureBox4.Show();
                }/*
                if (PuertoA7 == true)

                {

                    pictureBox1.Hide();
                    pictureBox2.Show();
                    pictureBox3.Hide();
                    pictureBox4.Hide();

                }
                else
                {
                    pictureBox1.Hide();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                    pictureBox4.Show();
                }
                if (PuertoA6 == true)

                {

                    pictureBox1.Hide();
                    pictureBox2.Hide();
                    pictureBox3.Show();
                    pictureBox4.Hide();

                }
                else
                {
                    pictureBox1.Hide();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                    pictureBox4.Show();
                }*/
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

 
    }
}
