using System;
using System.Collections.Generic;
using System.Text;

namespace PF_Xamarin_Estud
{
    public class Evaluation
    {
        public string Name { get; private set; }
        public string SubjectKey { get; private set; }
        public string RubricKey { get; private set; }
        public bool IsCompleted { get; private set; }
        public string Status { get; private set; }
        public IList<Calification> Califications { get; private set; }

        public Evaluation(string name, string subjectKey, string rubricKey, bool isCompleted, string status, IList<Calification> califications)
        {
            Name = name;
            SubjectKey = subjectKey;
            RubricKey = rubricKey;
            IsCompleted = isCompleted;
            Status = status;
            Califications = califications;
        }

        public class Calification
        {
            public string StudentKey { get; private set; }
            public string StudentFullName { get; private set; }
            public ScoreCategory[] CategoriesScores { get; private set; }
            public float FinalScore { get; set; }

            public Calification(string studentKey, string studentFullName, ScoreCategory[] categoriesScores, float finalScore)
            {
                StudentKey = studentKey;
                StudentFullName = studentFullName;
                CategoriesScores = categoriesScores;
                FinalScore = finalScore;
            }
        }

        public class ScoreCategory
        {
            public float CategoryScore { get; set; }
            public float[] ElementScores { get; private set; }
            public int[] ElementsSelectedLevelsIndex { get; private set; }

            public ScoreCategory(float categoryScore, float[] elementScores, int[] elementsSelectedLevelsIndex)
            {
                CategoryScore = categoryScore;
                ElementScores = elementScores;
                ElementsSelectedLevelsIndex = elementsSelectedLevelsIndex;
            }

        }
    }
}
