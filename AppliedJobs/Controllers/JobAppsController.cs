using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppliedJobs.Data;
using AppliedJobs.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppliedJobs.Controllers
{
    public class JobAppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobAppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobApps
        public async Task<IActionResult> Index()
        {
            return _context.JobApp != null ?
            View("Index", await _context.JobApp.Where(j => j.Rejected == false).ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.JobApp'  is null.");
        }

        // GET: JobApps/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: JobApps/ShowSearchForm
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return _context.JobApp != null ?
                          View("Index", await _context.JobApp.Where(j => j.JobTitle.Contains(SearchPhrase) || j.CompanyName.Contains(SearchPhrase) || j.JobLink.Contains(SearchPhrase)).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.JobApp'  is null.");
        }

        // GET: JobApps/ShowInterviewForm
        public async Task<IActionResult> ShowInterviewForm()
        {
            return _context.JobApp != null ?
              View("Index", await _context.JobApp.Where(j => j.Interview == true).ToListAsync()) :
              Problem("Entity set 'ApplicationDbContext.JobApp'  is null.");
        }

        // GET: JobApps/ShowInterviewForm
        public async Task<IActionResult> ShowRejectedForm()
        {
            return _context.JobApp != null ?
              View("Index", await _context.JobApp.Where(j => j.Rejected == true).ToListAsync()) :
              Problem("Entity set 'ApplicationDbContext.JobApp'  is null.");
        }

        // GET: JobApps/ShowInterviewForm
        public async Task<IActionResult> ShowActiveRecruitersForm()
        {
            return View();
        }
        // GET: JobApps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobApp == null)
            {
                return NotFound();
            }

            var jobApp = await _context.JobApp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApp == null)
            {
                return NotFound();
            }

            return View(jobApp);
        }

        // GET: JobApps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobApps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,CompanyName,isRecruiter,Interview,JobLink,DateApplied,Rejected,Notes")] JobApp jobApp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobApp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobApp);
        }

        // GET: JobApps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobApp == null)
            {
                return NotFound();
            }

            var jobApp = await _context.JobApp.FindAsync(id);
            if (jobApp == null)
            {
                return NotFound();
            }
            return View(jobApp);
        }

        // POST: JobApps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,CompanyName,isRecruiter,Interview,JobLink,DateApplied,Rejected,Notes")] JobApp jobApp)
        {
            if (id != jobApp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobAppExists(jobApp.Id))
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
            return View(jobApp);
        }

        // GET: JobApps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobApp == null)
            {
                return NotFound();
            }

            var jobApp = await _context.JobApp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApp == null)
            {
                return NotFound();
            }

            return View(jobApp);
        }

        // POST: JobApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobApp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JobApp'  is null.");
            }
            var jobApp = await _context.JobApp.FindAsync(id);
            if (jobApp != null)
            {
                _context.JobApp.Remove(jobApp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobAppExists(int id)
        {
          return (_context.JobApp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
