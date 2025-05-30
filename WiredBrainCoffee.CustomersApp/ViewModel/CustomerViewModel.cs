﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using WiredBrainCoffee.CustomersApp.Command;
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
            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete,CanDelete);
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
                RaisePropertyChanged(nameof(IsCustomerSelected));
                DeleteCommand.RaiseCanExecuteChanged();
                 
            }
        }
        public bool IsCustomerSelected => SelectedCustomer is not null;

        public NavigationSide NavigationSide 
        { 
            get => _navigationSide;
            private set 
            {
                _navigationSide = value;
                RaisePropertyChanged();
            } 
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand MoveNavigationCommand { get; }
        public DelegateCommand DeleteCommand { get; }


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

        private void Add(object? parameter)
        {
            var customer = new Customer { FirstName = "New" };
            var ViewModel = new CustomerItemViewModel(customer);

            Customers.Add(ViewModel);
            SelectedCustomer = ViewModel;

        }


        private void MoveNavigation(object? parameter)
        {
            NavigationSide = NavigationSide == NavigationSide.Left 
                ? NavigationSide.Right : NavigationSide.Left;
        }
        private bool CanDelete(object? parameter) => SelectedCustomer is not null;
        

        private void Delete(object? parameter)
        {
           if(SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }
    }
    public enum NavigationSide
    {
        Left,
        Right
    }
}
