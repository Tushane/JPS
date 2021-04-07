using JPS.Areas.Identity.Data;
using JPS.Data;
using JPS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JPS.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly BillContext _context;
        private readonly UserManager<JPSUser> userManager;
        public CustomerController(UserManager<JPSUser> _userManager, BillContext context)
        {
            this.userManager = _userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Bill_Information.ToListAsync();
            List<Bill_Information> model = new List<Bill_Information>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                decimal total = 0;
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    foreach (var item in data)
                    {
                        if (item.cust_id == userIdValue)
                        {
                            model.Add
                                (
                                    new Bill_Information
                                    {
                                        address = item.address,
                                        amount = item.amount,
                                        bill_id = item.bill_id,
                                        cust_id = item.cust_id,
                                        date_generated = item.date_generated,
                                        due_date = item.due_date,
                                        premise_id = item.premise_id,
                                        status = item.status
                                    }
                                );
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Pending()
        {
            var data = await _context.Bill_Information.ToListAsync();
            List<Bill_Information> model = new List<Bill_Information>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                decimal total = 0;
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    foreach (var item in data)
                    {
                        if (item.status == "PENDING" && item.cust_id == userIdValue)
                        {
                            model.Add
                                (
                                    new Bill_Information
                                    {
                                        address = item.address,
                                        amount = item.amount,
                                        bill_id = item.bill_id,
                                        cust_id = item.cust_id,
                                        date_generated = item.date_generated,
                                        due_date = item.due_date,
                                        premise_id = item.premise_id,
                                        status = item.status
                                    }
                                );
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Paid()
        {
            var data = await _context.Bill_Information.ToListAsync();
            List<Bill_Information> model = new List<Bill_Information>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                decimal total = 0;
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    foreach (var item in data)
                    {
                        if (item.status == "PAID" && item.cust_id == userIdValue)
                        {
                            model.Add
                                (
                                    new Bill_Information
                                    {
                                        address = item.address,
                                        amount = item.amount,
                                        bill_id = item.bill_id,
                                        cust_id = item.cust_id,
                                        date_generated = item.date_generated,
                                        due_date = item.due_date,
                                        premise_id = item.premise_id,
                                        status = item.status
                                    }
                                );
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult ToPayment(string prod_id)
        {
           if(prod_id != null)
            {
                return RedirectToAction("Payment_Details", new { Id = prod_id });
            }
            return View();
        }

        public async Task<IActionResult> Payment_Details(string prod_id)
        {
            var prod_info = await _context.Bill_Information.FindAsync(prod_id);
            Payment_Details pd = new Payment_Details{prod_id = prod_id, desc = "JPS BILL", prod_amount = prod_info.amount};
            return View(pd);
        }
    }
}
