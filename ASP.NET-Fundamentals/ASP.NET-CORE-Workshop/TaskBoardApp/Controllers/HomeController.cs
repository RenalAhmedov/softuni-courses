using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;
using TaskBoardApp.Models.Home;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public HomeController(TaskBoardAppDbContext context)
        {
            this.data = context;
        }

        public async Task<IActionResult> Index()
        {
            var taskBoards = this.data.Boards
                .Select(b => b.Name)
                .Distinct();

            var tasksCounts = new List<HomeBoardModel>();
            foreach (var boardName in taskBoards)
            {
                var tasksInBoard = await this.data.Tasks
                    .Where(t => t.Board.Name == boardName)
                    .CountAsync();

                tasksCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TaskCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                userTasksCount = await this.data.Tasks
                    .Where(t => t.OwnerId == currentUserId)
                    .CountAsync();
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = await this.data.Tasks.CountAsync(),
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}