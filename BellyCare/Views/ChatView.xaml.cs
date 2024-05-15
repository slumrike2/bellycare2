using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class ChatView : BaseContentPage
{
	public ChatView(ChatViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();

        viewModel.CollectionView = collectionView;
        viewModel.ChatEntry = chatEntry;
    }
}