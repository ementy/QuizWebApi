using Contracts.DbModels;
using System.ComponentModel.DataAnnotations;

namespace Entities.DbModels
{
    public class Quote : BaseModel<int>
    {
        //The Id of Quote is initiated by the BaseModel with the provided type


        //Content field cannot be null and has max length of 500 symbols
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        //Connecting property to tha author entity. One quote has only one author
        [Required]
        public virtual int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
