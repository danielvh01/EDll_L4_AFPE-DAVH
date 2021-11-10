using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class Caesar : ICipher<string>
    {
        private Dictionary<char, int> dictionary;
        private Dictionary<int, char> dictionary2;

        public Caesar()
        {
            dictionary = new Dictionary<char, int>();
            dictionary2 = new Dictionary<int, char>();
        }

        public byte[] Cipher(byte[] text, string key)
        {
            byte[] result = new byte[text.Length];
            if (text != null || text.Length != 0)
            {
                //Fills the base dictionary
                for (int i = 0; i < 256; i++)
                    dictionary.Add((char)i,i);                
                //Fills the second dictionary, starting with the key
                for (int i = 0; i < key.Length; i++)
                {
                    if (!dictionary2.ContainsValue(key[i]))
                    {
                        dictionary2.Add(dictionary2.Count, key[i]);
                    }
                }
                //Once the key values has been added to the dictionary, the rest of the base dictionary will be added
                for (int i = 0; i < 256; i++)
                {
                    if (!dictionary2.ContainsValue((char)i))
                    {
                        dictionary2.Add(dictionary2.Count, (char)i);
                    }
                }
                //The text that is going to be ciphered is taken from the one that match between the value of the second dictionary and the key of the base dictionary.
                for (int i = 0; i < text.Length; i++)
                {
                    result[i] = (byte)dictionary2[(char)dictionary[(char)text[i]]];
                }
                return result;
            }
            else 
            {
                return default;
            }
        }

        //Same process of the cipher but now in reverse. The base dictionary is the one that contains the key and then, The text that 
        //is going to be deciphered is taken from the one that match between the value of the base dictionary(now with the key sent by the user) 
        //and the key of the second dictionary (which is actually the base dictionary).
        public byte[] Decipher(byte[] text, string key)
        {
            byte[] result = new byte[text.Length];
            if (text != null || text.Length != 0)
            {               
                for (int i = 0; i < key.Length; i++)
                {
                    if (!dictionary.ContainsKey(key[i]))
                    {
                        dictionary.Add(key[i],dictionary.Count);
                    }
                }

                for (int i = 0; i < 256; i++)
                {
                    if (!dictionary.ContainsKey((char)i))
                    {
                        dictionary.Add((char)i, dictionary.Count);
                    }
                }

                for (int i = 0; i < 256; i++)
                    dictionary2.Add(i, (char)i);

                for (int i = 0; i < text.Length; i++)
                {
                    result[i] = (byte)dictionary[(char)dictionary2[(int)text[i]]];
                }
                return result;

            }
            else
            {
                return default;
            }
        }


    }
}
