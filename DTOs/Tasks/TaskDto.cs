namespace TaskManagementSystem.API.DTOs.Tasks
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty; 

        public int Status { get; set; }

        public string StatusName { get; set; } = string.Empty;
    }
}
