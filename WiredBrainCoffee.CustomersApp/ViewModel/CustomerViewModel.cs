using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly ICustomerDataProvider _customerDataProvider;
        private NavigationSide _navigationSide;

        public CustomerViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();
        public CustomerItemViewModel? _selectedCustomer { get; set; }
        public CustomerItemViewModel? SelectedCustomer 
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
                 
            }
        }

        public NavigationSide NavigationSide 
        { 
            get => _navigationSide;
            private set 
            {
                _navigationSide = value;
                RaisePropertyChanged();
            } 
        }

        public async Task LoadAsync() 
        {
            if(Customers.Any())
            {
                return;
            }
            var customers = await _customerDataProvider.GetAllAsync();

            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }

        }

        internal void Add()
        {
            var customer = new Customer { FirstName = "New" };
            var ViewModel = new CustomerItemViewModel(customer);

            Customers.Add(ViewModel);
            SelectedCustomer = ViewModel;

        }


        internal void MoveNavigation()
        {
            NavigationSide = NavigationSide == NavigationSide.Left 
                ? NavigationSide.Right : NavigationSide.Left;
        }
    }
    public enum NavigationSide
    {
        Left,
        Right
    }
}
