# ExempleHasH!!
MP09 UF1 NF1 A1 
Exemple de funcionament del hash en C#

## Introducció
Introduir la criptografia en els  nostres codis, no implica haver de programar algoritmes, és més, es recomana no fer-ho, sinó utilitzar llibreries estàndard sòlides i comprovades. 
A .NET existeix la llibreria o espai de noms, System.Security.Criptography, mentre que en Java l’opció més estesa és la Java Criptography Extension (JCE).

## Algoritmes de hash
Els algoritmes de hash s’utilitzen per obtenir una sortida única a partir d'un determinat missatge d'entrada. Aquesta sortida és de mida fixa, independentment de la mida de l'entrada i no existeix una funció inversa, que permeti obtenir l'entrada a partir de la sortida. Aquesta característica fa que aquestes funcions s'utilitzin per controlar la integritat dels missatges o arxius, ja que qualsevol modificació, per petita que sigui, produirà una sortida totalment diferent.

Els principals algoritmes de hash són:
- MD5 genera resums de 128 bits i el missatge d'entrada té un límit de 2<sup>64</sup>-1 bits 
- SHA-1 el resum és de 160 bits i el missatge d'entrada té el mateix límit que en MD5
- SHA-256 aquí el resum és de 256 bits i manté la mateixa limitació d'entrada
- SHA-384 com es pot deduir el resum serà de 384 bits, però el límit d'entrada s'amplia fins 2<sup>128</sup>-1 bits
- SHA-512 el resum s'amplia fins fins el 512 bits respecte l'anterior.

La mida del hash ens indica el conjunt finit de hashes possibles. Així amb 128 tindrem 2<sup>128</sup> = 3,4·10<sup>38</sup>, així que suposant que puc comprovar 10 milions de missatges per segon, trigaria milions d’anys a provar-los tots. Ara és un conjunt finit, per tant hi ha una probabilitat d’obtenir el mateix hash per dos missatges diferents. De fet, el nombre d’intents és sensiblement inferior.

Els algoritmes MD5 i SHA-1 es consideren obsolets perquè pateixen diverses vulnerabilitats de implementació, de manera que avui dia les implementacions utilitzades són les variants més modernes de SHA.

## El programa

````C#

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace hash01
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var textIn = "";
            while (string.IsNullOrEmpty(textIn))
            {
                Console.Clear();
                Console.Write("Entra text: "); 
                textIn = Console.ReadLine();
            }

            // Convertim l'string a un array de bytes
            byte[] bytesIn = Encoding.UTF8.GetBytes(textIn);
            // Instanciar classe per fer hash
            SHA512Managed SHA512 = new SHA512Managed();
            // Calcular hash
            byte[] hashResult = SHA512.ComputeHash(bytesIn);

            // Si volem mostrar el hash per pantalla o guardar-lo en un arxiu de text
            // cal convertir-lo a un string

             String textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);
            Console.WriteLine("Hash del text{0}", textIn);
            Console.WriteLine(textOut);
            Console.ReadKey();

            // Eliminem la classe instanciada
            SHA512.Dispose();
        }
    }
}
````

### Comentaris sobre el codi
Es tracta d'un programa de consola que a partir d'un string introduït per teclat, mostra per pantalla el hash obtingut (algoritme SHA-512).

En primer lloc els using permeten carregar els espais de noms (bàsicament el mateix que feu amb els import de Java). Haurem d'afegir les biblioteques de criptografia de .NET

````C#
using System.Security.Cryptography;
````

Per llegir un string en C# des de consola, és tant senzill com utilitzar _Console.ReadLine()_. Després caldrà fer la conversió a bytes que serà amb el que treballen els algoritmes de hash:

````C#
byte[] bytesIn = Encoding.UTF8.GetBytes(textIn);
````

Per poder fer el hash haurem d'utilitzar un objecte de la classe _SHA512Managed_ que està definida a l'espai de noms que hem agregat. A continuació, calcularem el hash dels bytes obtinguts del string introduït

````C#
SHA512Managed SHA512 = new SHA512Managed();
byte[] hashResult = SHA512.ComputeHash(bytesIn);
````
Si volem que el hash es converteixi a un string de caràcters hexadecimals, usarem la funció _BitConverter_. Per defecte, separa cada byte convertit amb guions, per eliminar-los es fa servir el _Replace_.

````C#
 String textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);
````
Finalment, una bona pràctica, els objectes que creem, en aquest cas SHA512, els eliminem de la memòria quan ja no són necessaris.

````C#
 SHA512.Dispose();
````

## Activitat a realitzar
En primer lloc cloneu-vos el repositori al vostre equip. Ho podeu fer utilitzant l'eina SourceTree, el propi Visual Studio o directament via comandes fent:

````
git clone URI del vostre repo creat
````

Un cop tingueu el projecte clonat, l'obriu amb el Visual Studio i l'executeu. Comproveu el funcionament i mireu d'entendre el funcionament del codi.

Ara que ja enteneu què fa el programa i com ho fa, caldrà que el modifiqueu per tal que enlloc de realitzar el hash d'un string entrat per teclat,  ho faci d'un fitxer de text. El primer pas, serà crear una nova branca que anomenareu v1. Serà en aquesta branca on realitzareu la modificació de l'arxiu. Penseu que estareu llegint arxius, per tant poden passar coses: l'arxiu no existeix, no es pot llegir, etc. per tant, cal que tingueu present utilitzar try/catch per capturar els errors.

Per llegir un fitxer caldrà que crideu a l'espai de noms System.IO. Tenim diverses maneres de llegir un fitxer de text, per fitxers de tipus text, el mètode més senzill és llegir-los directament i emmagatzemar el contingut a un string. Per això teniu el mètode File.ReadAllText (investigueu com funciona).

Un cop tingueu aquest apartat funcionant, crearem una branca v2, on anirem un pas més enllà. Guardarem el hash resultant en un arxiu que s'ha d'anomenar igual que l'arxiu original, però amb l'extensió .SHA. Com ja podeu intuir, disposem d'un mètode anomenat File.WriteAllText que ens permetrà guardar el hash en format string en un arxiu. De nou, cal que controleu els possibles errors.

Com apartat final, crearem una branca final v3, on implementarem la darrera funcionalitat. A més de crear el hash d'un fitxer, ens ha de permetre comprovar la integritat d'un fitxer. Per fer aquesta funcionalitat, caldrà que calculeu el hash del fitxer i el compareu amb el hash emmagatzemat a l'arxiu. Com ara el programa té dues funcionalitats, caldrà fer un senzill menú o usar arguments per poder seleccionar una acció o l'altra.

**Punt final**

La darrera versió que tingeu funcional, la enviarem a la branca master, per fer això, no ho farem pas amb un merge, sinó amb un pull request que farem des de la pàgina de GitHub.




