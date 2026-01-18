using System.IO;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace PraceSTextovymiSoubory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /**/ // <-- stačí když jednu ze dvou * smažete a celá studijní část se zakomentuje (a naopak)
            #region Studijní část
            // 1. OTEVŘENÍ SOUBORU
            // 1.1 Adresa k souboru (File PATH):
            //  - RELATIVNÍ vzhledem k EXE souboru (ve složce ...\Váš projekt\bin\Debug\netX.Y\)
            //      - př. "vstupni_soubor.txt" 
            //      - př. @"vstupy\1.txt" -> použijeme zavináč, abychom nemuseli zdvojovat zpětná lomítka (vedou obvykle na vyjádření speciálních znaků)
            //      - př. @"..\vstupy\1.txt" -> jdeme o složku zpět a v ní hledáme složku "vstupy" se souborem 1.txt
            //  - ABSOLUTNÍ adresa: 
            //      - př. @"C:\Users\mysak\Documents\03_TEACHING\02_SEMINAR_Z_PROGRAMOVANI\25_26\PrgSem2\vstup.txt"

            // 1.2 Blok using
            //  - soubor otevíráme vždy v rámci bloku "using", který zajišťuje uzavření souboru a to vždy (i kdyby program v průběhu spadl)
            //  - syntaktická zkratka za try - finally blok (bez catch!) - ve finally se uzavírá soubor a odemyká se pro další používání
            
            // 2. ČTENÍ ZE SOUBORU
            // 2.1 File
            //  - vhodný pro menší až střední soubory, které můžeme přečíst celé najednou (vejdou se celé do paměti)
            //  - neumí číst po částech
            //  - nemusí být v using bloku (uzavření si hlídá sám)
            string text = File.ReadAllText("data.txt");
            string[] lines = File.ReadAllLines("data.txt");

            // 2.2 StreamReader 
            //  - při volání konstuktoru specifikujeme adresu k souboru
            //  - vhodný pro větší soubory a čtení/zápis po částech

            using (StreamReader sr = new StreamReader(@"vstupy\0_vstup.txt"))
            {
                //  - nabízí funkce pro čtení souboru (pokračuje od místa, kde se skončilo číst):
                char prvniZnak = (char)sr.Read(); // přečteme 1 znak, vrací int (-1, když konec souboru)
                string zbytekPrvniRadky = sr.ReadLine(); // od prvního znaku dočte zbytek až do oddělovače řádku
                string zbytekSouboru = sr.ReadToEnd(); // přečte zbytek souboru od msíta, kde se skončilo se čtením

            }

            // 3. ZÁPIS DO SOUBORU
            // 3.1 File
            //  - rozlišujeme mezi WRITE (přepsání) a APPEND (připsání)
            File.WriteAllText("vystup.txt", "Ahoj světe!\nToto je nový soubor."); // \n - oddělovač řádků
            File.AppendAllText("vystup.txt", "Přidáváme další řádku.\n");
            string[] radky = { "První řádek", "Druhý řádek", "Třetí řádek" };
            File.WriteAllLines("vystup.txt", radky); // PŘEPÍŠE několik řádků souborů
            string[] dalsiRadky = { "Čtvrtý řádek", "Pátý řádek" };
            File.AppendAllLines("vystup.txt", dalsiRadky);

            // 3.2 StreamWriter
            using (StreamWriter sw = new StreamWriter("vystup.txt", false)) // false -> PŘEPISOVÁNÍ, true -> PŘIPISOVÁNÍ (append)
            {
                // Upozornění: pokud soubor s takovým jménem neexistoval, vytvoří ho

                sw.Write("Ahoj"); // píše se tam, kde se skončilo
                sw.WriteLine("Toto je nová řádka"); // přidá se oddělení řádku
            }

            // 4. PROBLÉMY
            //  - při práci se soubory může dojít k různým výjimkám -> používáme try-catch bloky
            try
            {
                using (StreamReader sr = new StreamReader(@"vstupy\0_vstup.txt"))
                {
                    // čtení
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Soubor nebyl nalezen: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nemáme práva k souboru nebo složce.
                // Např. zápis do chráněného systému(C:\Windows...) nebo čtení souboru, který je uzamčen pro čtení.
                Console.WriteLine("Nemáš oprávnění: " + ex.Message);
            }
            catch (IOException ex)
            {
                // obecná I/O (input/output) chyba (např. soubor je uzamčen jiným procesem)
                Console.WriteLine("Chyba vstupu/výstupu: " + ex.Message);
            }
            catch
            {
                Console.WriteLine("Nastala nějaká chyba");
            }
            #endregion
            /**/

            #region Praktická část
            // Následující úkoly vyřešte pomocí programování. Metodu "kouknu a vidim" si nechte pro kontrolu.
            // Výsledek si nechte vypsat do konzole a uveďte odpověď v komentáři.
            // Vyřešte vždy i všechny podotázky.

            // Složku vstupni_soubory si přesuňte do ...\bin\Debug\netX.Y\. 
            // Dále si ji otevřte ve VS Code a sledujte obsah souborů. 
            // Doporučení: pro důkladnější pátrání si stáhněte extension do VS Code Inspector Hex nebo Hex Editor.



            //
            using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\2.txt"))
            {
                sw.WriteLine("Ahoj \tsvěte!\n");
            }
            // (10b) 1. Jaký je počet znaků v souboru 1.txt a jaký v 2.txt?
            // Zkontrolujte s VS Code a vysvětlete rozdíly.
                // Windows používá Line feed \n a Carriage return \r na konci rádku, což jsou 2 znaky, Linux jen \n
            // Tip: Při Debugování uvidíte všchny čtené znaky.
            using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\1.txt"))
            {
            string text1 = File.ReadAllText("1.txt");
            Console.WriteLine($"Počet znaků v 1.txt: {text1.Length}");
            }
            
            using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\2.txt"))
            {
            string text2 = File.ReadAllText("2.txt");
            Console.WriteLine($"Počet znaků v 2.txt: {text2.Length}");    
            }
            

            // (10b) 2. Jaký je počet znaků v souboru 1.txt, když pomineme bílé znaky?
            // Tip: Struktura Char má statickou funkci IsWhiteSpace().            
              using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\1.txt"))
              {int Charnotwhite = 0;
            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c) == false)
                {
                    Charnotwhite++;
                }
        
            }
            Console.WriteLine($"Počet nebílých znaků v 1.txt: {Charnotwhite}");
            
            //
            using (StreamWriter sw = new StreamWriter(@"..\..\..\..\vstupni_soubory\4.txt"))
            {
                sw.WriteLine("1");
                sw.WriteLine("2");
                sw.WriteLine("3");
            }
            using (StreamWriter sw = new StreamWriter(@"..\..\..\..\vstupni_soubory\5.txt"))
            {
                sw.Write("1\n2\n3");
            }
            // (5b) 3. Jaké znaky (vypište jako integery) jsou použity pro oddělení řádků v souboru 3.txt?
            // Porovnejte s 4.txt a 5.txt.
            // Jakým znakům odpovídají v ASCII tabulce? https://www.ascii-code.com/
            // Zde se stačí podívat do VS Code a napsat sem odpověď, není potřeba nic programovat.
            // 3.txt 13 10 (CR LF)
            // 4.txt 13 10 (CR LF)
            // 5.txt 10 (LF)


            // (10b) 4. Kolik slov má soubor 6.txt?
            // Za slovo teď považujme neprázdnou souvislou posloupnost nebílých znaků oddělené bílými.
            // Tip: Split defaultně odděluje na základě libovolných bílých znaků, ale je tam jeden háček.. jaký?
            // V souboru je vidět 52 slov.
            using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\6.txt"))
            {
                string text6 = File.ReadAllText("6.txt");
                string[] words6 = text6.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine($"Počet slov v 6.txt: {words6.Length}");

            }


            // (15b) 5. Zapište do souboru 7.txt slovo "řeřicha". Povedlo se? 
            // Vypište obsah souboru do konzole. V čem je u konzole problém a jak ho spravit?
            // Jaké kódování používá C#? Kolik bytů na znak?
                    string czWord = "řeřicha";
                  using (StreamWriter sw = new StreamWriter(@"..\..\..\..\vstupni_soubory\7.txt"))
                {
                    File.WriteAllText("7.txt", czWord);

                }
                using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\7.txt"))
                {
                                string read7 = File.ReadAllText("7.txt");
                }

                  using (StreamWriter sw = new StreamWriter(@"..\..\..\..\vstupni_soubory\7.txt"))
                {
                                Console.OutputEncoding = Encoding.UTF8;  
                                // Default Encoding neumí všechny UTF8 znaky
                                // C# string ma 16 bitů (UTF16)
                                Console.WriteLine($"Obsah 7.txt: {read7}");
                }


            // (25b) 6. Vypište četnosti jednotlivých slov v souboru 8.txt do souboru 9.txt ve formátu slovo:četnost na samostatný řádek.
            // Tentokrát však slova nejprve očištěte od diakritiky a všechna písmena berte jako malá (tak je i ukládejte do slovníku).
            // Tip: Využijte slovník: Dictionary<string, int> slova = new Dictionary<string, int>();
            using (StreamReader sw = new StreamReader(@"..\..\..\..\vstupni_soubory\8.txt"))
            {
                            string text8 = File.ReadAllText("8.txt");
            }
            char[] separators = { ' ', '.', ',', '?', '!', '\n', '\r', '\t', ':', ';' };
            string[] rawWords8 = text8.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            foreach (string word in rawWords8)
            {
                string processed = word.ToLower();
                processed = RemoveDiacritics(processed);

                if (wordCounts.ContainsKey(processed))
                {
                    wordCounts[processed]++;
                }
                else
                {
                    wordCounts[processed] = 1;
                }
            }

            using (StreamWriter sw = new StreamWriter("9.txt"))
            {
                foreach (var pair in wordCounts)
                {
                    sw.WriteLine($"{pair.Key}:{pair.Value}");
                }

            // (+15b) Bonus: Vypište četnosti jednotlivých znaků abecedy (malá a velká písmena) v souboru 8.txt do konzole.
            Dictionary<char, int> charCounts = new Dictionary<char, int>();
            
            foreach (char c in text8)
            {
                if (char.IsLetter(c))
                {
                    if (charCounts.ContainsKey(c))
                        charCounts[c]++;
                    else
                        charCounts[c] = 1;
                }
            }
            
            foreach (var pair in charCounts.OrderBy(x => x.Key))
            {
                Console.WriteLine($"'{pair.Key}': {pair.Value}");
            }
            #endregion
        }
    }
}
