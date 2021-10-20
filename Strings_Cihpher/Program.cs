using System;
using DataStructures;
using System.Text;

namespace Strings_Cihpher
{
    class Program
    {
        static void Main(string[] args)
        {
            bool seguir = true;
            do
            {
                Console.WriteLine("¡Bienvenido!");
            menu:
                Console.WriteLine("Seleccione el método de cifrado que desea utilizar:");
                Console.WriteLine("1. Cifrado cesar");
                Console.WriteLine("2. Cifrado Zigzag");
                Console.WriteLine("3. SDES");
                string opcion = Console.ReadLine();
                Console.Clear();
                byte[] textToCipher;
                switch (opcion)
                {
                    case "1":
                        ICaesarCipher chiper = new Caesar();
                        Console.WriteLine("Escriba el texto que desea cifrar:");
                        string content = Console.ReadLine();
                        Console.WriteLine("Escriba la clave para el cifrado");
                        string key = Console.ReadLine();
                        Console.WriteLine("Su texto cifrado es:");
                        textToCipher = new byte[content.Length];
                        for (int i = 0; i < content.Length; i++)
                        {
                            textToCipher[i] = (byte)content[i];
                        }
                        byte[] chipherText = chiper.Cipher(textToCipher, key);
                        string result = "";
                        for(int i = 0; i < chipherText.Length; i++)
                        {
                            result += (char)chipherText[i];
                        }
                        Console.WriteLine(result);
                        break;
                    case "2":
                        IZigzagCipher chiperz = new Zigzag();
                        Console.WriteLine("Escriba el texto que desea cifrar:");
                        string contentz = Console.ReadLine();
                        Console.WriteLine("Escriba la clave para el cifrado");
                        int keyz = 0;
                        if (int.TryParse(Console.ReadLine(), out keyz))
                        {
                            textToCipher = new byte[contentz.Length];
                            for (int i = 0; i < contentz.Length; i++)
                            {
                                textToCipher[i] = (byte)contentz[i];
                            }
                            Console.WriteLine("Su texto cifrado es:");
                            byte[] chipherTextz = chiperz.Cipher(textToCipher, keyz);
                            string resultz = "";
                            for (int i = 0; i < chipherTextz.Length; i++)
                            {
                                resultz += (char)chipherTextz[i];
                            }
                            Console.WriteLine(resultz);
                        }
                        else
                        {
                            bool error = true;
                            do
                            {
                                Console.WriteLine("La llave para este método debe de ser un número entero, intentelo de nuevo por favor.");
                                Console.ReadLine();
                                Console.WriteLine("Escriba la clave para el cifrado");
                                if (int.TryParse(Console.ReadLine(), out keyz))
                                {
                                    textToCipher = new byte[contentz.Length];
                                    for (int i = 0; i < contentz.Length; i++)
                                    {
                                        textToCipher[i] = (byte)contentz[i];
                                    }
                                    Console.WriteLine("Su texto cifrado es:");
                                    byte[] chipherTextz = chiperz.Cipher(textToCipher, keyz);
                                    string resultz = "";
                                    for (int i = 0; i < chipherTextz.Length; i++)
                                    {
                                        resultz += (char)chipherTextz[i];
                                    }
                                    Console.WriteLine(resultz);
                                    error = false;
                                }
                            } while (error);

                        }
                        break;
                    case "3":
                        IZigzagCipher chipers = new Zigzag();
                        Console.WriteLine("Escriba el texto que desea cifrar:");
                        string contents = Console.ReadLine();
                        Console.WriteLine("Escriba la clave para el cifrado");
                        int keys = 0;
                        if (int.TryParse(Console.ReadLine(), out keys) && keys < 1023)
                        {
                            textToCipher = new byte[contents.Length];
                            for (int i = 0; i < contents.Length; i++)
                            {
                                textToCipher[i] = (byte)contents[i];
                            }
                            Console.WriteLine("Su texto cifrado es:");
                            byte[] chipherTexts = chipers.Cipher(textToCipher, keys);
                            string results = "";
                            for (int i = 0; i < chipherTexts.Length; i++)
                            {
                                results += (char)chipherTexts[i];
                            }
                            Console.WriteLine(results);
                        }
                        else
                        {
                            bool error = true;
                            do
                            {
                                Console.WriteLine("Llave inválida, intentelo de nuevo por favor.");
                                Console.ReadLine();
                                Console.WriteLine("Escriba la clave para el cifrado");
                                if (int.TryParse(Console.ReadLine(), out keyz))
                                {
                                    textToCipher = new byte[contents.Length];
                                    for (int i = 0; i < contents.Length; i++)
                                    {
                                        textToCipher[i] = (byte)contents[i];
                                    }
                                    Console.WriteLine("Su texto cifrado es:");
                                    byte[] chipherTexts = chipers.Cipher(textToCipher, keys);
                                    string results = "";
                                    for (int i = 0; i < chipherTexts.Length; i++)
                                    {
                                        results += (char)chipherTexts[i];
                                    }
                                    Console.WriteLine(results);
                                    error = false;
                                }
                            } while (error);

                        }
                        break;
                    default:
                        Console.WriteLine("La opción ingresada es inválida, intentelo de nuevo por favor.");
                        goto menu;
                }
                Console.ReadLine();
            final:
                Console.WriteLine("¿Desea seguir probando mas cifrados? [S/N]");
                string opcionF = Console.ReadLine();
                if (opcionF == "N")
                {
                    seguir = false;
                }
                else if(opcionF != "S")
                {
                    Console.WriteLine("La opción ingresada es inválida, intentelo de nuevo por favor.");
                    goto final;
                }
            } while (seguir);
        }
    }
}
