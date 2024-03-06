using System;
using System.Runtime.InteropServices;
using Phonebook.Crud.Brokers.Loggings;
using Phonebook.Crud.Brokers.Storages;
using Phonebook.Crud.Models;


namespace Phonebook.Crud.Services.Contacts
{
    internal class ContactService : IContactService 
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;


        public ContactService()
        {
            this.storageBroker = new FileStorageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public Contact AddContact(Contact contact)
        {
            return contact is null 
                ? CreateAndLogInvalidContact()
                : ValidateAndAddContact(contact);
        }
      
        public void ShowContacts()
        {
            Contact[] contacts = this.storageBroker.ReadAllContacts();

            foreach (Contact contact in contacts)
            {
                this.loggingBroker.LogInformation($"{contact.Id}. {contact.Name} - {contact.Phone}");
            }
            this.loggingBroker.LogInformation("===End of contacts===");
        }
        public bool DeleteContact(int contactId)
        {
            bool isDeleted = this.storageBroker.DeleteContact(contactId);
            if (!isDeleted)
            {
                this.loggingBroker.LogError($"Contact with Id: {contactId} not found.");
            }
            return isDeleted;
        }
        private Contact CreateAndLogInvalidContact()
        {
            this.loggingBroker.LogError("Contact is invalid");
            return new Contact();
        }

        private Contact ValidateAndAddContact(Contact contact) 
        {
            if (contact.Id is 0 ||   String.IsNullOrWhiteSpace(contact.Name) || String.IsNullOrWhiteSpace(contact.Phone))
            {
                this.loggingBroker.LogError("Contact details missing.");
                return new Contact();
            }
            else
            {
                return this.storageBroker.AddContact(contact);
            }
        }
}
}
