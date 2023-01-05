using CustomerInformationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerInformationSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerContext customerContext;
        public CustomerController(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }
        [HttpGet]
        public async Task<IActionResult> Read()
        {
           var customers = await customerContext.Customers.ToListAsync();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel createCustomer)
        {
            var customer = new Customer()
            {

                Name = createCustomer.Name,
                Lastname = createCustomer.Lastname,
                Tc = createCustomer.Tc,
                Emaİl = createCustomer.Emaİl,
                Phone = createCustomer.Phone,
                Address = createCustomer.Address

            };
            await customerContext.Customers.AddAsync(customer);
            await customerContext.SaveChangesAsync();
            return RedirectToAction("Read");
        }

        [HttpGet]   
        public async Task<IActionResult> View(int Id)
        {
            var customer = await customerContext.Customers.FirstOrDefaultAsync(item => item.Id == Id);

            if (customer != null)
            {
                var viewModel = new UpdateCustomerViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Lastname = customer.Lastname,
                    Tc = customer.Tc,
                    Emaİl = customer.Emaİl,
                    Phone = customer.Phone,
                    Address = customer.Address
                };
                return await Task.Run(()=>View("View",viewModel)); 
            }
            return RedirectToAction("Read");
        }
        [HttpPost]
        public async Task<IActionResult>  View(UpdateCustomerViewModel model)
        {
            var customer = await customerContext.Customers.FindAsync(model.Id);

            if(customer != null)
            {
                customer.Name= model.Name;
                customer.Lastname = model.Lastname;
                customer.Tc= model.Tc;
                customer.Emaİl = model.Emaİl;
                customer.Phone = model.Phone;
                customer.Address = model.Address;

                await customerContext.SaveChangesAsync();

                return RedirectToAction("Read");
            }
            return RedirectToAction("Read");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCustomerViewModel model)
        {
            var customer = await customerContext.Customers.FindAsync(model.Id);

            if (customer != null)
            {
                customerContext.Customers.Remove(customer);
                await customerContext.SaveChangesAsync();

                return RedirectToAction("Read");
            }
            return RedirectToAction("Read");
        
        }

    }
}

