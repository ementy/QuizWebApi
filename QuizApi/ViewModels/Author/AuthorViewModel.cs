using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.ViewModels.Author
{
    public class AuthorViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
