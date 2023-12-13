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
                {
                    leadTime = CalculateLeadDate(orderDate, leadTime);
                    maxLeadDate = orderDate.AddDays(leadTime);
                }
            }

            return new DispatchDate { Date = maxLeadDate };
        }

        private static int CalculateLeadDate(DateTime orderDate, int leadTime)
        {
            
            for (int i = 0; i <= leadTime; i++)
            {
                if (orderDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday || orderDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    leadTime ++;
                }
            }

            return leadTime;
        }
    }
}
