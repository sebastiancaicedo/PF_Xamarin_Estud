using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF_Xamarin_Estud
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private IList<Subject> Subjects { get; set; }
        private bool appeared = false;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        LoginPage.LoggedUser = await FirebaseHelper.GetStudentById(LoginPage.Auth.User.LocalId);
                        listviewSubjects.ItemsSource = null;
                        Subjects = await FirebaseHelper.GetSubjectsById(LoginPage.LoggedUser.SubjectsKeys);
                        listviewSubjects.ItemsSource = Subjects;
                        IsRefreshing = false;
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", "Problema al traer las asignaturas, : " + ex, "OK");
                        //throw;
                    }
                });
            }
        }

        public HomePage ()
		{
            Title = "Mis Clases";
			InitializeComponent ();
            BindingContext = this;
            IsRefreshing = LoginPage.LoggedUser.SubjectsKeys.Count > 0 ? true : false;
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
                IsRefreshing = false;
            }
        }

        public void ShowSubjectInfo(object sender, ItemTappedEventArgs e)
        {
            Subject subject = e.Item as Subject;
            SubjectPage page = new SubjectPage(subject);
            Navigation.PushAsync(page);
            (sender as ListView).SelectedItem = null;
        }
    }
}