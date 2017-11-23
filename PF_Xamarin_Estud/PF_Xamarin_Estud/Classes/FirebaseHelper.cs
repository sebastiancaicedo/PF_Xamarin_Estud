using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PF_Xamarin_Estud
{
    class FirebaseHelper
    {

        //Auth Config Parameters
        public const string APP_API_KEY = "AIzaSyCV8rNgC4yWFirF911_lOgE9vUjlzmLlC0";
        public static FirebaseAuthProvider AuthProvider { get; } = new FirebaseAuthProvider(new FirebaseConfig(APP_API_KEY));

        //DB Parameters
        public static FirebaseClient FirebaseDBClient { get; } = new FirebaseClient("https://proyectofinal-xamarin.firebaseio.com/");


        /// <summary>
        /// Trae al estudiante con el id, desde firebase
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public static async Task<Student> GetStudentById(string studentId)
        {
            var student = await FirebaseDBClient
                    .Child("students")
                    .Child(studentId)
                    .OnceSingleAsync<Student>();

            return student;
        }

        public static async Task<IList<Subject>> GetSubjectsById(List<string> subjectsKeys)
        {
            IList<Subject> list = new ObservableCollection<Subject>();
            if (subjectsKeys.Count > 0)
            {
                foreach (var subjectKey in subjectsKeys)
                {
                    Subject subject = await FirebaseDBClient
                        .Child("subjects")
                        .Child(subjectKey)
                        .OnceSingleAsync<Subject>();

                    list.Add(subject);
                }
            }

            return list;
        }

        public static async Task<IList<Student>> GetStudentsByIds(List<string> studentsKeys)
        {
            IList<Student> list = new ObservableCollection<Student>();
            if (studentsKeys.Count > 0)
            {
                foreach (var studentKey in studentsKeys)
                {
                    var student = await FirebaseDBClient
                        .Child("students")
                        .Child(studentKey)
                        .OnceSingleAsync<Student>();

                    list.Add(student);
                }
            }
            return list;
        }

        public static async Task<IList<Evaluation>> GetEvaluations(List<string> evaluationsKeys)
        {
            IList<Evaluation> list = new ObservableCollection<Evaluation>();
            if (evaluationsKeys.Count > 0)
            {
                foreach (var studentKey in evaluationsKeys)
                {
                    var evaluation = await FirebaseDBClient
                        .Child("evaluations")
                        .Child(studentKey)
                        .OnceSingleAsync<Evaluation>();


                    list.Add(evaluation);
                }
            }
            return list;
        }
    }
}
