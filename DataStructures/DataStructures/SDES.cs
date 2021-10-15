using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataStructures
{
    public class SDES
    {
        string[] P10out;
        string[] P8out;
        string[] P4out;
        string[] EPout;
        string[] IPout;
        string[] IP_1out;

        string[,] SBOXo = new string [4,4]

            { { "01","00","11","10"},
              { "11","10","01","00"},
              { "00","10","01","11"},
              { "11","01","11","10"} };

        string[,] SBOX1 = new string[4, 4]

            { { "00","01","10","11"},
              { "10","00","01","11"},
              { "11","00","01","00"},
              { "10","01","00","11"} };

        void PermutationConfigurator()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Configuration\Permutations.txt");
            string[] files = File.ReadAllLines(path);
            P10out = files[0].Split(",");
            P8out = files[1].Split(",");
            P4out = files[2].Split(",");
            EPout = files[3].Split(",");
            IPout = files[4].Split(",");
            IP_1out = files[5].Split(",");
        }
        #region methods used by cipher and decipher methods
        string P10(string key)
        {            
            string P10array = "";
            for (int i = 0; i < 10; i++)
            {
                P10array+= key[int.Parse(P10out[i])-1];
            }
            return P10array;
        }

        string LeftShift(string p10array)
        {
            string LeftShiftarray = "";
            for (int i = 0; i < 9; i++)
            {                
                LeftShiftarray += p10array[i + 1];
            }
            LeftShiftarray += p10array[0];
            return LeftShiftarray;
        }

        string P8(string LeftShiftarray)
        {            
            string P8array = "";
            for (int i = 0; i < 8; i++)
            {
                P8array += LeftShiftarray[int.Parse(P8out[i])-1];
            }
            return P8array;
        }

        string LeftShift2(string LeftShiftarray)
        {
            string LeftShift2array = "";
            for (int i = 0; i < 8; i++)
            {
                LeftShift2array += LeftShiftarray[i + 2];
            }
            LeftShift2array += LeftShiftarray[0];
            LeftShift2array += LeftShiftarray[1];
            return LeftShift2array;
        }


        string IP(string array)
        {            
            string IP = "";
            for (int i = 0; i < 8; i++)
            {
                IP += array[int.Parse(IPout[i])-1];
            }
            return IP;
        }

        string IP_1(string array)
        {            
            string IP_1 = "";
            for (int i = 0; i < 10; i++)
            {
                IP_1 += array[int.Parse(IP_1out[i])-1];
            }
            return IP_1;
        }

        string EP(string array)
        {            
            string EP = "";
            for (int i = 0; i < 10; i++)
            {
                EP += array[int.Parse(EPout[i])-1];
            }
            return EP;
        }

        string P4(string array)
        {            
            string P4 = "";
            for (int i = 0; i < 4; i++)
            {
                P4 += array[int.Parse(P4out[i])-1];
            }
            return P4;
        }

        string xor(string key1, string key2)
        {
            string result = "";
            for (int i = 0; i < key1.Length; i++)
            {
                if (key1[i] != key2[i])
                {
                    result += "1";
                }
                else {
                    result += "0";
                }
            }
            return result;
        }
        #endregion

        public byte[] Cipher(byte [] message, int key)
        {
            
        }

    }
}
