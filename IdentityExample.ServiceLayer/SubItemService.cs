using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;

namespace IdentityExample.ServiceLayer
{
    public class SubItemService : ISubItemService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<SubItem> _subItem;
        public SubItemService(IUnitOfWork uow)
        {
            _uow = uow;
            _subItem = _uow.Set<SubItem>();
        }
        public void Create(SubItem subItem)
        {
            _subItem.Add(subItem);
            _uow.SaveAllChanges();
        }

        public void Delete(int id)
        {
            var model = _subItem
                 .FirstOrDefault(p => p.Id == id);
            _subItem.Remove(model);
            _uow.SaveAllChanges();
        }

        public IEnumerable<SubItem> GetAll()
        {
            return _subItem.ToList();
        }

        public SubItem GetById(int id)
        {
            var model = _subItem.FirstOrDefault(p => p.Id == id);
            return model;
        }

        public SubItem GetSubItemBySubFreeContentId(int? id)
        {
            return _subItem.FirstOrDefault(p => p.SubFreeContentId == id);
        }

        public List<SubItem> GetSubItemListBySubFreeContentId(int? id)
        {
            return _subItem.Where(p => p.SubFreeContentId == id).ToList();
        }

        public void Update(SubItem entity)
        {
            var model = _subItem.FirstOrDefault(p => p.Id == entity.Id);

            model.Condition = entity.Condition;
            model.DisLikeCount = entity.DisLikeCount;
            model.EnTitle = entity.EnTitle;
            model.SubFreeContentId = entity.SubFreeContentId;
            model.Icon = entity.Icon;
            model.LastCommentDate = entity.LastCommentDate;
            model.LastDisLikeDate = entity.LastDisLikeDate;
            model.LastEdit = entity.LastEdit;
            model.LastLikeDate = entity.LastLikeDate;
            model.LastView = entity.LastView;
            model.LikeCount = entity.LikeCount;
            model.LongDescription = entity.LongDescription;
            model.MetaDescription = entity.MetaDescription;
            model.MetaKeyword = entity.MetaKeyword;
            model.PageTitle = entity.PageTitle;
            model.ShortDescription = entity.ShortDescription;
            model.ShortDescription2 = entity.ShortDescription2;
            model.Title = entity.Title;
            
            _uow.MarkAsChanged(model);
            _uow.SaveAllChanges();

        }

        public List<SubItem> GetSubItemListBySearch(string term)
        {

            if (!string.IsNullOrEmpty(term))
            {
                var model = _subItem.Where(p => p.Title.Contains(term) || p.EnTitle.Contains(term));


                return model.ToList();
            }


            return null;
        }

       
    }
}
