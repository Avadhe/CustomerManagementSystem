using CMV.Data;
using CMV.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CMV.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Customer/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Customer/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var validUsername = "avadheshd@unifycloud.com";
            var validPassword = "avadheshD43@";

            if (username == validUsername && password == validPassword)
            {
                // Set session value and timestamp
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("SessionStart", DateTime.UtcNow.ToString("o"));

                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid login attempt.";
            return View();
        }

        // GET: /Customer/Index
        public async Task<IActionResult> Index()
        {
            // Check session validity
            if (IsSessionExpired())
            {
                // If session expired, clear session and redirect to login
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            // Retrieve customer list
            var customers = await _context.Customers.ToListAsync();

            // Retrieve username from session for display
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;

            // Pass session customer name to the view
            ViewBag.SessionCustomerName = username;

            return View(customers);
        }

        private bool IsSessionExpired()
        {
            var sessionStart = HttpContext.Session.GetString("SessionStart");
            if (sessionStart == null)
                return true;

            var sessionStartDateTime = DateTime.Parse(sessionStart, null, System.Globalization.DateTimeStyles.RoundtripKind);

            return (DateTime.UtcNow - sessionStartDateTime).TotalMinutes > 5;
        }

        // GET: /Customer/Details/16
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: /Customer/Edit/16
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: /Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,FirstName,LastName,Email,PhoneNumber,Address")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
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
            return View(customer);
        }

        // GET: /Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PhoneNumber,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: /Customer/Delete/16
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: /Customer/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        // POST: /Customer/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
