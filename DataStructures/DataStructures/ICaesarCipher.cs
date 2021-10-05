using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public interface ICaesarCipher
    {
        byte[] Cipher(string text, string Key);
        byte[] Decipher(string text, string key);


    }
}
