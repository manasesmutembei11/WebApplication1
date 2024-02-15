using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using WebApplication1.DTOs.PesapalDTOs;
using WebApplication1.Models;
using WebApplication1.Repository.Repositories;
using WebApplication1.Service.IService;
using WebApplication1.Repository.IRepository;
using MvcPaging;

namespace WebApplication1.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _repository;
        private readonly IPesaPalService _pesaPalService;
        IConfiguration _configuration;
        public PeopleController(IPeopleRepository repository, IPesaPalService pesaPalService)
        {
            _repository = repository;
            _pesaPalService = pesaPalService;
            
        }



        // GET: People
        public async Task<ActionResult> Index(int? page)
        {
            var peopleList = await _repository.GetPeopleAsync();

            int pageSize = 10; 
            int pageNumber = (page ?? 1); 

            
            var pagedPeopleList = peopleList.ToPagedList(pageNumber, pageSize);

            return View(pagedPeopleList);

            /*  page = page ?? 1;
               int pageSize = 5
               int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
               var pagedList = await _repository.GetPagedPeopleAsync(currentPageIndex, page.Value, pageSize);
               return View(pagedList);
            */
        }

        [HttpGet, ActionName("search")]
        public async Task<ActionResult> SearchPeople(string searchString)
        {
            var person = string.IsNullOrEmpty(searchString)
                ? await _repository.GetPeopleAsync()
                : await _repository.SearchPeopleAsync(searchString);

            return View(person);
        }



        // GET: People/Details/5

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return (ActionResult)HttpNotFound();
            }

            var person = await _repository.GetPersonByIdAsync(id.Value);
            if (person == null)
            {
                return (ActionResult)HttpNotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return (ActionResult)View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include ="Id,FirstName,LastName,Email,Phone,Country,DateOfBirth")] People person)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddPersonAsync(person);
                return (ActionResult)RedirectToAction(nameof(Index));
            }
            return (ActionResult)View(person);
        }



        // GET: People/Edit/5

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return (ActionResult)HttpNotFound();
            }

            var person = await _repository.GetPersonByIdAsync(id.Value);
            if (person == null)
            {
                return (ActionResult)HttpNotFound();
            }

            return (ActionResult)View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include = "Id,firstName,lastName,email,phone,country,dateOfBirth")] People person)
        {
            if (id != person.Id)
            {
                return (ActionResult)HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdatePersonAsync(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.PersonExists(person.Id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return (ActionResult)RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return (ActionResult)HttpNotFound();
            }

            var person = await _repository.GetPersonByIdAsync(id.Value);
            if (person == null)
            {
                return (ActionResult)HttpNotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeletePersonAsync(id);
            return (ActionResult)RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _repository.PersonExists(id);
        }


        public async Task<ActionResult> Payment(int? id)
        {
            string consumerKey = _configuration["AuthRequest:consumer_key"];
            string consumerSecret = _configuration["AuthRequest:consumer_secret"];

            if (id == null)
            {
                return (ActionResult)HttpNotFound();
            }

            var person = await _repository.GetPersonByIdAsync(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }
            //Get token
            var authResponse = await _pesaPalService.RequestTokenAsync(consumerKey, consumerSecret);
            var token = authResponse.Token;

            // Prepare request
            var orderRequest = new SubmitOrderRequestDTO
            {
                Id = Guid.NewGuid().ToString(),
                Currency = "KES",
                Amount = 1.00f,
                Description = "Payment for services",
                CallbackUrl = $"https://localhost:7175/People/Success/{id}",
                CancellationUrl = $"https://localhost:7175/People/Failed/{id}",
                NotificationId = new Guid("2d4c9c87-3cec-4807-b31c-dd9f6b10f273"),
                BillingAddress = new BillingAddressDTO { PhoneNumber = "0768802661", EmailAddress = "manasesmutembei11@gmail.com" }


            };
            //Submit request to paypal

            var orderResponse = await _pesaPalService.SubmitOrderRequestAsync(token, orderRequest);

            var paymentUrl = orderResponse.RedirectUrl;
            person.PaymentNumber = orderResponse.OrderTrackingId;
            ViewBag.PaymentUrl = paymentUrl;
            return View();

        }


        public async Task<ActionResult> Success(int id, string orderTrackingId, string orderMerchantReference)
        {
            string consumerKey = _configuration["AuthRequest:consumer_key"];
            string consumerSecret = _configuration["AuthRequest:consumer_secret"];
            // Get the person by id
            var person = await _repository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            // Get the transaction status from PesaPal
            var authResponse = await _pesaPalService.RequestTokenAsync(consumerKey, consumerSecret);
            var token = authResponse.Token;
            var transactionStatus = await _pesaPalService.GetTransactionStatusAsync(token, orderTrackingId);

            // Check if payment status description is "COMPLETED"
            if (transactionStatus.StatusCode == 0)
            {
                person.Status = PersonStatus.Invalid;
            }

            if (transactionStatus.StatusCode == 1)
            {
                person.Status = PersonStatus.Confirmed;
            }

            if (transactionStatus.StatusCode == 2)
            {
                person.Status = PersonStatus.Failed;
            }
            if (transactionStatus.StatusCode == 3)
            {
                person.Status = PersonStatus.Reversed;
            }


            person.PaymentNumber = orderTrackingId;
            await _repository.UpdatePersonAsync(person);

            return RedirectToAction("Index");
        }

        public ActionResult Failed(Guid id)
        {
            // Handle failed transaction
            return RedirectToAction("Index");
        }



    }
}