using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.Repository
{
    public class RepositoryBase
    {
        protected TaoKeEntities _entities;

        public RepositoryBase()
        {
            _entities = new TaoKeEntities();
        }
    }
}