//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using RatesParsingWeb.Domain;
//using RatesParsingWeb.Storage;

//namespace RatesParsingWeb
//{
//    public class DeleteModel : PageModel
//    {
//        private readonly RatesParsingWeb.Storage.BankRatesContext _context;

//        public DeleteModel(RatesParsingWeb.Storage.BankRatesContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Bank Bank { get; set; }

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            Bank = await _context.Banks
//                .Include(b => b.Currency).FirstOrDefaultAsync(m => m.Id == id);

//            if (Bank == null)
//            {
//                return NotFound();
//            }
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            Bank = await _context.Banks.FindAsync(id);

//            if (Bank != null)
//            {
//                _context.Banks.Remove(Bank);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToPage("./Index");
//        }
//    }
//}
