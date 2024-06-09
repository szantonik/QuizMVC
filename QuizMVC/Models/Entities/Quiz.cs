using System.ComponentModel.DataAnnotations;

namespace QuizMVC.Models.Entities
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }


        // Navigation property
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
