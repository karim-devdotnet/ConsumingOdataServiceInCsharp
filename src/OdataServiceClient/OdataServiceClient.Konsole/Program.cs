using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.SampleService.Models.TripPin;

namespace OdataServiceClient.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //ListPeople().Wait();
            ListPeople();
        }

        static async Task ListPeopleAsync()
        {
            var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
            var context = new DefaultContainer(new Uri(serviceRoot));

            IEnumerable<Person> people = await context.People.ExecuteAsync();
            foreach (var person in people)
            {
                Console.WriteLine("{0} {1}", person.FirstName, person.LastName);
            }
            var test = context.GetMetadataUri();
            Console.WriteLine(test);
        }
        
        static void ListPeople()
        {
            var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
            var context = new DefaultContainer(new Uri(serviceRoot));

            IEnumerable<Person> people = context.People.Execute();
            foreach (var person in people)
            {
                Console.WriteLine("{0} {1}", person.FirstName, person.LastName);
            }
            var test = context.GetMetadataUri();
            Console.WriteLine(test);
        }
    }
}
