using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiGirl360.Models.ViewModel;

namespace HiGirl360.Models.Repository
{
    public class MenuRepository
    {
        private TaoKeEntities _entities;

        public MenuRepository()
        {
            _entities = new TaoKeEntities();
        }

        //取菜单
        public List<Menu> GetMenu()
        {
            List<Menu> _Menu = new List<Menu>();
            //1. 取到顶级菜单
            var menus = _entities.TAOKECATEGORY.Where(p => p.CateStatus == "A").ToList();

            var _topMenus = menus.Where(p => p.ParentCateID == 0);
            foreach (var item in _topMenus)
            {
                var menu = new Menu
                {
                    CateID = item.CateID,
                    CateName = item.CateName
                };
                menu.SubMenu = new List<SubMenu>();
                //取子级菜单
                var _subMenus = menus.Where(p => p.ParentCateID == item.CateID).OrderByDescending(p => p.CateClickCount).Take(5);
                foreach (var subItem in _subMenus)
                {
                    menu.SubMenu.Add(new SubMenu
                    {
                        CateID = subItem.CateID,
                        CateName = subItem.CateName,
                        ParentCateID = subItem.ParentCateID
                    });
                }
                _Menu.Add(menu);
            }

            return _Menu;
        }
    }
}