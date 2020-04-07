using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly RGProject1Context _context;

        private static Orders ShoppingCart = null;

        public ShoppingController(RGProject1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (ShoppingCart == null)
            {
                ViewData["Customer"] = "No customer selected.";
                ViewData["Location"] = "No location selected.";
                ViewData["CartItems"] = new List<Dictionary<string, string>>();
                ViewData["Total"] = "0.00";
            }
            else
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Id == ShoppingCart.CustomerId);
                var location = _context.Locations.FirstOrDefault(l => l.Id == ShoppingCart.LocationId);
                ViewData["Customer"] =  customer.FullName;
                ViewData["Location"] = location.FullAddress;
                var ProductsInCart = new List<Dictionary<string, string>>();
                foreach (var item in ShoppingCart.OrderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                    var dict = new Dictionary<string, string>();
                    dict.Add("Pid", product.Id.ToString());
                    dict.Add("Name", product.Name);
                    dict.Add("Price", product.Price.ToString("#.##"));
                    dict.Add("Quantity", item.Quantity.ToString());
                    ProductsInCart.Add(dict);
                }
                ViewData["CartItems"] = ProductsInCart;
                ViewData["Total"] = ShoppingCart.Total.ToString("#.##");
            }
            return View();
        }

        // GET: Orders/Create
        public IActionResult CreateOrder()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "FullAddress");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder([Bind("Id,LocationId,CustomerId,OrderDate,Total")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart = orders;
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FirstName", orders.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", orders.LocationId);
            return View(orders);
        }

        // GET: OrderItems/Create
        public IActionResult CreateOrderItem()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrderItem([Bind("Id,OrderId,ProductId,Quantity")] OrderItems orderItems)
        {
            if (ModelState.IsValid)
            {
                if (ShoppingCart != null)
                {
                    orderItems.OrderId = ShoppingCart.Id;
                    var inv = _context.Inventory.FirstOrDefault(i => i.ProductId == orderItems.ProductId && i.LocationId == ShoppingCart.LocationId);
                    var item = ShoppingCart.AddToCart(orderItems);
                    if (item.Quantity > inv.Stock)
                    {
                        item.Quantity = inv.Stock;
                    }
                    ShoppingCart.Total += item.Quantity * _context.Products.FirstOrDefault(p => p.Id == item.ProductId).Price;
                }
                ViewData["Total"] = ShoppingCart.Total.ToString("#.##");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Total"] = ShoppingCart.Total.ToString("#.##");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItems.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderItems.ProductId);
            return View(orderItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            if (ModelState.IsValid)
            {
                if (ShoppingCart == null)
                {
                    ViewData["Message"] = "I'm sorry, but you don't have a shopping cart.";
                }
                else if (ShoppingCart.OrderItems.Count == 0)
                {
                    ViewData["Message"] = "I'm sorry, but you haven't added anything to your cart.";
                }
                else
                {
                    foreach (var item in ShoppingCart.OrderItems)
                    {
                        var inventory = _context.Inventory.First(i => i.LocationId == ShoppingCart.LocationId && i.ProductId == item.ProductId);
                        inventory.Stock -= item.Quantity;
                    }
                    _context.Add(ShoppingCart);
                    await _context.SaveChangesAsync();
                    ViewData["Message"] = "Your order has been placed!";
                    ShoppingCart = null;
                }
            }
            return View();
        }

        public IActionResult CancelOrder()
        {
            ShoppingCart = null;
            return View();
        }
    }
}