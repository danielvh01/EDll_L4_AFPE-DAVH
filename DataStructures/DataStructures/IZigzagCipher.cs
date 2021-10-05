using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public interface IZigzagCipher
    {
        byte[] Cipher(string text, int Key);
        byte[] Decipher(string text, int key);
    }
}
