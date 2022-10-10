namespace TaskManagement.Api.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
    }
}
