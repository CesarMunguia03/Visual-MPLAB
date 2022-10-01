using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//lineas que permiten depurar
using System.Diagnostics;
using usbGenericHidCommunications;

namespace MOEE
{
    public partial class USBControl : usbGenericHidCommunication
    {
        public USBControl(int vid, int pid)
           : base(vid, pid)
        {
        }
        private const bool A = true; 

        //public deleted
        public String CollectDebug()
        {
            //Declaracion de buffer de salida
            Byte[] outputBuffer = new Byte[65];

            //declaracion de buffer de entreda
            Byte[] inputBuffer = new Byte[65];

            //El byte 0 del buffer debe ser 0
            outputBuffer[0] = 0;

            //el byte 1 del buffer almacenado el comando que que le idicara
            //a la tarjeta que la pc esta solisitando datos de depuracion
            outputBuffer[1] = 0x10;

            //se envia el buffer de salida al dispositivo ui
            writeRawReportToDevice(outputBuffer);

            //se lee cual fue la respuesta del dispositivo
            readSingleReportFromDevice(ref inputBuffer);

            //el byte 1 del buffer de entrada contiene el numero
            //de caracteres que han sido transferidos

            //si el byte 1 tiene 0 se retornara a un string vacio
            if (inputBuffer[1] == 0 | inputBuffer[1] == 255) return String.Empty;

            //la siguiente linea que convierte los byte del buffer de entrada en
            //un string de una longitud adecuada
            String s = System.Text.ASCIIEncoding.ASCII.GetString(inputBuffer, 2,
                inputBuffer[1]);

            return s;

           // if (input[1] == true)
           // {

            //}
          
        }
        public bool SendASCII(string strOutputBuffer)
        {
            Byte[] outputBuffer = new Byte[65];

            outputBuffer[0] = 0;
            outputBuffer[1] = 0x11;

            byte[] bytesToSendBuffer = Encoding.ASCII.GetBytes(strOutputBuffer);

            for (int i = 2; i < outputBuffer.Length; i++)
            {
                if (i < bytesToSendBuffer.Length + 2)
                    outputBuffer[i] = bytesToSendBuffer[i - 2];
                else
                    //outputBuffer[i] = 0;
                    break;
            }

            bool success = writeRawReportToDevice(outputBuffer);

            return success;
        }
        public bool toggleRB1()
        {
            Byte[] outputBuffer = new Byte[65];

            outputBuffer[0] = 0;
            outputBuffer[1] = 0x21;

            bool success = writeRawReportToDevice(outputBuffer);

            return success;
        }
        public bool toggleRB2()
        {
            Byte[] outputBuffer = new Byte[65];

            outputBuffer[0] = 0;
            outputBuffer[1] = 0x22;

            bool success = writeRawReportToDevice(outputBuffer);

            return success;
        }
        public bool toggleRB3()
        {
            Byte[] outputBuffer = new Byte[65];

            outputBuffer[0] = 0;
            outputBuffer[1] = 0x23;

            bool succees = writeRawReportToDevice(outputBuffer);

            return succees;
        }

        public void inputRead(ref byte porta, ref byte portb, ref byte portc,
        ref byte portd, ref byte porte)
        {
            //Con el comando 0x23 se leeran varias entradas al mismo tiempo
            Byte[] outputBuffer = new Byte[65];
            Byte[] inputBuffer = new Byte[65];

            outputBuffer[0] = 0;
            outputBuffer[1] = 0x23;

            bool succes = writeRawReportToDevice(outputBuffer);

            if (succes)
            {
                succes = readSingleReportFromDevice(ref inputBuffer);
                inputBuffer[0] = 0;
                if (succes)
                {
                    if (inputBuffer[1] == 0x23)
                    {
                        porta = inputBuffer[2];
                        portb = inputBuffer[3];
                        portc = inputBuffer[4];
                        portd = inputBuffer[5];
                        porte = inputBuffer[6];

                    }
                }
            }
        }
    }
}
