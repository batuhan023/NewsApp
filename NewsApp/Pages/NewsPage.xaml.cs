using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Pages;

public partial class NewsPage : ContentPage
{
    private bool IsNextPage = false;
	
    public List<Article> ArticlesList { get; set; }
    public List<Category> CategoryList = new List<Category>()
    {
        new Category(){Name="General"},
        new Category(){Name="World"},
        new Category(){Name="Nation"},
        new Category(){Name="Business"},
        new Category(){Name="Technology"},
        new Category(){Name="Entertaiment"},
        new Category(){Name="Sports"},
        new Category(){Name="Science"},
        new Category(){Name="Health" }
    };
    public NewsPage()
	{
		InitializeComponent();
        ArticlesList = new List<Article>();
        CvCategories.ItemsSource = CategoryList;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (IsNextPage==false)
        {
            await PassCategory("general");
        }
    }

    public async Task PassCategory(string categoryName)
    {
        CvNews.ItemsSource = null;
        ArticlesList.Clear();
        ApiService apiService = new ApiService();
        var newsResult = await apiService.GetNews(categoryName);
        foreach (var item in newsResult.Articles)
        {
            ArticlesList.Add(item);
        }
        CvNews.ItemsSource = ArticlesList;
    }

    private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       var selectedItem = e.CurrentSelection.FirstOrDefault() as Category;
       await PassCategory(selectedItem.Name);
    }

    private void CvNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Article;
        IsNextPage = true;
        Navigation.PushAsync(new NewDetailPage(selectedItem));
    }
}