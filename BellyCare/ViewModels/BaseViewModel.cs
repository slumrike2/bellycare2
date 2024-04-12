using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;

namespace BellyCare.ViewModels
{
    public class BaseViewModel(FirebaseClient db) : ObservableObject
    {
        protected readonly FirebaseClient db = db;
    }
}