using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.DTO;
using TaskManager.API.Model;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TMDbContext _context;

        public TaskController(TMDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<GetTask>> GetTasks()
        {
            try
            {
                var resp = await _context.Tasks.ToListAsync();
                var tasks = resp.Select(task => new GetTask        //Map task to AllTasks
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    IsCompleted = task.IsCompleted
                }).ToList();

                return tasks;
            }
            catch (Exception ex) {

                Console.WriteLine($"Error retrieving tasks: {ex.Message}");
                return new List<GetTask>();
            }
           
            
        }

        [HttpGet("{taskid}")]
        public async Task<GetTask> GetTask(int taskid)
        {
            try
            {
                var resp = await _context.Tasks.SingleOrDefaultAsync(task => task.TaskId == taskid);
                var task = new GetTask
                {
                    TaskId = resp.TaskId,
                    Title = resp.Title,
                    IsCompleted = resp.IsCompleted
                };
                return task;
            } catch (Exception ex) {
                Console.WriteLine($"Error fetching task: {ex.Message}");
                return null;

            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTask task)
        {
            try {
                var T = new Model.Tasks
                {
                    Title = task.Title,
                    IsCompleted = false

                };

                _context.Tasks.AddAsync(T);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTask), new { taskid = T.TaskId }, task);

            } 
            catch (Exception ex) {
                Console.WriteLine($"Error creating task: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            
        }

        [HttpPut("{taskid}")]
        public async Task UpdateTask(int taskid ,UpdateTask updateTask)
        {
            var task = await _context.Tasks.FindAsync(taskid);
            if (task == null)
            {
                throw new Exception("Task not found");
            }


            task.IsCompleted = updateTask.IsCompleted;
            await _context.SaveChangesAsync();
        }

        [HttpDelete("{taskid}")]
        public async Task DeleteTask(int taskid)
        {
            var task = await _context.Tasks.FindAsync(taskid);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }


    }
}
