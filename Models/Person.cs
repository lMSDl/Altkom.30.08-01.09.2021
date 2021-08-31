using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person : Entity, ICloneable
    {
        //private string _firstName;

        //public string GetFirstName()
        //{
        //    return _firstName;
        //}
        //public void SetFirstName(string value)
        //{
        //    _firstName = value;
        //}
        //public Person()
        //{
        //}

        //public Person(string firstName, string lastName)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //}

        //public Person(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName)
        //{
        //    BirthDate = birthDate;
        //}

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public int SomeInt { get; set; }

        [JsonIgnore]
        public string OptionalProperty { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
