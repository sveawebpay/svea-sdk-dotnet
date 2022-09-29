﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.AspNetCore.Data;
using Sample.AspNetCore.Models.ViewModels;
using Svea.WebPay.SDK;
using Order = Sample.AspNetCore.Models.Order;


namespace Sample.AspNetCore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly StoreDbContext context;
        private readonly SveaWebPayClient _sveaClient;
       
        public OrdersController(StoreDbContext context,
            SveaWebPayClient sveaClient)
        {
            this.context = context;
            this._sveaClient = sveaClient;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var orders = await this.context.Orders.ToListAsync();
            if (orders != null)
            {
                this.context.Orders.RemoveRange(orders);
                await this.context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentOrderId")] Order order)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(order);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }


        //GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));

            var orders = new List<Order>();
            if (id.HasValue)
            {
                var order = await this.context.Orders.FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound();
                }

                orders.Add(order);
            }
            else
            {
                orders = await this.context.Orders.ToListAsync();
            }
      
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var orderId = int.Parse(order.SveaOrderId);
                var orderViewModel = new OrderViewModel(orderId);
                if (!string.IsNullOrWhiteSpace(order.SveaOrderId))
                {
                    try
                    {
                        orderViewModel.Order = await this._sveaClient.PaymentAdmin.GetOrder(long.Parse(order.SveaOrderId)).ConfigureAwait(false);
                        orderViewModel.IsLoaded = true;
                    }
                    catch {}

                    orderViewModels.Add(orderViewModel);
                }
            }

            return View(new OrderListViewModel
            {
                PaymentOrders = orderViewModels
            });
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var order = await this.context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();
            return View(order);
        }


        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentOrderId")] Order order)
        {
            if (id != order.OrderId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(order);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }


        // GET: Orders
        public IActionResult Index()
        {
            return View();
        }


        private bool OrderExists(int id)
        {
            return this.context.Orders.Any(e => e.OrderId == id);
        }
    }
}