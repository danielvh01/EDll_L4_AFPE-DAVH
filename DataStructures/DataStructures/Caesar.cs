using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class Caesar : ICaesarCipher
    {
        private Dictionary<int, string> dictionary;
        private Dictionary<int, string> dictionary2;
        public Caesar()
        {
            dictionary = new Dictionary<int, string>();
            dictionary2 = new Dictionary<int, string>();

        }
        public byte[] Cipher(string text,string key)
        {
            string cipheredText = "";
            byte[] result = new byte[text.Length];
            if (text != "" || text != null || text.Length != 0)
            {
                for (int i = 0; i < 256; i++)
                    dictionary.Add(i, ((char)i).ToString());

                for (int i = 0; i < 256; i++)
                {
                    if (!dictionary2.ContainsValue(key[i].ToString()) && i < key.Length)
                    {
                        dictionary2.Add(i, key[i].ToString());
                    }
                    else if(!dictionary2.ContainsValue(((char)i).ToString()))
                    {
                        dictionary2.Add(i, ((char)i).ToString());
                    }
                }

                for (int i = 0; i < text.Length; i++)
                {
                    result += 
                }
            }
        }

    }
}
