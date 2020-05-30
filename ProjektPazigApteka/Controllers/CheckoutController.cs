using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektPazigApteka.Models;

namespace ProjektPazigApteka.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ProjektPazigAptekaEntities storeDB = new ProjektPazigAptekaEntities();
        const string PromoCode = "50";

        public ActionResult AddressAndPayment()
        {
            return View();
        }

       [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    //  return View(order);
                    // return RedirectToAction("Complete",
                    //   new { id = order.OrderId });
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    order.Email = order.Username;
                    //  return RedirectToPage("./LoginWith2fa", new
                    //  {
                    //      ReturnUrl = returnUrl,
                    //      RememberMe = Input.Username
                    //  });

                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();


                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    storeDB.SaveChanges();

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    order.Email = order.Username;
                    //  return RedirectToPage("./LoginWith2fa", new
                    //  {
                    //      ReturnUrl = returnUrl,
                    //      RememberMe = Input.Username
                    //  });

                    
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
    

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    order.Total = order.Total / 2;

                    storeDB.SaveChanges();


                    return RedirectToAction("Complete",
                       new { id = order.OrderId });
                    
                }
                
            }
            catch(Exception ex)
            {
        

                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
                
            }
        }
    }
}