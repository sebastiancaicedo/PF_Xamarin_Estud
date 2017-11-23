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
	public partial class HomePage : ContentPage
	{
        private IList<Subject> Subjects { get; set; }
        private bool appeared = false;

		public HomePage ()
		{
            Title = "Mis Clases";
			InitializeComponent ();
            BindingContext = this;
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!appeared)
            {
                try
                {
                    Subjects = await FirebaseHelper.GetSubjectsById(LoginPage.LoggedUser.SubjectsKeys);
                    listviewSubjects.ItemsSource = Subjects;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Problema al traer las asignaturas de firebase: " + ex, "OK");
                    //throw;
                }
                appeared = true;
            }
        }

        public void ShowSubjectInfo(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new SubjectPage((e.Item as Subject)));
            (sender as ListView).SelectedItem = null;
        }
    }
}