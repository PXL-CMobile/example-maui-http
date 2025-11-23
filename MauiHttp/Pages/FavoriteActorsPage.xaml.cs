namespace MauiHttp.Pages;

public partial class FavoriteActorsPage : ContentPage
{
	public FavoriteActorsPage(FavoriteActorsListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm; 
	}
}