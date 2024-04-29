using ConsoleTools;
using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Numerics;

namespace QV596H_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Car")
            {
                Console.Write("Enter Car Name: ");
                string name = Console.ReadLine();
                rest.Post(new Car() { Model = name }, "Car");
            }
            else if (entity=="Brands")
            {
                Console.WriteLine("Enter a brand name: ");
                string name = Console.ReadLine();
                rest.Post(new Brands() { BrandName = name }, "Brands");

            }
            else if(entity=="Rental")
            {
                Console.Write("Enter Rental Date (YYYY-MM-DD HH:MM:SS): ");
                DateTime rentalDate = (DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None));
                rest.Post(new Rental() {RentalDate=rentalDate }, "rental");
            }
        }
        static void List(string entity)
        {
            if (entity == "Car")
            {
                List<Car> cars = rest.Get<Car>("Car");
                foreach (var item in cars)
                {
                    Console.WriteLine(item.CarId + ": " + item.Model);
                }
            }
            else if(entity=="Brands")
                {
                List<Brands> brands = rest.Get<Brands>("Brands");
                foreach(var item in brands)
                {
                    Console.WriteLine(item.BrandId+ ": "+ item.BrandName);
                }
            }
            else if(entity=="Rental")
            {
                List<Rental> rentals = rest.Get<Rental>("rental");
                foreach (var item in rentals)
                {
                    Console.WriteLine(item.RentalId+": "+item.RentalDate);
                }
            }
            Console.ReadLine();
            
        }
        static void Update(string entity)
        {
            if (entity == "Car")
            {
                Console.Write("Enter car's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Car one = rest.Get<Car>(id, "Car");
                Console.Write($"New name [old: {one.Model}]: ");
                string name = Console.ReadLine();
                one.Model = name;
                rest.Put(one, "Car");
                
            }
           
            else if (entity == "Brands")
            {
                Console.Write("Enter brand's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Brands one = rest.Get<Brands>(id, "Brands");
                Console.Write($"New name [old: {one.BrandName}]: ");
                string name = Console.ReadLine();
                one.BrandName = name;
                rest.Put(one, "Brands");
            }
            else if (entity == "Rental")
            {
                Console.Write("Enter a rental id to update: ");
                int id = int.Parse(Console.ReadLine());
                Rental one = rest.Get<Rental>(id, "rental");
                Console.Write($"New name [old: {one.CarId}]: ");
                int caird = int.Parse(Console.ReadLine());
                one.CarId = caird;
                rest.Put(one, "rental");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Car")
            {
                Console.Write("Enter Car's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Car");
            }
            else if (entity == "Brands")
            {
                Console.Write("Enter brands's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Brands");
            }
            else if (entity == "Rental")
            {
                Console.Write("Enter rental id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Rental");
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:55149/", "car");

            var carSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Car"))
                .Add("Create", () => Create("Car"))
                .Add("Delete", () => Delete("Car"))
                .Add("Update", () => Update("Car"))
                .Add("Exit", ConsoleMenu.Close);

            var brandsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Brands"))
                .Add("Create", () => Create("Brands"))
                .Add("Delete", () => Delete("Brands"))
                .Add("Update", () => Update("Brands"))
                .Add("Exit", ConsoleMenu.Close);

            var rentalSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Rental"))
                .Add("Create", () => Create("Rental"))
                .Add("Delete", () => Delete("Rental"))
                .Add("Update", () => Update("Rental"))
                .Add("Exit", ConsoleMenu.Close);



            var menu = new ConsoleMenu(args, level: 0)
                .Add("Car", () => carSubMenu.Show())
                .Add("Brands", () => brandsSubMenu.Show())
                .Add("Rental", () => rentalSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);
 
            menu.Show();

        }
    }
}
