using Barreto.Exe.Maui.ViewModels;
using Firebase.Database;

namespace BellyCare.ViewModels
{
    public class HomeViewModel(FirebaseClient db) : BaseViewModel(db), IEventfulViewModel
    {
        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}