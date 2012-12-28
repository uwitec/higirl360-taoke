using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.Repository
{
    public class ADRepository : RepositoryBase, IAdRepository
    {
        public IQueryable<TAOKEAD> GetHomeAdList()
        {
            //更新过期的广告
            var entity = _entities.TAOKEAD.Where(p=>(p.AdStatus =="A" && p.AdEndTime < DateTime.Now));
            foreach (var item in entity)
	        {
                item.AdStatus="D";
            }
            _entities.SaveChanges();

            return _entities.TAOKEAD.Where(p => (p.AdStatus == "A" && p.AdStartTime < DateTime.Now && p.AdEndTime > DateTime.Now));
        }
    }
}