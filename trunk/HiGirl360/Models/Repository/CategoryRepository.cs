using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiGirl360.Models.ViewModel;

namespace HiGirl360.Models.Repository
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        /// <summary>
        /// 首面查询热门分类
        /// </summary>
        /// <returns></returns>
        public IList<Category> GetTopCategory()
        {
            List<Category> _TopCategory = new List<Category>();

            var _category = _entities.TAOKECATEGORY.Where(p => p.CateStatus == "A").ToList();

            var _topCategory = _category.Where(p => p.ParentCateID == 0).OrderBy(p => p.CateSort);
            foreach (var topItem in _topCategory)
            {
                var category = new Category
                {
                    CateID = topItem.CateID,
                    CateName = topItem.CateName
                };

                category.SubCategory = new  List<SubCategory>();
                var _subCategory = _category.Where(p => p.ParentCateID == category.CateID).OrderBy(p => p.CateSort);

                foreach (var subItem in _subCategory)
                {
                    category.SubCategory.Add(new SubCategory
                    {
                        CateID = subItem.CateID,
                        CateName = subItem.CateName,
                        ParentCateID = subItem.ParentCateID
                    });
                }
                _TopCategory.Add(category);
            }

            return _TopCategory;
        }
    }
}