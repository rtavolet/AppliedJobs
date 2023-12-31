﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppliedJobs.Data;
using AppliedJobs.Models;

namespace AppliedJobs.Controllers
{
    public class RecruitersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecruitersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recruiters
        public async Task<IActionResult> Index()
        {
              return _context.Recruiter != null ? 
                          View(await _context.Recruiter.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Recruiter'  is null.");
        }

        // GET: Recruiters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recruiter == null)
            {
                return NotFound();
            }

            var recruiter = await _context.Recruiter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruiter == null)
            {
                return NotFound();
            }

            return View(recruiter);
        }

        // GET: Recruiters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recruiters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecruiterName,JobTitle,DateContacted,Status")] Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recruiter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recruiter);
        }

        // GET: Recruiters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recruiter == null)
            {
                return NotFound();
            }

            var recruiter = await _context.Recruiter.FindAsync(id);
            if (recruiter == null)
            {
                return NotFound();
            }
            return View(recruiter);
        }

        // POST: Recruiters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecruiterName,JobTitle,DateContacted,Status")] Recruiter recruiter)
        {
            if (id != recruiter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recruiter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecruiterExists(recruiter.Id))
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
            return View(recruiter);
        }

        // GET: Recruiters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recruiter == null)
            {
                return NotFound();
            }

            var recruiter = await _context.Recruiter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruiter == null)
            {
                return NotFound();
            }

            return View(recruiter);
        }

        // POST: Recruiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recruiter == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recruiter'  is null.");
            }
            var recruiter = await _context.Recruiter.FindAsync(id);
            if (recruiter != null)
            {
                _context.Recruiter.Remove(recruiter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruiterExists(int id)
        {
          return (_context.Recruiter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
