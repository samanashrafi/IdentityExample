using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;

namespace IdentityExample.ServiceLayer
{
    public class FreeContentService : IFreeContentService
    {
        IUnitOfWork _uow;
        readonly IDbSet<FreeContent> _freeContents;
        
        public FreeContentService(IUnitOfWork uow)
        {
            _uow = uow;
            _freeContents = _uow.Set<FreeContent>();
        }

        public void Create(FreeContent freeContent)
        {
            _freeContents.Add(freeContent);
        }

        public IList<FreeContent> GetAllProducts()
        {
            return _freeContents.Include(x => x.SubFreeContents).ToList();
        }

        public FreeContent GetById(int id)
        {
            var model = _freeContents
                .FirstOrDefault(p => p.Id == id);
            return model;
        }        

        public void Delete(int id)
        {
            var model = _freeContents
                 .FirstOrDefault(p => p.Id == id);
            _freeContents.Remove(model);
            
        }

        public List<FreeContent> GetAll()
        {
            var model = _freeContents.ToList();
            return model;
        }



        public List<FreeContent> GetAllJson()
        {            
            var model = _freeContents.ToList();
            return model;
        }

        public void Update(FreeContent freeContent)
        {
            var model = _freeContents
                .FirstOrDefault(p => p.Id == freeContent.Id);
            model.Title = freeContent.Title;
            model.TitleEn = freeContent.TitleEn;
            model.Type = freeContent.Type;
            model.Icon = freeContent.Icon;
            model.PageTitle = freeContent.PageTitle;
            model.MetaKeyword = freeContent.MetaKeyword;
            model.MetaDescription = freeContent.MetaDescription;
            model.ShortDescription = freeContent.ShortDescription;
            //model.ShortDescription2 = freeContent.ShortDescription2;
            //model.LongDescription = freeContent.LongDescription;
            model.LastCommentDate = freeContent.LastCommentDate;
            model.LastDisLikeDate = freeContent.LastDisLikeDate;
            model.LastLikeDate = freeContent.LastLikeDate;
            model.LastEdit = freeContent.LastEdit;
            model.LastView = freeContent.LastView;
            //model.LikeCount = freeContent.LikeCount;
            //model.DisLikeCount = freeContent.DisLikeCount;
            model.Condition = freeContent.Condition;
           
            _uow.MarkAsChanged(model);
            
        }

        public List<FreeContent> GetFreeContentListBySearch(string term)
        {
            
            if (!string.IsNullOrEmpty(term))
            {
                var model = _freeContents.Where(p => p.Title.Contains(term) || p.TitleEn.Contains(term));


                return model.ToList();
            }


            return null;
        }

        IEnumerable<FreeContent> IEntityService<FreeContent>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
