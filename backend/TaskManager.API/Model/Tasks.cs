using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Model
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
