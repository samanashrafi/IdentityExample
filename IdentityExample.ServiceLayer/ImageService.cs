using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;

namespace IdentityExample.ServiceLayer
{
    public class ImageService : IImageService
    {
        IUnitOfWork _uow;
        readonly IDbSet<Images> _image;
        public ImageService(IUnitOfWork uow)
        {
            _uow = uow;
            _image = _uow.Set<Images>();
        }

        public void Create(Images img)
        {
            _image.Add(img);
        }

        public Images GetById(int id)
        {
            var model = _image
                .FirstOrDefault(p => p.Id == id);
            return model;
        }

        public void Delete(int id)
        {
            var model = _image
                 .FirstOrDefault(p => p.Id == id);
            _image.Remove(model);
            
        }

        public IEnumerable<Images> GetAll()
        {
            var model = _image.ToList();
            return model;
        }



        public List<Images> GetAllJson()
        {            
            var model = _image.ToList();
            return model;
        }

        public void Update(Images img)
        {
            var model = _image
                .FirstOrDefault(p => p.Id == img.Id);
            _uow.MarkAsChanged(model);
            
        }

        FreeContent IImageService.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
