using System;
using Phonebook.Crud.Models;
using Phonebook.Crud.Services.Contacts;

namespace Phonebook.Crud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IContactService contactService = new ContactService();

            Console.WriteLine("Welcome to Phonebook CRUD Application!");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Show Contacts");
            Console.WriteLine("3. Delete Contact");
            Console.WriteLine("4. Exit");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add new contact");
                        Contact newContact = new Contact();
                        contactService.AddContact(newContact);
                        break;
                    case "2":
                        Console.WriteLine("Displaying all contacts");
                        contactService.ShowContacts();
                        break;
                    case "3":
                        Console.WriteLine("Deleting a contact...");
                        Console.Write("Enter the ID of the contact to delete: ");
                        int contactIdToDelete = Convert.ToInt32(Console.ReadLine());
                        bool isDeleted = contactService.DeleteContact(contactIdToDelete);
                        if (isDeleted)
                        {
                            Console.WriteLine($"Contact with ID: {contactIdToDelete} deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Contact with ID: {contactIdToDelete} not found.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exiting the application...");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option (1-4).");
                        break;
                }
            }
        }
    }
}
