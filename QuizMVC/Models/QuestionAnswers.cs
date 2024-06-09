namespace QuizMVC.Models
{
    public class QuestionAnswers
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }

        public List<bool> CorrectAnswers { get; set; } = new List<bool>();
        public List<bool> UserAnswers { get; set; } = new List<bool>();

        public bool A_selected { get; set; }
        public bool B_selected { get; set; }
        public bool C_selected { get; set; }
        public bool D_selected { get; set; }
    }
}
