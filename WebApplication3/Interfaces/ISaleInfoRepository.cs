using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Interfaces
{
    public interface ISaleInfoRepository
    {
        SalesInfo Add(SalesInfo salesInfo);
    }
}
