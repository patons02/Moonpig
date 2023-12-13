namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    [Route("api/[controller]")]
    public class DispatchDateController : Controller
    {
        [HttpGet]
        public DispatchDate GetDispatchDate(List<int> productIds, DateTime orderDate)
        {
            DateTime maxLeadDate = orderDate; // max lead time
            foreach (var productId in productIds)
            {
                DbContext dbContext = new DbContext();
                int supplierId = dbContext.Products.Single(x => x.ProductId == productId).SupplierId;
                var leadTime = dbContext.Suppliers.Single(x => x.SupplierId == supplierId).LeadTime;
                if (orderDate.AddDays(leadTime) > maxLeadDate)
                    maxLeadDate = orderDate.AddDays(leadTime);
            }
            if (maxLeadDate.DayOfWeek == DayOfWeek.Saturday)
            {
                return new DispatchDate { Date = maxLeadDate.AddDays(2) };
            }
            
            if (maxLeadDate.DayOfWeek == DayOfWeek.Sunday) return new DispatchDate { Date = maxLeadDate.AddDays(1) };
            
            return new DispatchDate { Date = maxLeadDate };
        }
    }
}
