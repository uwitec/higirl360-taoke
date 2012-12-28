using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Models.Repository
{
    public interface IAdRepository
    {
        IQueryable<TAOKEAD> GetHomeAdList();
    }
}
