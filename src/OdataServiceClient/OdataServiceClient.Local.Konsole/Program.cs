using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OdataServiceServer.Models;

namespace OdataServiceClient.Local.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ListBooks().Wait();
        }

        static async Task ListBooks()
        {
            var serviceRoot = "https://localhost:44326/odata/";
            var context = new Default.Container(new Uri(serviceRoot));

            IEnumerable<Book> books = await context.Books.ExecuteAsync();
            foreach (var book in books)
            {
                Console.WriteLine("{0} {1}", book.Title, book.ISBN);
            }
        }
    }
}
