using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; //HOZZÁADNI

namespace WindowsFormsApp1
{
    internal static class Statistics
    {
        static List<Book> books; //ebbe a listába menti az adatokat

        public static void Run() //futtatás
        {
            Console.WriteLine("Statisztika");
            try
            {
                //Feladatok
                ReadAllBooks(); //kapcsolat létrehozása
                Console.WriteLine("500 oldalnál hosszabb könyvek:" + HosszabbMint500Oldal()); //második feladat
                Console.WriteLine("{0} 1950-nél régebbi könyv.", RegebbiKonyv() ? "Van" : "Nincs"); //harmadik feladat

                Book longest = GetLongestBook(); //negyedik feladat
                Console.WriteLine("Leghosszabb könyv adataiai: " +
                    "\r\nSzerző: {0}" +
                    "\r\nCím: {1}" +
                    "\r\nKiadási éve: {2}" +
                    "\r\nOldalszám: {3}", longest.Author, longest.Title, longest.Publish_year, longest.PageCount);

                Console.WriteLine("Legtöbb könyvvel rendelkező szerző: {0}", LegjobbSzerzo()); //ötödik feladat
                Console.Write("Adjon meg egy könyv címet: ");
                string title = Console.ReadLine();
                Kereses(title);
                Console.ReadKey();
            }
            catch (MySqlException ex) //ha adatkapcsolati hiba van 
            {
                Console.WriteLine("Hiba történt az adatbázis kapcsolat kiépítésekor:");
                Console.WriteLine(ex.Message);
            }
        }
        private static void ReadAllBooks() //BookServicből betölti az adatbázis kapcsolatot
        {
            BookService bookService = new BookService();
            books = bookService.GetBooks(); //elmenti a listába
        }
        private static object HosszabbMint500Oldal() //második feladat
        {
            int darab = 0;
            foreach (Book book in books)
            {
                if (book.PageCount > 500)
                {
                    darab++;
                }
            }
            return darab;
        }
        private static bool RegebbiKonyv() //Harmadik feladat
        {
            int index = 0;
            while (index < books.Count && !(books[index].Publish_year < 1950))
            {
                index++;
            }
            return index < books.Count;
            //Ha egyenlő akkor lefutott végig, tehát nincs, ha kisebb, akkor van
        }
        private static Book GetLongestBook() //negyedik feladat
        {
            Book longest = books[0];
            for (int i = 1; i < books.Count; i++)
            {
                if (books[i].PageCount > longest.PageCount)
                {
                    longest = books[i];
                }
            }
            return longest;
        }
        private static string LegjobbSzerzo() //ötödik feladat
        {
            Dictionary<string, int> legjobbSzerzo = new Dictionary<string, int>();
            foreach (Book book in books)
            {
                if (!legjobbSzerzo.ContainsKey(book.Author))
                {
                    legjobbSzerzo[book.Author] = 0;
                }
                legjobbSzerzo[book.Author]++;
            }
            string szerzo = null;
            foreach (KeyValuePair<string, int> item in legjobbSzerzo)
            {
                if (szerzo == null)
                {
                    szerzo = item.Key;
                }
                if (item.Value > legjobbSzerzo[szerzo])
                {
                    szerzo = item.Key;
                }
            }
            return szerzo;
        }
        private static void Kereses(string title) //hatodik feladat
        {
            int index = 0;
            while (index < books.Count && books[index].Title != title)
            {
                index++;
            }
            if (index < books.Count)
            {
                Console.WriteLine("A megadott könyv szerzője: {0}", books[index].Author);
            }
            else
            {
                Console.WriteLine("Nincs ilyen könyv.");
            }
        }
    }
}
