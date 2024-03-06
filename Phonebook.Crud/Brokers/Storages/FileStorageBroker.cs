using System;
using System.IO;
using Phonebook.Crud.Models;
using Phonebook.Crud.Services.Contacts;

namespace Phonebook.Crud.Brokers.Storages
{
    internal class FileStorageBroker : IStorageBroker
    {
        private const string FilePath = ("../../../Assets/Contacts.txt");
        public FileStorageBroker()
        {
            EnsureFileExists();
        }
        public Contact AddContact(Contact contact)
        {
            string contactLine = $"{contact.Id}*{contact.Name}*{contact.Phone}\n";

            File.AppendAllText(FilePath, contactLine);
            return contact;
        }

        public Contact[] ReadAllContacts()
        {

            string[] contactLines = File.ReadAllLines(FilePath);
            int contactLength = contactLines.Length;
            Contact[] contacts = new Contact[contactLength];

            for(int iterator = 0; iterator< contactLength; iterator++)
            {
                string contactLine = contactLines[iterator];
                string[] contactProperties = contactLine.Split('*');
                Contact contact = new Contact
                {
                    Id = Convert.ToInt32(contactProperties[0]),
                    Name = contactProperties[1],
                    Phone = contactProperties[2]
                };
                contacts[iterator] = contact;
            }
            return contacts;
        }
        public bool DeleteContact(int contactId)
        {
            Contact[] existingContacts = ReadAllContacts();

            bool contactExists = false;
            for (int i = 0; i < existingContacts.Length; i++)
            {
                if (existingContacts[i].Id == contactId)
                {
                    contactExists = true;
                    existingContacts[i] = null;
                    break;
                }
            }

            if (!contactExists)
                return false;

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                foreach (var contact in existingContacts)
                {
                    if (contact != null)
                    {
                        string contactLine = $"{contact.Id}*{contact.Name}*{contact.Phone}";
                        sw.WriteLine(contactLine);
                    }
                }
            }

            return true;
        }
        private void EnsureFileExists()
        {
            bool fileExists = File.Exists(FilePath);

            if (fileExists is false) 
            {
                File.Create(FilePath).Close();
            }
        }

       
    }
}
