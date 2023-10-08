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
            Console.Write("Enter file path: ");
            while (string.IsNullOrEmpty(filePath))
            {
                Console.Clear();
                Console.Write("Enter file path: ");
                filePath = Console.ReadLine();
            }

            try
            {
                string fileContents = File.ReadAllText(filePath);
            // Convertim l'string a un array de bytes
                byte[] bytesIn = Encoding.UTF8.GetBytes(fileContents);
            // Instanciar classe per fer hash

            // fent servir using ja es delimita el seu àmbit i no cal fer dispose
                using (SHA512Managed SHA512 = new SHA512Managed())
            {
                // Calcular hash
                byte[] hashResult = SHA512.ComputeHash(bytesIn);
                
                // Si volem mostrar el hash per pantalla o guardar-lo en un arxiu de text
                // cal convertir-lo a un string
                    String textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                    string hashFilePath = Path.ChangeExtension(filePath, ".SHA");
                    File.WriteAllText(hashFilePath, textOut);

                    Console.WriteLine("Hash of file {0}", filePath);
                    Console.WriteLine(textOut);
                    Console.ReadKey();
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