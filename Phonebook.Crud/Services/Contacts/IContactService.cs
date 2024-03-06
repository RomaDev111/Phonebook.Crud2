using Phonebook.Crud.Brokers.Storages;
using Phonebook.Crud.Models;
using Phonebook.Crud.Brokers.Storages;

namespace Phonebook.Crud.Services.Contacts
{
    internal interface IContactService
    {
        Contact AddContact(Contact contact);
        bool DeleteContact(int contactIdToDelete);
        void ShowContacts();
    }
}
