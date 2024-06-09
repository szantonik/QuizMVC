using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizMVC.Data.Enum;
using QuizMVC.Models.Entities;

namespace QuizMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QuizzesMVC;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding data
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    Id = 1,
                    Name = "General Knowledge Quiz",
                    Description = "Test your general knowledge."
                },
                new Quiz
                {
                    Id = 2,
                    Name = "Science Quiz",
                    Description = "Test your science knowledge."
                }
            );

            modelBuilder.Entity<Question>().HasData(
                // General Knowledge Quiz Questions
                new Question
                {
                    Id = 1,
                    QuizId = 1,
                    QuizName = "General Knowledge Quiz",
                    QuestionCategory = QuestionCategoryEnum.GeneralKnowledge,
                    QuestionText = "What is the capital of France?",
                    A_Answer = "Paris",
                    B_Answer = "London",
                    C_Answer = "Berlin",
                    D_Answer = "Madrid",
                    A_Correct = true,
                    B_Correct = false,
                    C_Correct = false,
                    D_Correct = false
                },
                new Question
                {
                    Id = 2,
                    QuizId = 1,
                    QuizName = "General Knowledge Quiz",
                    QuestionCategory = QuestionCategoryEnum.GeneralKnowledge,
                    QuestionText = "Which planet is known as the Red Planet?",
                    A_Answer = "Earth",
                    B_Answer = "Mars",
                    C_Answer = "Jupiter",
                    D_Answer = "Saturn",
                    A_Correct = false,
                    B_Correct = true,
                    C_Correct = false,
                    D_Correct = false
                },
                new Question
                {
                    Id = 5,
                    QuizId = 1,
                    QuizName = "General Knowledge Quiz",
                    QuestionCategory = QuestionCategoryEnum.GeneralKnowledge,
                    QuestionText = "Who wrote 'To Kill a Mockingbird'?",
                    A_Answer = "Harper Lee",
                    B_Answer = "Mark Twain",
                    C_Answer = "J.K. Rowling",
                    D_Answer = "Ernest Hemingway",
                    A_Correct = true,
                    B_Correct = false,
                    C_Correct = false,
                    D_Correct = false
                },
                new Question
                {
                    Id = 6,
                    QuizId = 1,
                    QuizName = "General Knowledge Quiz",
                    QuestionCategory = QuestionCategoryEnum.GeneralKnowledge,
                    QuestionText = "What is the largest ocean on Earth?",
                    A_Answer = "Atlantic Ocean",
                    B_Answer = "Indian Ocean",
                    C_Answer = "Pacific Ocean",
                    D_Answer = "Arctic Ocean",
                    A_Correct = false,
                    B_Correct = false,
                    C_Correct = true,
                    D_Correct = false
                },
                // Science Quiz Questions
                new Question
                {
                    Id = 3,
                    QuizId = 2,
                    QuizName = "Science Quiz",
                    QuestionCategory = QuestionCategoryEnum.Science,
                    QuestionText = "What is the chemical symbol for water?",
                    A_Answer = "O2",
                    B_Answer = "H2O",
                    C_Answer = "CO2",
                    D_Answer = "HO",
                    A_Correct = false,
                    B_Correct = true,
                    C_Correct = false,
                    D_Correct = false
                },
                new Question
                {
                    Id = 4,
                    QuizId = 2,
                    QuizName = "Science Quiz",
                    QuestionCategory = QuestionCategoryEnum.Science,
                    QuestionText = "What is the speed of light?",
                    A_Answer = "300,000 km/s",
                    B_Answer = "150,000 km/s",
                    C_Answer = "100,000 km/s",
                    D_Answer = "50,000 km/s",
                    A_Correct = true,
                    B_Correct = false,
                    C_Correct = false,
                    D_Correct = false
                },
                new Question
                {
                    Id = 7,
                    QuizId = 2,
                    QuizName = "Science Quiz",
                    QuestionCategory = QuestionCategoryEnum.Science,
                    QuestionText = "What is the powerhouse of the cell?",
                    A_Answer = "Nucleus",
                    B_Answer = "Mitochondria",
                    C_Answer = "Ribosome",
                    D_Answer = "Endoplasmic Reticulum",
                    A_Correct = false,
                    B_Correct = true,
                    C_Correct = false,
                    D_Correct = false
                },
                new Question
                {
                    Id = 8,
                    QuizId = 2,
                    QuizName = "Science Quiz",
                    QuestionCategory = QuestionCategoryEnum.Science,
                    QuestionText = "What planet is known as the Earth's Twin?",
                    A_Answer = "Mars",
                    B_Answer = "Venus",
                    C_Answer = "Jupiter",
                    D_Answer = "Saturn",
                    A_Correct = false,
                    B_Correct = true,
                    C_Correct = false,
                    D_Correct = false
                }
            );
        }
    }
}
