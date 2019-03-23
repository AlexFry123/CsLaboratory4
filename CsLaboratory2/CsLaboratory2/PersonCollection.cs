using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsLaboratory2
{
    class PersonCollection : IDataStorage
    {
        private readonly List<Person> _users;

        internal PersonCollection() 
        {
            try
            {
                _users = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<Person>();
            }
        }

        public bool UserExists(string login)
        {
            return true;
        }

        public Person GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Name == login);
        }

        public void AddUser(Person user)
        {
            _users.Add(user);
            SaveChanges();
        }

        public void DeleteUser(Person user)
        {
            _users.Remove(user);
            SaveChanges();
        }

        public List<Person> UsersList
        {
            get { return _users.ToList(); }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }

    }
}
