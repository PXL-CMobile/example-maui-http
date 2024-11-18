namespace MauiHttp.Pages;

public partial class ActorListPage : ContentPage
{
	public ActorListPage(ActorListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm; 
	}
}