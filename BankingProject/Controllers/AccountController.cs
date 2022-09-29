using BankingProject.DataAccess.Repository.IRepository;
using BankingProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Account> ObjAccountList = _unitOfWork.Account.GetAll();
            return View(ObjAccountList);
        }

        //GET
        public IActionResult Create()
        {
            Account account = new();
            IEnumerable<SelectListItem> objCustomerList = _unitOfWork.Customer.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.CustomerName,
                    Value = u.CustomerId.ToString(),
                });
            var enumData = from AccountType e in Enum.GetValues(typeof(AccountType))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString(),
                           };
            ViewBag.CustomerList = objCustomerList;
            ViewBag.TypeList = new SelectList(enumData, "ID", "Name");

            return View(account);
        }


        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Account.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Account created succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? accountId)
        {
            Account Data = new Account();
            IEnumerable<SelectListItem> objCustomerList = _unitOfWork.Customer.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.CustomerName,
                    Value = u.CustomerId.ToString(),
                });
            if (accountId == null || accountId == 0)
            {
                return NotFound();
            }
            var accountFromDb = _unitOfWork.Account.GetFirstOrDefault(x => x.Id == accountId);

            if (accountFromDb == null)
            {
                return NotFound();
            }

            Data.AccountName = accountFromDb.AccountName;
            Data.AccountNumber = accountFromDb.AccountNumber;
            Data.accountType = accountFromDb.accountType;
            Data.CustomerId = accountFromDb.CustomerId;


            ViewBag.CustomerList = objCustomerList;

            return View(Data);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Account obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Account.Update(obj);    
                _unitOfWork.Save();
                TempData["success"] = "Account updated succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? accountId)
        {

            Account Data = new Account();

            if (accountId == null || accountId == 0)
            {
                return NotFound();
            }
            var accountFromDb = _unitOfWork.Account.GetFirstOrDefault(x => x.Id == accountId);

            if (accountFromDb == null)
            {
                return NotFound();
            }
          
            Data.AccountNumber = accountFromDb.AccountNumber;
            Data.AccountName = accountFromDb.AccountName;
            Data.accountType = accountFromDb.accountType;
            

            return View(Data);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Account Data)
        {
            var obj = _unitOfWork.Account.GetFirstOrDefault(x => x.AccountNumber == Data.AccountNumber);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Account.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Account deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
