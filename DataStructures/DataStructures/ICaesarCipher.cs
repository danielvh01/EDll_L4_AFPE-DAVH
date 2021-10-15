using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public interface ICaesarCipher : ICipher<string>
    {
        byte[] Cipher(byte[] text, string Key);
        byte[] Decipher(byte[] text, string key);


    }
}
