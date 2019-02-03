using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTracker.Mobile.ViewModel
{
    public class ExpenseViewModel
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

    public class MainPageViewModel
    {
        public ObservableCollection<ExpenseViewModel> ExpensesList { get; private set; }

        public ICommand AddButtonCommand { get; private set; }

        public MainPageViewModel()
        {
            this.ExpensesList = new ObservableCollection<ExpenseViewModel>
            {
                new ExpenseViewModel{Description = "test1", Amount = 100},
                new ExpenseViewModel{Description = "test2", Amount = 220},
            };

            this.AddButtonCommand = new Command(this.OnButtonAdd);
        }

        private void OnButtonAdd()
        {
            this.ExpensesList.Add(new ExpenseViewModel { Description = "from button", Amount = 125 });
        }
    }
}
