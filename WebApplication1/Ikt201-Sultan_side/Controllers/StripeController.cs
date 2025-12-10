using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;

namespace Ikt201_Sultan_side.Controllers
{
    [Route("stripe")]
    public class StripeController : Controller
    {
        public class CheckoutItem
        {
            public string Name { get; set; } = "";
            public long Price { get; set; }  // i NOK
            public int Quantity { get; set; }
        }

        public class CreateCheckoutRequest
        {
            public List<CheckoutItem> Items { get; set; } = new();
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] CreateCheckoutRequest request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { error = "Handlekurven er tom." });
            }

            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = $"{domain}/Home/BestillingFullfort?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/Home/BestillingAvbrutt",
                LineItems = request.Items.Select(i => new SessionLineItemOptions
                {
                    Quantity = i.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "nok",
                        UnitAmount = i.Price * 100, // NOK → øre
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = i.Name
                        }
                    }
                }).ToList()
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Json(new { url = session.Url });
        }
    }
}