using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeBank.Models;

namespace CodeBank.Controllers
{
    public class CodeBankViewModelsController : Controller
    {
        private readonly CodeBankContext _context;

        public CodeBankViewModelsController(CodeBankContext context)
        {
            _context = context;
        }

        // GET: CodeBankViewModels
        public async Task<IActionResult> Index()
        {
            var model = new CodeBank.ViewModels.CodebankViewModel()
            {
                Code = await _context.Code.ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Search(string value)
        {
            var item = await _context.Code.ToListAsync();
            if (string.IsNullOrEmpty(value))
            {
                var m = new CodeBank.ViewModels.CodebankViewModel()
                {
                    Code = item
                };

                return View("Index", m);
            }

            var result = item.Where(x => x.KeyWords.ToLower().Contains(value.ToLower()) ||
                                         x.Title.ToLower().Contains(value)
            ).ToList();
            var model = new CodeBank.ViewModels.CodebankViewModel()
            {
                Code = result
            };


            return View("Index", model);
        }

        // GET: CodeBankViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeBankViewModel = await _context.Code
                .FirstOrDefaultAsync(m => m.Id == id);
            if (codeBankViewModel == null)
            {
                return NotFound();
            }

            return View(codeBankViewModel);
        }

        // GET: CodeBankViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CodeBankViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Link,Title,Description,KeyWords")] Code codeBankViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(codeBankViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(codeBankViewModel);
        }

        // GET: CodeBankViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeBankViewModel = await _context.Code.FindAsync(id);
            if (codeBankViewModel == null)
            {
                return NotFound();
            }
            return View(codeBankViewModel);
        }

        // POST: CodeBankViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,Title,Description,KeyWords")] Code codeBankViewModel)
        {
            if (id != codeBankViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(codeBankViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeBankViewModelExists(codeBankViewModel.Id))
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
            return View(codeBankViewModel);
        }

        // GET: CodeBankViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeBankViewModel = await _context.Code
                .FirstOrDefaultAsync(m => m.Id == id);
            if (codeBankViewModel == null)
            {
                return NotFound();
            }

            return View(codeBankViewModel);
        }

        // POST: CodeBankViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var codeBankViewModel = await _context.Code.FindAsync(id);
            _context.Code.Remove(codeBankViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeBankViewModelExists(int id)
        {
            return _context.Code.Any(e => e.Id == id);
        }
    }
}
