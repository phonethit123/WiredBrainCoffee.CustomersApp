using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        private CustomerViewModel _viewModel;
        public CustomerView()
        {
            InitializeComponent();
            _viewModel = new CustomerViewModel(new CustomerDataProvider());
            DataContext = _viewModel;
            Loaded += CustomerViewLoaded;
        }

        private  async void CustomerViewLoaded(object sender, RoutedEventArgs e)
        {
           await _viewModel.LoadAsync();
        }

        private void ButtonMoveNavigation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveNavigation();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Add();
        }
    }
   
}
