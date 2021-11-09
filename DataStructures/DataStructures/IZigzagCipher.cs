using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public interface IZigzagCipher : ICipher<int>
    {
        byte[] Cipher(byte[] text, int key);
        byte[] Decipher(byte[] text, int key);
    }
}
