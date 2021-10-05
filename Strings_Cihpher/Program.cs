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
                string opcion = Console.ReadLine();
                Console.Clear();
                switch (opcion)
                {
                    case "1":
                        ICaesarCipher chiper = new Caesar();
                        Console.WriteLine("Escriba el texto que desea cifrar:");
                        string content = Console.ReadLine();
                        Console.WriteLine("Escriba la clave para el cifrado");
                        string key = Console.ReadLine();
                        Console.WriteLine("Su texto cifrado es:");
                        Console.WriteLine(Encoding.GetEncoding(28591).GetString(chiper.Cipher(content, key)));
                        break;
                    case "2":
                        IZigzagCipher chiperz = new Zigzag();
                        Console.WriteLine("Escriba el texto que desea cifrar:");
                        string contentz = Console.ReadLine();
                        Console.WriteLine("Escriba la clave para el cifrado");
                        int keyz = 0;
                        if (int.TryParse(Console.ReadLine(), out keyz))
                        {
                            Console.WriteLine("Su texto cifrado es:");
                            Console.WriteLine(Encoding.GetEncoding(28591).GetString(chiperz.Cipher(contentz, keyz)));
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
                                    Console.WriteLine("Su texto cifrado es:");
                                    Console.WriteLine(chiperz.Cipher(contentz, keyz));
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
