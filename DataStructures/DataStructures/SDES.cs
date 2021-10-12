using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class SDES
    {
        
        string P10(string key)
        {
            int[] P10out = { 3, 7, 5, 1, 2, 9, 0, 4, 8, 6 };
            string P10array = "";
            for (int i = 0; i < 10; i++)
            {
                P10array+= key[P10out[i]];
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
            int[] P8out = {9,4,2,8,3,0,6,1 };
            string P8array = "";
            for (int i = 0; i < 8; i++)
            {
                P8array += LeftShiftarray[P8out[i]];
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
            int[] IPout = { 3, 0, 6, 1, 7, 2, 4, 5 };
            string IP = "";
            for (int i = 0; i < 8; i++)
            {
                IP += array[IPout[i]];
            }
            return IP;
        }

        string IP_1(string array)
        {
            int[] IP_1out = { 1, 3, 5, 0, 6, 7, 2, 4 };
            string IP_1 = "";
            for (int i = 0; i < 10; i++)
            {
                IP_1 += array[IP_1out[i]];
            }
            return IP_1;
        }

        string EP(string array)
        {
            int[] EPout = { 2, 1, 3, 4, 3, 2, 4, 1 };
            string EP = "";
            for (int i = 0; i < 10; i++)
            {
                EP += array[EPout[i]];
            }
            return EP;
        }

        string P4(string array)
        {
            int[] P4out = { 3,1,4,2 };
            string P4 = "";
            for (int i = 0; i < 4; i++)
            {
                P4 += array[P4out[i]];
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

    }
}
