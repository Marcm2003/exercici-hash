using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace hash01
{
    class Program
    {
        static void Main(string[] args)
        {
            String filePath = null;
            Console.Write("Introdueix la ruta de l'arxiu: ");
            while (string.IsNullOrEmpty(filePath))
            {
                Console.Clear();
                Console.Write("Introdueix la ruta de l'arxiu: ");
                filePath = Console.ReadLine();
            }

            Console.WriteLine("Selecciona una opció:");
            Console.WriteLine("1. Generar hash");
            Console.WriteLine("2. Verificar integritat de l'arxiu");

            int opcio = 0;
            while (opcio != 1 && opcio != 2)
            {
                Console.Write("Introdueix l'opció: ");
                if (!int.TryParse(Console.ReadLine(), out opcio))
                {
                    opcio = 0;
                }
            }

            try
            {
                if (opcio == 1)
                {
            
                    string contingutArxiu = File.ReadAllText(filePath);

                    byte[] bytesArxiu = Encoding.UTF8.GetBytes(contingutArxiu);

                    using (SHA512Managed SHA512 = new SHA512Managed())
                    {
                        byte[] resultatHash = SHA512.ComputeHash(bytesArxiu);

                        
                        String textOut = BitConverter.ToString(resultatHash).Replace("-", string.Empty);

                        string rutaHash = Path.ChangeExtension(filePath, ".SHA");
                        File.WriteAllText(rutaHash, textOut);

                        Console.WriteLine("Hash de l'arxiu {0}", filePath);
                        Console.WriteLine(textOut);
                        Console.ReadKey();
                    }
                }
                else if (opcio == 2)
                {
                    string rutaHash = Path.ChangeExtension(filePath, ".SHA");
                    string hashEsperat = File.ReadAllText(rutaHash);

                    string contingutArxiu = File.ReadAllText(filePath);

                    byte[] bytesArxiu = Encoding.UTF8.GetBytes(contingutArxiu);

                    using (SHA512Managed SHA512 = new SHA512Managed())
                    {
                        byte[] resultatHash = SHA512.ComputeHash(bytesArxiu);

                        String hashActual = BitConverter.ToString(resultatHash).Replace("-", string.Empty);

                        if (hashEsperat == hashActual)
                        {
                            Console.WriteLine("L'arxiu {0} no ha estat modificat", filePath);
                        }
                        else
                        {
                            Console.WriteLine("L'arxiu {0} ha estat modificat", filePath);
                        }

                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: {0}", ex.Message);
                Console.ReadKey();
            }
        }
    }
}
