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
            return GetMaxLeadDate(productIds, orderDate);
        }

        private static DispatchDate GetMaxLeadDate(List<int> productIds, DateTime orderDate)
        {
            DateTime maxLeadDate = orderDate;

            foreach (var productId in productIds)
            {
                DbContext dbContext = new DbContext();
                int supplierId = dbContext.Products.Single(x => x.ProductId == productId).SupplierId;
                int leadTime = dbContext.Suppliers.Single(x => x.SupplierId == supplierId).LeadTime;
                if (orderDate.AddDays(leadTime) > maxLeadDate)
                    maxLeadDate = orderDate.AddDays(leadTime);
            }
            
            if (orderDate.DayOfWeek == DayOfWeek.Saturday || orderDate.DayOfWeek == DayOfWeek.Friday) 
                return new DispatchDate { Date = maxLeadDate.AddDays(2) };

            if (orderDate.DayOfWeek == DayOfWeek.Sunday)
                return new DispatchDate { Date = maxLeadDate.AddDays(1) };

            return new DispatchDate { Date = maxLeadDate };
        }
    }
}
