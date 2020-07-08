using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Implementation
{
    public class SalesInfoRepository : ISaleInfoRepository
    {
        private readonly ApplicationDBContext context;
        public SalesInfoRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public SalesInfo Add(SalesInfo salesInfo)
        {
            context.SalesInfo.Add(salesInfo);
            context.SaveChanges();
            return salesInfo;
        }

    }
}
