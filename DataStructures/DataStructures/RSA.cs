using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class RSA
    {
        public RSA()
        {

        }

        #region Generate private and public Keys
        // int #1 and #2 are the public key (e,N) and int #3 and #4 the private key (d,N)
        public (int,int,int,int) GenKeys(int p,int q)
        {
            int N = p * q;
            int phiN = (p - 1)*(q - 1);
            int e = 2;

            while (e < N || !Coprime(N,e) && !Coprime(phiN, e))
            {
                e++;
            }
            int d = GetD(1,phiN,e);
            return (e,N,d,N);
        }

        int GetD(int k, int phiN, int e)
        {
            int d = 0;            
            int deq = 1;
            //Choose d
            while (deq != 0)
            {
                deq = k * phiN + 1 % e;
                k++;
            }
            d = ((k - 1) * phiN + 1) / e;
            if (d == e)
            {
                return GetD(k, phiN, e);
            }
            else 
            {
                return d;                
            }
        }

        int MCD(int value1, int value2)
        {
            while (value1 != 0 && value2 != 0)
            {
                if (value1 > value2)
                    value1 %= value2;
                else
                    value2 %= value1;
            }
            return Math.Max(value1, value2);
        }

        bool Coprime(int value1, int value2)
        {
            return MCD(value1, value2) == 1;
        }

        #endregion

        #region Cipher / Decipher method

        public byte[] RSApher(byte[] content, int k1,int k2)
        {
            List<byte> result = new List<byte>();
            foreach (byte c in content) {
                result.Add((byte)(Math.Pow(c,k1) % k2));
            }
            return result.ToArray();
        }

        #endregion
    }
}
