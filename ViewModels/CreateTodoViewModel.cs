using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}