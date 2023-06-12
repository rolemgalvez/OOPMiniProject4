using HomeworkThirtyThree.Library;
using HomeworkThirtyThree.Library.Models;
using System;
using System.Collections.Generic;

namespace HomeworkThirtyThree
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PersonModel> people = GetPeople();
            List<CarModel> cars = GetCars();

            DataAccess<PersonModel> peopleData = new DataAccess<PersonModel>();
            peopleData.BadEntryFound += PeopleData_BadEntryFound;
            peopleData.SaveToCSV(people, @"D:\source\me\github\OOPMiniProject4\SavedFiles\people.csv");

            DataAccess<CarModel> carsData = new DataAccess<CarModel>();
            carsData.BadEntryFound += CarsData_BadEntryFound;
            carsData.SaveToCSV(cars, @"D:\source\me\github\OOPMiniProject4\SavedFiles\cars.csv");

            Console.ReadLine();
        }

        private static void CarsData_BadEntryFound(object sender, CarModel e)
        {
            Console.WriteLine($"Bad entry found for {e.Manufacturer} {e.Model}");
        }

        private static void PeopleData_BadEntryFound(object sender, PersonModel e)
        {
            Console.WriteLine($"Bad entry found for {e.FirstName} {e.LastName}");
        }

        private static List<CarModel> GetCars()
        {
            return new List<CarModel>
            {
                new CarModel { Manufacturer = "Toyota", Model = "CorollaDarn" },
                new CarModel { Manufacturer = "Toyota", Model = "Highlander" },
                new CarModel { Manufacturer = "Fordheck", Model = "Mustang" }
            };
        }

        private static List<PersonModel> GetPeople()
        {
            return new List<PersonModel>
            {
                new PersonModel { FirstName = "Juan", LastName = "DarnCruz", Email = "juan@email.com" },
                new PersonModel { FirstName = "Martin", LastName = "Yu", Email = "martin@email.com" },
                new PersonModel { FirstName = "Maria", LastName = "HeckSy", Email = "maria@email.com" }
            };
        }
    }
}
