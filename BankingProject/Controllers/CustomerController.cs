using BankingProject.DataAccess;
using BankingProject.DataAccess.Repository.IRepository;
using BankingProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> ObjCustomerList = _unitOfWork.Customer.GetAll();
            return View(ObjCustomerList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customer.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Customer created succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? customerId)
        {
            if(customerId == null || customerId == 0)
            {
                return NotFound();
            } 
            var customerFromDb = _unitOfWork.Customer.GetFirstOrDefault(x => x.CustomerId == customerId);

            if(customerFromDb == null)
            {
                return NotFound();
            }
            return View(customerFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customer.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Customer updated succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? customerId)
        {
            if (customerId == null || customerId == 0)
            {
                return NotFound();
            }
            var customerFromDb = _unitOfWork.Customer.GetFirstOrDefault(x => x.CustomerId == customerId);

            if (customerFromDb == null)
            {
                return NotFound();
            }
            return View(customerFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? customerId)
        {
            var obj = _unitOfWork.Customer.GetFirstOrDefault(x => x.CustomerId == customerId);

            if(obj == null)
            {
                return NotFound();
            }
              
            _unitOfWork.Customer.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Customer deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
