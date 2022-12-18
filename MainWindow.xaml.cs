using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Markup;
using System.Windows.Threading;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _serialPort = new SerialPort();
        DispatcherTimer _timer = new DispatcherTimer();
        bool _isChanged = false;
        int[] chars = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 95, 95, 6, 0, 0, 0, 3, 3, 0, 3, 3, 0, 0, 20, 127, 127, 20, 127, 127, 20, 0, 36, 46, 107, 107, 58, 18, 0, 0, 70, 102, 48, 24, 12, 102, 98, 0, 48, 122, 79, 93, 55, 122, 72, 0, 4, 7, 3, 0, 0, 0, 0, 0, 0, 28, 62, 99, 65, 0, 0, 0, 0, 65, 99, 62, 28, 0, 0, 0, 8, 42, 62, 28, 28, 62, 42, 8, 8, 8, 62, 62, 8, 8, 0, 0, 0, 128, 224, 96, 0, 0, 0, 0, 8, 8, 8, 8, 8, 8, 0, 0, 0, 0, 96, 96, 0, 0, 0, 0, 96, 48, 24, 12, 6, 3, 1, 0, 62, 127, 113, 89, 77, 127, 62, 0, 64, 66, 127, 127, 64, 64, 0, 0, 98, 115, 89, 73, 111, 102, 0, 0, 34, 99, 73, 73, 127, 54, 0, 0, 24, 28, 22, 83, 127, 127, 80, 0, 39, 103, 69, 69, 125, 57, 0, 0, 60, 126, 75, 73, 121, 48, 0, 0, 3, 3, 113, 121, 15, 7, 0, 0, 54, 127, 73, 73, 127, 54, 0, 0, 6, 79, 73, 105, 63, 30, 0, 0, 0, 0, 102, 102, 0, 0, 0, 0, 0, 128, 230, 102, 0, 0, 0, 0, 8, 28, 54, 99, 65, 0, 0, 0, 36, 36, 36, 36, 36, 36, 0, 0, 0, 65, 99, 54, 28, 8, 0, 0, 2, 3, 81, 89, 15, 6, 0, 0, 62, 127, 65, 93, 93, 31, 30, 0, 124, 126, 19, 19, 126, 124, 0, 0, 65, 127, 127, 73, 73, 127, 54, 0, 28, 62, 99, 65, 65, 99, 34, 0, 65, 127, 127, 65, 99, 62, 28, 0, 65, 127, 127, 73, 93, 65, 99, 0, 65, 127, 127, 73, 29, 1, 3, 0, 28, 62, 99, 65, 81, 115, 114, 0, 127, 127, 8, 8, 127, 127, 0, 0, 0, 65, 127, 127, 65, 0, 0, 0, 48, 112, 64, 65, 127, 63, 1, 0, 65, 127, 127, 8, 28, 119, 99, 0, 65, 127, 127, 65, 64, 96, 112, 0, 127, 127, 14, 28, 14, 127, 127, 0, 127, 127, 6, 12, 24, 127, 127, 0, 28, 62, 99, 65, 99, 62, 28, 0, 65, 127, 127, 73, 9, 15, 6, 0, 30, 63, 33, 113, 127, 94, 0, 0, 65, 127, 127, 9, 25, 127, 102, 0, 38, 111, 77, 89, 115, 50, 0, 0, 3, 65, 127, 127, 65, 3, 0, 0, 127, 127, 64, 64, 127, 127, 0, 0, 31, 63, 96, 96, 63, 31, 0, 0, 127, 127, 48, 24, 48, 127, 127, 0, 67, 103, 60, 24, 60, 103, 67, 0, 7, 79, 120, 120, 79, 7, 0, 0, 71, 99, 113, 89, 77, 103, 115, 0, 0, 127, 127, 65, 65, 0, 0, 0, 1, 3, 6, 12, 24, 48, 96, 0, 0, 65, 65, 127, 127, 0, 0, 0, 8, 12, 6, 3, 6, 12, 8, 0, 128, 128, 128, 128, 128, 128, 128, 128, 0, 0, 3, 7, 4, 0, 0, 0, 32, 116, 84, 84, 60, 120, 64, 0, 65, 127, 63, 72, 72, 120, 48, 0, 56, 124, 68, 68, 108, 40, 0, 0, 48, 120, 72, 73, 63, 127, 64, 0, 56, 124, 84, 84, 92, 24, 0, 0, 72, 126, 127, 73, 3, 2, 0, 0, 152, 188, 164, 164, 248, 124, 4, 0, 65, 127, 127, 8, 4, 124, 120, 0, 0, 68, 125, 125, 64, 0, 0, 0, 96, 224, 128, 128, 253, 125, 0, 0, 65, 127, 127, 16, 56, 108, 68, 0, 0, 65, 127, 127, 64, 0, 0, 0, 124, 124, 24, 56, 28, 124, 120, 0, 124, 124, 4, 4, 124, 120, 0, 0, 56, 124, 68, 68, 124, 56, 0, 0, 132, 252, 248, 164, 36, 60, 24, 0, 24, 60, 36, 164, 248, 252, 132, 0, 68, 124, 120, 76, 4, 28, 24, 0, 72, 92, 84, 84, 116, 36, 0, 0, 0, 4, 62, 127, 68, 36, 0, 0, 60, 124, 64, 64, 60, 124, 64, 0, 28, 60, 96, 96, 60, 28, 0, 0, 60, 124, 112, 56, 112, 124, 60, 0, 68, 108, 56, 16, 56, 108, 68, 0, 156, 188, 160, 160, 252, 124, 0, 0, 76, 100, 116, 92, 76, 100, 0, 0, 8, 8, 62, 119, 65, 65, 0, 0, 0, 0, 0, 119, 119, 0, 0, 0, 65, 65, 119, 62, 8, 8, 0, 0, 2, 3, 1, 3, 2, 3, 1, 0 };
        public MainWindow()
        {
            InitializeComponent();

            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
            {
                cbxComPorts.Items.Add(s);
            }

            cbxComPorts.SelectedItem = "None";

            cbxPower.Items.Add("Uit");
            cbxPower.Items.Add("Aan");

            cbxLedtest.Items.Add("Uit");
            cbxLedtest.Items.Add("Aan");

            _serialPort.BaudRate = 9600;
            _serialPort.StopBits = StopBits.One;
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;

            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += _timer_Tick;

        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            if (_isChanged)
            {
                List<CheckBox[]> cbx = new List<CheckBox[]>
                                   { new CheckBox[] { c1, c9, c17, c25, c33, c41, c49, c57 },
                                    new CheckBox[] { c2, c10, c18, c26, c34, c42, c50, c58 },
                                    new CheckBox[] { c3, c11, c19, c27, c35, c43, c51, c59 },
                                    new CheckBox[] { c4, c12, c20, c28, c36, c44, c52, c60 },
                                    new CheckBox[] { c5, c13, c21, c29, c37, c45, c53, c61 },
                                    new CheckBox[] { c6, c14, c22, c30, c38, c46, c54, c62 },
                                    new CheckBox[] { c7, c15, c23, c31, c39, c47, c55, c63 },
                                    new CheckBox[] { c8, c16, c24, c32, c40, c48, c56, c64 }
                                   };
                _isChanged = false;
                for (UInt16 i = 1; i < 9; i++)
                {
                    UInt16 command = MakeMAX7219CommandFromCheckbox((UInt16)(i * 256), cbx[i - 1]);
                    SendCommand(command);
                }
            }
        }

        private void checkBoxChanged(object sender, RoutedEventArgs e)
        {
            _isChanged = true;
        }
        private UInt16 MakeMAX7219CommandFromCheckbox(UInt16 address, CheckBox[] cbx)
        {
            UInt16 num = 0;
            for (int i = 0; i < cbx.Length; i++)
            {
                if (cbx[i].IsChecked == true)
                {
                    num += (UInt16)Math.Pow(2, i);
                }
            }
            return (UInt16)(address + num);
        }
        private void SendLetter(int asciiValue)
        {
            int address = 1;
            for (int i = (asciiValue-32)*8; i < (asciiValue - 32)*8 + 8; i++)
            {
                SendCommand((UInt16)((address * 256) + chars[i]));
                address++;
            }
        }
        private void SendCommand(UInt16 command)
        {
            byte[] data = new byte[2];
            data[0] = (byte)(command >> 8);
            data[1] = (byte)command;
            if (_serialPort != null)
            {
                _serialPort.Write("<"); // Start marker
                _serialPort.Write(data, 0, 2);
                _serialPort.Write(">"); // Stop marker
            }
        }
        private void cbxComPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    // Disable alle checkboxen
                    foreach (Control ctl in grdMatrix.Children)
                    {
                        if (ctl.GetType() == typeof(CheckBox))
                            ((CheckBox)ctl).IsEnabled = false;
                    }

                    sldrHelderheid.IsEnabled = false;
                    cbxPower.IsEnabled = false;
                    cbxLedtest.IsEnabled = false;
                    btnClear.IsEnabled = false;

                    _timer.Stop();
                    _serialPort.Close();
                }

                if (cbxComPorts.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxComPorts.SelectedItem.ToString();
                    _serialPort.Open();

                    // Enable alle Checkboxen
                    foreach (Control ctl in grdMatrix.Children)
                    {
                        if (ctl.GetType() == typeof(CheckBox))
                            ((CheckBox)ctl).IsEnabled = true;
                    }

                    sldrHelderheid.IsEnabled = true;
                    cbxPower.IsEnabled = true;
                    cbxLedtest.IsEnabled = true;
                    btnClear.IsEnabled = true;
                    cbxPower.SelectedItem = "Uit";
                    cbxLedtest.SelectedItem = "Uit";

                    // Initialiseer de MAX7219
                    SendCommand(MAX7219.TestMode(false)); // Test mode off
                    SendCommand(MAX7219.Scan(7)); // Scan all digits
                    SendCommand(MAX7219.DecodeMode(0)); // No decode
                    SendCommand(MAX7219.Intensity(0)); // Lowest intensity
                    SendCommand(MAX7219.POWEROFF); // Turn off
                    SendCommand(0x0100); // Doe alle leds uit
                    SendCommand(0x0200);
                    SendCommand(0x0300);
                    SendCommand(0x0400);
                    SendCommand(0x0500);
                    SendCommand(0x0600);
                    SendCommand(0x0700);
                    SendCommand(0x0800);

                    _timer.Start();
                }
            }
        }
        private void sldrHelderheid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte[] data = new byte[2];
            UInt16 command = MAX7219.Intensity(sldrHelderheid.Value);
            SendCommand(command);
        }
        private void cbxPower_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPower.SelectedItem.ToString() == "Uit")
            {
                SendCommand(MAX7219.POWEROFF);
            }
            if (cbxPower.SelectedItem.ToString() == "Aan")
            {
                SendCommand(MAX7219.POWERON);
            }
        }

        private void cbxLedtest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxLedtest.SelectedItem.ToString() == "Uit")
            {
                SendCommand(MAX7219.TestMode(false));
            }
            if (cbxLedtest.SelectedItem.ToString() == "Aan")
            {
                SendCommand(MAX7219.TestMode(true));
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (Control ctl in grdMatrix.Children)
            {
                if (ctl.GetType() == typeof(CheckBox))
                    ((CheckBox)ctl).IsChecked = false;
            }
        }

        private void btnAan_Click(object sender, RoutedEventArgs e)
        {
            foreach (char c in txt.Text)
            {
                SendLetter(c);
                System.Threading.Thread.Sleep(250);
            }
            _isChanged = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _timer.Stop();
            _serialPort.Close();
        }
    }
}

