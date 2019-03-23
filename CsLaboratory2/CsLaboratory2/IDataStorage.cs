using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsLaboratory2
{
    internal interface IDataStorage
    {
        bool UserExists(string login);

        Person GetUserByLogin(string login);

        void AddUser(Person user);
        void DeleteUser(Person user);
        List<Person> UsersList { get; }
    }
}
