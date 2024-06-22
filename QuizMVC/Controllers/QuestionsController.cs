using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizMVC.Data.Enum;
using QuizMVC.Helpers;
using QuizMVC.Models;
using QuizMVC.Models.Entities;

namespace QuizMVC.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var hasQuizes = await _context.Quizzes.AnyAsync();
            ViewBag.HasQuizzes = hasQuizes;
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            // Pass Quizlist to View
            ViewBag.QuizList = GetQuizzes();

            ViewBag.QuestionCategoryList = QuestionCategoryEnum.GeneralKnowledge.ToSelectList();
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,QuestionType,QuestionText,A_Answer,B_Answer,C_Answer,D_Answer,A_Correct,B_Correct,C_Correct,D_Correct")] Question question)
        {
            if (ModelState.IsValid)
            {
                // Check if at least one checkbox checked
                if (!question.A_Correct && !question.B_Correct && !question.C_Correct && !question.D_Correct)
                {
                    ModelState.AddModelError("", "Select at least one correct answer!");
                    ViewBag.QuizList = GetQuizzes();
                    ViewBag.QuestionCategoryList = question.QuestionCategory.ToSelectList();
                    return View(question);
                }
                var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == question.QuizId);
                if (quiz != null)
                {
                    question.QuizName = quiz.Name;
                }
                else
                {
                    ModelState.AddModelError("", "Can't add question to selected quiz. Try again later.");
                    question.QuizName = null;
                }
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.QuizList = GetQuizzes();
            ViewBag.QuestionCategoryList = question.QuestionCategory.ToSelectList();
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                                         .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            // Pass list to View
            ViewBag.QuizList = GetQuizzes(question.QuizId);
            ViewBag.QuestionCategoryList = question.QuestionCategory.ToSelectList();

            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuizId,QuestionType,QuestionText,A_Answer,B_Answer,C_Answer,D_Answer,A_Correct,B_Correct,C_Correct,D_Correct")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == question.QuizId);
                    if (quiz != null)
                    {
                        question.QuizName = quiz.Name;
                    }
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Updated successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.QuizList = GetQuizzes(question.QuizId);
            ViewBag.QuestionCategoryList = question.QuestionCategory.ToSelectList();
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

        private List<SelectListItem> GetQuizzes(int? quizId = null)
        {
            var quizzes = _context.Quizzes.ToList();

            var quizListItems = quizzes.Select(q => new SelectListItem
            {
                Value = q.Id.ToString(),
                Text = q.Name
            }).ToList();

            if (quizId.HasValue)
            {
                var itemToRemove = quizListItems.FirstOrDefault(q => q.Value == quizId.Value.ToString());
                if (itemToRemove != null)
                {
                    quizListItems.Remove(itemToRemove);
                    quizListItems.Insert(0, itemToRemove);
                }
            }
            else
            {
                quizListItems.Insert(0, new SelectListItem { Value = "", Text = "Select quiz..." });
            }

            return quizListItems;
        }
    }
}
