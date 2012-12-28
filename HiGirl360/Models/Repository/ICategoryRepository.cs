using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HiGirl360.Models.ViewModel;

namespace HiGirl360.Models.Repository
{
    public interface ICategoryRepository
    {
        IList<Category> GetTopCategory();
    }
}
