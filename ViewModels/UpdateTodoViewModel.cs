using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class UpdateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}