using Contracts.DbModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.DbModels
{
    public class Author : BaseModel<int>
    {
        //the Id is initiated by the BaseModel with the provided type (int)

        //constructor initates the collection of the Quotes. One author can have more than one quote
        public Author()
        {
            this.Quotes = new HashSet<Quote>();
        }

        //An author must have a name. The filed contains the full name, cannot be null and the max lenght is 100 symbols
        [Required]
        [StringLength(100)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        //One author can have more than 1 quote. collection of quotes initiated by the constructor
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
