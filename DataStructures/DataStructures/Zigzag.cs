using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class Zigzag : ICipher<int>
    {
        public Zigzag()
        {

        }
        public byte[] Cipher(byte[] text, int Key)
        {
            double x = Convert.ToDouble(text.Length) / Convert.ToDouble(2 + 2*(Key - 2));
            int cantOlas = Convert.ToInt32(Math.Round(x, MidpointRounding.ToPositiveInfinity));
            byte[,] map = new byte[Key, cantOlas * 2];
            int cont = 0;
            for (int i = 0; i < (cantOlas * 2); i++)
            {
                for (int j = 0; j < Key; j++)
                {
                    if ((i % 2) == 0)
                    {
                        if (cont < text.Length)
                        {
                            map[j, i] = (byte)text[cont++];
                        }
                        else
                        {
                            map[j, i] = 0;
                        }
                    }
                    else if (j < (Key - 1) && j > 0)
                    {
                        if (cont < text.Length)
                        {
                            map[((Key - 1) - j), i] = (byte)text[cont++];
                        }
                        else
                        {
                            map[(Key - 1) - j, i] = 0;
                        }
                    }
                }
            }

            byte[] result = new byte[(2 + 2 * (Key - 2)) * cantOlas];

            cont = 0;
            for (int i = 0; i < Key; i++)
            {
                for(int j = 0; j < (cantOlas * 2); j++)
                {
                    if((j % 2) == 0)
                    {
                        result[cont++] = map[i,j];
                    }
                    else if(i < (Key - 1) && i > 0)
                    {
                        result[cont++] = map[i,j];
                    }
                }
            }
            return result;
        }

        public byte[] Decipher(byte[] text, int key)
        {
            int cantOlas = text.Length / (2 + 2 * (key - 2));
            byte[,] map = new byte[key, cantOlas*2];
            int newLength = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] != 0)
                {
                    newLength++;
                }
            }
            for(int i = 0; i < cantOlas; i++)
            {
                map[0, i * 2] = (byte)text[i];
                map[key - 1, i * 2] = (byte)text[text.Length - (cantOlas - i)];
            }
            int cont = cantOlas;
            for(int i = 1; i < key - 1; i++)
            {
                for(int j = 0; j < (cantOlas * 2); j++)
                {
                    map[i,j] = (byte)text[cont++];
                }
            }
            byte[] result = new byte[newLength];
            cont = 0;
            for(int i = 0; i < cantOlas*2 && cont < result.Length; i++)
            {
                for(int j = 0; j < key && cont < result.Length; j++)
                {
                    if((i % 2) == 0)
                    {
                        result[cont++] = map[j , i];
                    }
                    else if(j < (key - 1) && j > 0)
                    {
                        result[cont++] = map[(key - 1) - j, i];
                    }
                }
            }
            return result;
        }
    }
}
