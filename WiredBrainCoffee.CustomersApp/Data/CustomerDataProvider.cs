using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Data
{
    public interface ICustomerDataProvider
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
    }
    public class CustomerDataProvider : ICustomerDataProvider
    {
        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            await Task.Delay(100); //Simulate a bit od server work
            return new List<Customer>
            {
                new Customer { Id = 1,FirstName="Julia",LastName="Developer",IsDeveloper=true} ,
                new Customer { Id = 1,FirstName="Alex",LastName="Rider"} ,
                new Customer { Id = 1,FirstName="Phone",LastName="Thit",IsDeveloper=true} ,
                new Customer { Id = 1,FirstName="Anna",LastName="Rockstar"} ,
                new Customer { Id = 1,FirstName="Sara",LastName="Corna"} ,
                new Customer { Id = 1,FirstName="Thomas",LastName="Ronaldo"}
            };
        }
    }
}
