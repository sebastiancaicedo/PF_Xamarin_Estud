using Firebase.Xamarin.Auth;
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
    public partial class LoginPage : ContentPage
    {

        public static FirebaseAuth Auth { get; private set; }
        public static Student LoggedUser { get; set; }

        public LoginPage()
        {
            Title = "Iniciar Sesión";
            InitializeComponent();
        }

        /// <summary>
        /// Evento del click del boton LogIn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void LogIn(object sender, EventArgs e)
        {
            StartLoadingIndicator();
            if (String.IsNullOrEmpty(entryEmail.Text) && String.IsNullOrEmpty(entryPassword.Text))
            {
                entryEmail.Text = "jap@uninorte.edu.co";
                entryPassword.Text = "estudiante";
            }
            if (!String.IsNullOrEmpty(entryEmail.Text) && !String.IsNullOrEmpty(entryPassword.Text))
            {
                try
                {
                    Auth = await FirebaseHelper.AuthProvider.SignInWithEmailAndPasswordAsync(entryEmail.Text, entryPassword.Text);
                    LoggedUser = await FirebaseHelper.GetStudentById(Auth.User.LocalId);
                    if (LoggedUser == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        ShowHomePage();
                    }

                }
                catch (Exception ex)
                {
                    StopLoadingIndicator();
                    await DisplayAlert("Error", "No es posible iniciar sesión :" + ex, "OK");
                    //throw ex;
                }
            }
            else
            {
                StopLoadingIndicator();
                await DisplayAlert("Error", "Uno o mas campos vacios", "OK");
            }
        }


        /// <summary>
        /// Muestra la ventana principal y la coloca como la main page de la aplicacion
        /// </summary>
        private void ShowHomePage()
        {
            StopLoadingIndicator();
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }

        /// <summary>
        /// Muestra el indicador de carga y oculta los demas Views
        /// </summary>
        private void StartLoadingIndicator()
        {
            loadIndicator.IsRunning = true;
            layoutTop.IsVisible = false;
        }

        /// <summary>
        /// Detiene el indicador de carga y muestra los demas Views
        /// </summary>
        private void StopLoadingIndicator()
        {
            if (loadIndicator.IsRunning)
            {
                loadIndicator.IsRunning = false;
                layoutTop.IsVisible = true;
            }
        }
    }
}