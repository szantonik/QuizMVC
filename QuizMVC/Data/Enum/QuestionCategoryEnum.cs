using System.ComponentModel;

namespace QuizMVC.Data.Enum
{
    public enum QuestionCategoryEnum
    {
        [Description("General Knowledge")]
        GeneralKnowledge,
        [Description("Cars")]
        Cars,
        [Description("Music")]
        Music,
        [Description("History")]
        History,
        [Description("Geography")]
        Geography,
        [Description("Sports")]
        Sports,
        [Description("Animals")]
        Animals,
        [Description("Science")]
        Science,
        [Description("Books")]
        Books,
        [Description("News")]
        News,
        [Description("Human Body")]
        HumanBody,
        [Description("Movies")]
        Movies,
        [Description("Famous People")]
        FamousPeople
    }
}
