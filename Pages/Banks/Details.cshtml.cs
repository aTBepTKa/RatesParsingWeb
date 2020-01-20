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
//    public class DetailsModel : PageModel
//    {
//        private readonly RatesParsingWeb.Storage.BankRatesContext _context;

//        public DetailsModel(RatesParsingWeb.Storage.BankRatesContext context)
//        {
//            _context = context;
//        }

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
//    }
//}
