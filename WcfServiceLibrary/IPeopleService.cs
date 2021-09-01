using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPeopleService" in both code and config file together.
    [ServiceContract]
    public interface IPeopleService
    {
        [OperationContract]
        int Create(Person entity);
        [OperationContract]
        Person Read(int id);
        [OperationContract]
        IEnumerable<Person> ReadAll();
        [OperationContract]
        bool Update(int id, Person entity);
        [OperationContract]
        bool Delete(int id);
    }
}
