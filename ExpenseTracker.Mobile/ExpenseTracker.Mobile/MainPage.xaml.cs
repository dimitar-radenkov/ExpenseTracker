using ExpenseTracker.Mobile.ViewModel;
using Xamarin.Forms;

namespace ExpenseTracker.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }
    }
}
