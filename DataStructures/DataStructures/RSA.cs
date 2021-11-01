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
        // int #1 and #2 are the public key (d,N) and int #3 and #4 the private key (e,N)
        public (double,int,int) GenKeys(int p,int q)
        {
            int N = p * q;
            int phiN = (p - 1)*(q - 1);
            int e = 2;
            while (e < phiN && (MCD(e, N) != 1 || MCD(e, phiN) != 1))
            {
                e++;
            }            
            int d = GetD(phiN,e);
            return (e,N,d);
        }

        //static int modInverse(int a, int m)
        //{

        //    for (int x = 1; x < m; x++)
        //        if (((a % m) * (x % m)) % m == 1)
        //            return x;
        //    return 1;
        //}


        //int GetD(int k, int phiN, int e)
        //{            
        //    int d = 0;            
        //    int deq = 1;
        //    //Choose d
        //    while (deq != 0)
        //    {
        //        deq = (k * phiN + 1) % e;
        //        k++;
        //    }
        //    d = ((k - 1) * phiN + 1) / e;
        //    if (d == e)
        //    {
        //        return GetD(k, phiN, e);
        //    }
        //    else 
        //    {
        //        return d;                
        //    }
        //}

        int GetD(int phiN, int e)
        {
            int d = 0;
            int k = 2;
            //Choose d
            while ((d*e)%phiN != 1){
                d = (1 + k * phiN) / e;
                k++;
            }            
            return d;            
        }

        int MCD(int a, int b)
        {
            int result = 0;
            do
            {
                result = b;
                b = a % b;
                a = result;
            }
            while (b != 0);
            return result;
        }

        //int __gcd(int a, int b)
        //{
        //    if (a == 0 || b == 0)
        //        return 0;

        //    if (a == b)
        //        return 0;

        //    if (a > b)
        //    {
        //        int div = a / b;
        //        a -= b * div;
        //        return a;
        //    }
        //    else
        //    {
        //        int div = b / a;
        //        b -= a * div;
        //        return b;
        //    }
        //}

        #endregion

        #region Cipher / Decipher method

        public byte[] RSApher(byte[] content, int k1, int k2)
        {

            List<byte> result = new List<byte>();
            foreach (byte c in content)
            {
                if(c > k2)
                {
                    return null;
                }
                result.Add((byte)ModularPower(c, k1, k2));
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
