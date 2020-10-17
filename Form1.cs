using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace LED
{
    public partial class Form1 : Form
    {
        static int col = 0;

        public Form1()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                try
                {
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.Open();

                    serialPort1.Write("S");
                    System.Threading.Thread.Sleep(200);
                    string data = serialPort1.ReadExisting();
                    if (!string.IsNullOrEmpty(data) && data == "O")
                    {
                        button1.Enabled = false;
                        comboBox1.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No response");
                        serialPort1.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select COM port");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (col == 0)
            {
                serialPort1.Write("R");
                col = 1;
            }
            else if (col == 1)
            {
                serialPort1.Write("G");
                col = 2;
            }
            else if (col == 2)
            {
                serialPort1.Write("Y");
                col = 3;
            }
            else if (col == 3)
            {
                serialPort1.Write("B");
                col = 0;
            }
        }
    }
}
