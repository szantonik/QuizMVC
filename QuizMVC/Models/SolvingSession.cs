namespace QuizMVC.Models
{
    public class SolvingSession
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string QuizDesc { get; set; }
        public List<QuestionAnswers> Questions { get; set; } = new List<QuestionAnswers>();
    }
}
