using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPS.Data;
using JPS.Models;
using Microsoft.AspNetCore.Identity;
using JPS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace JPS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Bill_InformationController : Controller
    {
        private readonly BillContext _context;
        private readonly UserManager<JPSUser> userManager;
        public Bill_InformationController(UserManager<JPSUser> _userManager, BillContext context)
        {
            this.userManager = _userManager;
            _context = context;
        }

  
        // GET: Bill_Information
        public async Task<IActionResult> Index()
        {
            var data = await _context.Bill_Information.ToListAsync();
            List<Bill_Information> model = new List<Bill_Information>();
            foreach(var item in data)
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
            return View(model);
        }
       
        // GET: Bill_Information/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bill_Information
             .FirstOrDefaultAsync(m => m.bill_id == id);

            Bill_Information bill_Information = 
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
                        };          

           
            if (bill_Information == null)
            {
                return NotFound();
            }

            return View(bill_Information);
        }

        // GET: Bill_Information/Create
        public async Task<IActionResult> Create()
        {

           var model = await _context.Bill_Information.ToListAsync();

          string id = "BIJPS00" + (model.Count() + 1).ToString();

            List<SeletedCustomer> temp_cust_list = new List<SeletedCustomer>();

            var users = userManager.Users;

            foreach(var user in users)
            {
                SeletedCustomer _user = new SeletedCustomer { cust_id = user.Id };
                temp_cust_list.Add(_user);
            }

            Bill_Information data = new Bill_Information
            {
                bill_id = id,

                date_generated = DateTime.Now,
                due_date = DateTime.Now.AddDays(10),
                cust_ids = temp_cust_list,
                cust_id = null,
                address = null,
                amount = null,
                premise_id = null,
                status = "PENDING"
            };

            return View(data);
        }

        // POST: Bill_Information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bill_id,date_generated,due_date,cust_ids,cust_id,premise_id,address,amount,status")] Bill_Information bill_Information)
        {
            if (ModelState.IsValid)
            {
                JPSUser data = await userManager.FindByIdAsync(bill_Information.cust_id);
                bill_Information.premise_id = data.premise_number;

                var address =  await _context.PREMISE_DETAILS.FirstOrDefaultAsync(m => m.ID == "PREPJPS200");
                bill_Information.address = address.LOCATION_ADDRESS;

                BillDatabaseModel model = new BillDatabaseModel
                {
                    address = bill_Information.address,
                    amount = bill_Information.amount,
                    bill_id = bill_Information.bill_id,
                    cust_id = bill_Information.cust_id,
                    date_generated = bill_Information.date_generated,
                    due_date = bill_Information.due_date,
                    premise_id = bill_Information.premise_id,
                    status = bill_Information.status
                };

                _context.Bill_Information.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bill_Information);
        }

        // GET: Bill_Information/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Bill_Information.FindAsync(id);
            Bill_Information bill_Information =
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
                      };

            if (bill_Information == null)
            {
                return NotFound();
            }
            return View(bill_Information);
        }

        // POST: Bill_Information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("bill_id,date_generated,due_date,cust_id,premise_id,address,amount,status")] Bill_Information bill_Information)
        {
            if (id != bill_Information.bill_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    BillDatabaseModel model = new BillDatabaseModel
                    {
                        address = bill_Information.address,
                        amount = bill_Information.amount,
                        bill_id = bill_Information.bill_id,
                        cust_id = bill_Information.cust_id,
                        date_generated = bill_Information.date_generated,
                        due_date = bill_Information.due_date,
                        premise_id = bill_Information.premise_id,
                        status = bill_Information.status
                    };

                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Bill_InformationExists(bill_Information.bill_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bill_Information);
        }

        // GET: Bill_Information/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Bill_Information
                .FirstOrDefaultAsync(m => m.bill_id == id);

            Bill_Information bill_Information =
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
                     };
            if (bill_Information == null)
            {
                return NotFound();
            }

            return View(bill_Information);
        }

        // POST: Bill_Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bill_Information = await _context.Bill_Information.FindAsync(id);
            _context.Bill_Information.Remove(bill_Information);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Bill_InformationExists(string id)
        {
            return _context.Bill_Information.Any(e => e.bill_id == id);
        }
    }
}
