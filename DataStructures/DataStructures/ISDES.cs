using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    interface ISDES : ICipher<int>
    {
        byte[] Cipher(byte[] message, int key);
        byte[] Decipher(byte[] message, int key);
    }
}
