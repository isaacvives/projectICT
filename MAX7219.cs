using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    /// <summary>
    /// Klasse met een aantal commandos voor de MAX7219.
    /// Bekijk de datasheet voor meer informatie:
    /// https://www.analog.com/media/en/technical-documentation/data-sheets/max7219-max7221.pdf
    /// </summary>
    internal class MAX7219
    {
        private static UInt16 poweroff = 0x0C00;

        public static UInt16 POWEROFF
        {
            get { return poweroff; }
        }

        private static UInt16 poweron = 0x0C01;

        public static UInt16 POWERON
        {
            get { return poweron; }
        }
        public static UInt16 Intensity(double intensity)
        {
            return (UInt16)(0x0A00 + intensity);
        }
        public static UInt16 DecodeMode(double mode)
        {
            return (UInt16)(0x0900 + mode);
        }
        public static UInt16 Scan(double num)
        {
            return (UInt16)(0x0B00 + num);
        }
        public static UInt16 TestMode(bool mode)
        {
            if (mode)
            {
                return (UInt16)(0x0F01);
            }
            else
            {
                return (UInt16)(0x0F00);
            }
        }
    }
}
