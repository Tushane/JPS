using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPS.Data;
using Microsoft.AspNetCore.Authorization;

namespace JPS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PREMISE_DETAILSController : Controller
    {
        private readonly BillContext _context;

        public PREMISE_DETAILSController(BillContext context)
        {
            _context = context;
        }

        // GET: PREMISE_DETAILS
        public async Task<IActionResult> Index()
        {
            return View(await _context.PREMISE_DETAILS.ToListAsync());
        }

        // GET: PREMISE_DETAILS/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pREMISE_DETAILS = await _context.PREMISE_DETAILS
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pREMISE_DETAILS == null)
            {
                return NotFound();
            }

            return View(pREMISE_DETAILS);
        }

        // GET: PREMISE_DETAILS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PREMISE_DETAILS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ROW_ID,SUB_ID,LOCATION_ADDRESS")] PREMISE_DETAILS pREMISE_DETAILS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pREMISE_DETAILS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pREMISE_DETAILS);
        }

        // GET: PREMISE_DETAILS/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pREMISE_DETAILS = await _context.PREMISE_DETAILS.FindAsync(id);
            if (pREMISE_DETAILS == null)
            {
                return NotFound();
            }
            return View(pREMISE_DETAILS);
        }

        // POST: PREMISE_DETAILS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ROW_ID,SUB_ID,LOCATION_ADDRESS")] PREMISE_DETAILS pREMISE_DETAILS)
        {
            if (id != pREMISE_DETAILS.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pREMISE_DETAILS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PREMISE_DETAILSExists(pREMISE_DETAILS.ID))
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
            return View(pREMISE_DETAILS);
        }

        // GET: PREMISE_DETAILS/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pREMISE_DETAILS = await _context.PREMISE_DETAILS
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pREMISE_DETAILS == null)
            {
                return NotFound();
            }

            return View(pREMISE_DETAILS);
        }

        // POST: PREMISE_DETAILS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pREMISE_DETAILS = await _context.PREMISE_DETAILS.FindAsync(id);
            _context.PREMISE_DETAILS.Remove(pREMISE_DETAILS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PREMISE_DETAILSExists(string id)
        {
            return _context.PREMISE_DETAILS.Any(e => e.ID == id);
        }
    }
}
