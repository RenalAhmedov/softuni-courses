namespace TaskBoardApp.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.Board;

public class Board
{
    public int Id { get; init; }

    [Required]
    [MaxLength(MaxBoardName)]
    public string Name { get; init; }

    public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
}
