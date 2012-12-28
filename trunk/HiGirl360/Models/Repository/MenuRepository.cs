using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiGirl360.Models.ViewModel;

namespace HiGirl360.Models.Repository
{
    public class MenuRepository : RepositoryBase, IMenuRepository
    {
        //取菜单
        public IList<Menu> GetMenu()
        {
            List<Menu> _Menu = new List<Menu>();
            //1. 取到顶级菜单
            var menus = _entities.TAOKEMENU.Where(p => p.MenuStatus == "A").ToList();

            var _topMenus = menus.Where(p => p.ParentMenuID == 0).OrderBy(p => p.MenuSort);
            foreach (var item in _topMenus)
            {
                var menu = new Menu
                {
                    MenuID = item.MenuID,
                    MenuName = item.MenuName,
                    MenuUrl = item.MenuUrl
                };
                menu.SubMenu = new List<SubMenu>();
                //取子级菜单
                var _subMenus = menus.Where(p => p.ParentMenuID == item.MenuID).OrderByDescending(p => p.MenuSort).Take(10);
                foreach (var subItem in _subMenus)
                {
                    menu.SubMenu.Add(new SubMenu
                    {
                        MenuID = subItem.MenuID,
                        MenuName = subItem.MenuName,
                        MenuUrl = subItem.MenuUrl
                    });
                }
                _Menu.Add(menu);
            }

            return _Menu;
        }
    }
}