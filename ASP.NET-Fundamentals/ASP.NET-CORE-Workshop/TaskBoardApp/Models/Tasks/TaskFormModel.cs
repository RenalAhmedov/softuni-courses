using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.Task;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace TaskBoardApp.Models.Tasks
{
    public class TaskFormModel
    {
        public TaskFormModel()
        {
            this.Boards = new List<TaskBoardModel>();
        }

        [Required]
        [StringLength(MaxTaskTitle, MinimumLength = MinTaskTitle, ErrorMessage = "Title shoild be at least {2} characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(MaxTaskDescription, MinimumLength = MinTaskDescription, ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; set; }

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; }
    }
}