using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;

namespace IdentityExample.ServiceLayer
{
    public class SubFreeContentService : ISubFreeContentService
    {
        IUnitOfWork _uow;
        readonly IDbSet<SubFreeContent> _subFreeContents;
        public SubFreeContentService(IUnitOfWork uow)
        {
            _uow = uow;
            _subFreeContents = _uow.Set<SubFreeContent>();
        }

        public void Create(SubFreeContent subFreeContent)
        {
            _subFreeContents.Add(subFreeContent);
        }

        public IList<SubFreeContent> GetAllSubFreeContent()
        {
            return _subFreeContents.ToList();
        }

        public List<SubFreeContent> GetAllJson()
        {
            var model = _subFreeContents.ToList();
            return model;
        }

        public List<SubFreeContent> GetSubFreeContentByFreeId(int id)
        {
            var model = _subFreeContents.
                Where(p => p.FreeContentId == id).ToList();
            return model;
        }

        public SubFreeContent GetById(int id)
        {
            var model = _subFreeContents
                .FirstOrDefault(p => p.Id == id);

            return model;
        }

        public SubFreeContent GetSubByFreeContentId(int id)
        {
            var model = _subFreeContents
                .FirstOrDefault(p => p.FreeContentId == id);
            return model;
        }

        public void Delete(int id)
        {
            var model = _subFreeContents
                 .FirstOrDefault(p => p.Id == id);
            _subFreeContents.Remove(model);
            _uow.SaveAllChanges();
        }

        public IEnumerable<SubFreeContent> GetAll()
        {
            var model = _subFreeContents.ToList();
            return model;
        }

        public void Update(SubFreeContent subFreeContent)
        {
            var model = _subFreeContents
                .FirstOrDefault(p => p.Id == subFreeContent.Id);
            // _subFreeContents.Remove(oldModel);
            //_subFreeContents.Add(subFreeContent);
            model.Condition = subFreeContent.Condition;
            model.DisLikeCount = subFreeContent.DisLikeCount;
            model.EnTitle = subFreeContent.EnTitle;
            model.FreeContentId = subFreeContent.FreeContentId;
            model.Icon = subFreeContent.Icon;
            model.LastCommentDate = subFreeContent.LastCommentDate;
            model.LastDisLikeDate = subFreeContent.LastDisLikeDate;
            model.LastEdit = subFreeContent.LastEdit;
            model.LastLikeDate = subFreeContent.LastLikeDate;
            model.LastView = subFreeContent.LastView;
            model.LikeCount = subFreeContent.LikeCount;
            model.LongDescription = subFreeContent.LongDescription;
            model.MetaDescription = subFreeContent.MetaDescription;
            model.MetaKeyword = subFreeContent.MetaKeyword;
            model.PageTitle = subFreeContent.PageTitle;
            model.ShortDescription = subFreeContent.ShortDescription;
            model.ShortDescription2 = subFreeContent.ShortDescription2;
            model.Title = subFreeContent.Title;
            model.Image.Name = subFreeContent.Image.Name;
            model.Image.Url = subFreeContent.Image.Url;
            //model.Id = subFreeContent.Id;
            _uow.MarkAsChanged(model);
            _uow.SaveAllChanges();
        }

        public List<SubFreeContent> GetSubFreeContentListBySearch(string term)
        {

            if (!string.IsNullOrEmpty(term))
            {
                var model = _subFreeContents.Where(p => p.Title.Contains(term) || p.EnTitle.Contains(term));


                return model.ToList();
            }


            return null;
        }

        public SubFreeContent GetSubFreeContentById(int id)
        {
            var model = _subFreeContents.
                FirstOrDefault(p => p.Id == id);
            return model;
        }
    }
}
