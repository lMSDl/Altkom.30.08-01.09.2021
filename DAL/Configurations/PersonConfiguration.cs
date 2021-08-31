using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            //HasKey(x => x.Id);

            Property(x => x.FirstName).HasMaxLength(15);
            Property(x => x.LastName).IsRequired();

            Ignore(x => x.OptionalProperty);
        }
    }
}
