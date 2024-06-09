using Microsoft.Extensions.Primitives;
using QuizMVC.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizMVC.Models.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; } = default!;

        public string? QuizName { get; set; }

        public QuestionCategoryEnum QuestionCategory { get; set; } = default!;

        public string QuestionText { get; set; } = default!;
        public string A_Answer { get; set; } = default!;
        public string B_Answer { get; set; } = default!;
        public string C_Answer { get; set; } = default!;
        public string D_Answer { get; set; } = default!;
        public bool A_Correct { get; set; } = default!;
        public bool B_Correct { get; set; } = default!;
        public bool C_Correct { get; set; } = default!;
        public bool D_Correct { get; set; } = default!;
    }
}
