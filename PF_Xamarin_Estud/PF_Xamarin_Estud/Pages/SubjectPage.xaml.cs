using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF_Xamarin_Estud
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubjectPage : TabbedPage
	{
        private Subject Subject { get; set; }
        private IList<Student> Partners { get; set; }
        private IList<Evaluation> Evaluations { get; set; }
        private bool appeared = false;

		public SubjectPage (Subject subject)
		{
            this.Subject = subject;
            Title = "Asignatura: " + Subject.Name;
			InitializeComponent ();
            labelSubjectInfoTitle.Text = "Profesor: " + Subject.ProfessorFullName;
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!appeared)
            {
                try
                {
                    Partners = await FirebaseHelper.GetStudentsByIds(Subject.StudentsKeys);
                    listviewPartners.ItemsSource = Partners;
                    Evaluations = await FirebaseHelper.GetEvaluations(Subject.EvaluationsKeys);
                    SetUpMyCalifications();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Problema al traer a los estudiantes de firebase, : " + ex, "OK");
                    //throw;
                }

                appeared = true;
            }
        }

        private void SetUpMyCalifications()
        {
            StackLayout layoutEvaluations = new StackLayout { Orientation = StackOrientation.Vertical, Margin = new Thickness(15) };
            StackLayout layoutEvaluationHeader = new StackLayout { Orientation = StackOrientation.Horizontal };
            Label labelEvaluationTitle = new Label { Text = "Evaluación", TextColor = Color.Red, FontSize = (double)new FontSizeConverter().ConvertFromInvariantString("Large"), HorizontalOptions = LayoutOptions.StartAndExpand };
            Label labelScoreTitle = new Label { Text = "Nota", TextColor = Color.Red, FontSize = (double)new FontSizeConverter().ConvertFromInvariantString("Large") };
            layoutEvaluationHeader.Children.Add(labelEvaluationTitle);
            layoutEvaluationHeader.Children.Add(labelScoreTitle);

            layoutEvaluations.Children.Add(layoutEvaluationHeader);
            foreach (var evaluation in Evaluations)
            {
                int evaluationIndex = Evaluations.IndexOf(evaluation);

                for (int index = 0; index < evaluation.Califications.Count; index++)
                {
                    Evaluation.Calification calification = evaluation.Califications[index];
                    if(calification.StudentKey == LoginPage.Auth.User.LocalId)
                    {
                        StackLayout layoutCalification = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(20,10,0,0) };
                        Label labelEvaluationName = new Label { Text = evaluation.Name, FontSize = (double)new FontSizeConverter().ConvertFromInvariantString("Medium"), HorizontalOptions = LayoutOptions.StartAndExpand };
                        Label labelScore = new Label { Text = calification.FinalScore.ToString(), FontSize = (double)new FontSizeConverter().ConvertFromInvariantString("Medium"), HorizontalTextAlignment = TextAlignment.Center };

                        layoutCalification.Children.Add(labelEvaluationName);
                        layoutCalification.Children.Add(labelScore);

                        layoutEvaluations.Children.Add(layoutCalification);
                        break;
                    }
                }
            }
            layoutMain.Children.Add(layoutEvaluations);
        }
    }
}