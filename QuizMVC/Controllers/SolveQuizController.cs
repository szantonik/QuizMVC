using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMVC.Helpers;
using QuizMVC.Models;
using QuizMVC.Models.Entities;

namespace QuizMVC.Controllers
{
    public class SolveQuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolveQuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Start(int id)
        {
            var quiz = await _context.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == id);
            if (quiz == null) return NotFound();

            var solvingSession = new SolvingSession
            {
                QuizId = quiz.Id,
                QuizName = quiz.Name,
                QuizDesc = quiz.Description,
                Questions = GetQuestionsByQuizID(id).Select(q => new QuestionAnswers
                {
                    QuestionId = q.Id,
                    QuestionText = q.QuestionText,
                    Answers = new List<string> { q.A_Answer, q.B_Answer, q.C_Answer, q.D_Answer },
                    CorrectAnswers = new List<bool> { q.A_Correct, q.B_Correct, q.C_Correct, q.D_Correct}
                }).ToList()
            };

            HttpContext.Session.SetObject("SolvingSession", solvingSession);

            return RedirectToAction("Solve");
        }

        public IActionResult Solve()
        {
            var solvingSession = HttpContext.Session.GetObject<SolvingSession>("SolvingSession");
            if (solvingSession == null) return RedirectToAction("Index", "Home");

            return View(solvingSession);
        }

        [HttpPost]
        public IActionResult SubmitAnswers(List<QuestionAnswers> questions)
        {
            var solvingSession = HttpContext.Session.GetObject<SolvingSession>("SolvingSession");
            if (solvingSession == null) return RedirectToAction("Index", "Home");

            foreach (var submittedQuestion in questions)
            {
                var question = solvingSession.Questions.FirstOrDefault(q => q.QuestionId == submittedQuestion.QuestionId);
                if (question != null)
                {
                    question.UserAnswers = submittedQuestion.UserAnswers;
                }
            }

            HttpContext.Session.SetObject("SolvingSession", solvingSession);

            return RedirectToAction("Finish");
        }

        public IActionResult Finish()
        {
            var solvingSession = HttpContext.Session.GetObject<SolvingSession>("SolvingSession");
            if (solvingSession == null) return RedirectToAction("Index", "Home");

            ViewBag.Score = 0;
            foreach (var question in solvingSession.Questions)
            {
                if (question.UserAnswers.SequenceEqual(question.CorrectAnswers))
                {
                    ViewBag.Score++;
                }
            }

            HttpContext.Session.Remove("SolvingSession");
            return View("Results", solvingSession);
        }

        private List<Question> GetQuestionsByQuizID (int quiz_id)
        {
            var questions = _context.Questions.Where(q => q.QuizId == quiz_id).ToList();
            foreach(var q in questions)
            {
                Console.WriteLine($"{q.A_Correct}, {q.B_Correct}, {q.C_Correct}, {q.D_Correct}");
            }
            return questions;
        }
    }
}
