using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using System.Collections.ObjectModel;

namespace BellyCare.ViewModels
{
    [QueryProperty(nameof(ChatId), nameof(ChatId))]
    [QueryProperty(nameof(ChatRepository), nameof(ChatRepository))]
    [QueryProperty(nameof(MessageRepository), nameof(MessageRepository))]
    public partial class ChatViewModel : BaseViewModel, IEventfulViewModel
    {
        [ObservableProperty]
        BaseOnlineRepository<Chat> chatRepository;

        [ObservableProperty]
        BaseOnlineRepository<ChatMessage> messageRepository;

        [ObservableProperty]
        string chatId;

        [ObservableProperty]
        string messageText;

        [ObservableProperty]
        ObservableCollection<ChatMessage> messages;

        [ObservableProperty]
        bool isLoading;

        public CollectionView CollectionView { get; set; }
        public Entry ChatEntry { get; set; }

        private readonly FirebaseClient db;

        public ChatViewModel(ISettingsService settings, INavigationService navigationService, FirebaseClient db) : base(settings, navigationService)
        {
            this.db = db;
        }

        [RelayCommand]
        async Task SendMessage()
        {
            if (string.IsNullOrEmpty(MessageText))
            {
                return;
            }

            string messageText = MessageText.Trim();

            try
            {
                await MessageRepository.Add(new()
                {
                    Content = messageText,
                    SenderId = settings.AccessToken,
                    SenderName = LoggedUserType.Patient == settings.UserType ? settings.Patient.Names : settings.Doctor.Names,
                    SentDate = DateTime.Now
                });

                await RefreshMessages();

                MessageText = string.Empty;
                await ChatEntry.HideKeyboardAsync(CancellationToken.None);
            }
            catch (Exception)
            {
                await AppUtils.ShowAlert("Error al enviar mensaje", AlertType.Error);
            }

        }

        async Task RefreshMessages()
        {
            IsLoading = true;
            var messages = (await MessageRepository.GetAll()).Select(m => m.Object).ToList();
            if (messages != null)
            {
                messages.ForEach(m =>
                {
                    m.IsMine = m.SenderId == settings.AccessToken;
                });

                Messages = new ObservableCollection<ChatMessage>(messages);
            }
            IsLoading = false;

            CollectionView.ScrollTo(0);
        }

        public async void OnAppearing()
        {

            Messages = [];

            await RefreshMessages();

            //Listen to new messages
            db.Child($"Chat/{ChatId}/Messages").AsObservable<ChatMessage>().Subscribe(async args =>
            {
                await RefreshMessages();
            });
        }

        public void OnDisappearing()
        {
        }
    }
}