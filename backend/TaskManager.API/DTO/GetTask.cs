namespace TaskManager.API.DTO
{
    public class GetTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
