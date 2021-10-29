using System;
using System.Collections.Generic;

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
            for (int i = 0; i < phiN; i++)
            {
                if (e < N && Coprime(N, e) == true && Coprime(phiN, e) == true)
                {
                    break;
                }
                else
                {
                    e++;
                }
            }
            int d = GetD(1,phiN,e);
            return (d,N,e,N);
        }

        int GetD(int k, int phiN, int e)
        {            
            int d = 0;            
            int deq = 1;
            //Choose d
            while (deq != 0)
            {
                deq = (k * phiN + 1) % e;
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

        static int __gcd(int a, int b)
        {            
            if (a == 0 || b == 0)
                return 0;
            
            if (a == b)
                return a;
            
            if (a > b)
                return __gcd(a - b, b);

            return __gcd(a, b - a);
        }


        bool Coprime(int a, int b)
        {

            if (__gcd(a, b) == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region Cipher / Decipher method

        public byte[] RSApher(byte[] content, int k1,int k2)
        {
            List<byte> result = new List<byte>();
            foreach (byte c in content) {
                result.Add((byte)(ModularPower(c,k1,k2)));
            }
            return result.ToArray();
        }

        int ModularPower(int x, int y, int p)
        {
            int res = 1;
            x = x % p;                       

            if (x == 0){
                return 0;            
            }
            
            while (y > 0)
            {
                if ((y & 1) != 0){
                    res = (res * x) % p;
                }                
                y = y >> 1;
                x = (x * x) % p;
            }
            return res;
        }

        #endregion
    }
}
