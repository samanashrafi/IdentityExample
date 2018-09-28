using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;

namespace IdentityExample.ServiceLayer
{
    public class ContactService : IContactService
    {
        IUnitOfWork _uow;
        readonly IDbSet<Contact> _contact;
        
        public ContactService(IUnitOfWork uow)
        {
            _uow = uow;
            _contact = _uow.Set<Contact>();
        }

        public void Create(Contact Contact)
        {
            _contact.Add(Contact);
        }

        public IList<Contact> GetAllProducts()
        {
            return _contact.Include(x => x.Title).ToList();
        }

        //public List<Contact> GeContactByType(string type)
        //{
        //    var model = _contact.Where(p => p.GetType == type).ToList();
        //    return model;
        //}

        public Contact GetById(int id)
        {
            var model = _contact
                .FirstOrDefault(p => p.Id == id);
            return model;
        }        

        public void Delete(int id)
        {
            var model = _contact
                 .FirstOrDefault(p => p.Id == id);
            _contact.Remove(model);
            
        }

        public List<Contact> GetAll()
        {
            var model = _contact.ToList();
            return model;
        }



        public List<Contact> GetAllJson()
        {            
            var model = _contact.ToList();
            return model;
        }

        public void Update(Contact Contact)
        {
            var model = _contact
                .FirstOrDefault(p => p.Id == Contact.Id);
            model.Title = Contact.Title;
            model.Email = Contact.Email;
            model.Name = Contact.Name;
            model.Message = Contact.Message;        
           
            _uow.MarkAsChanged(model);
            
        }

        public List<Contact> GetContactListBySearch(string term)
        {
            
            if (!string.IsNullOrEmpty(term))
            {
                var model = _contact.Where(p => p.Title.Contains(term) || p.Title.Contains(term));


                return model.ToList();
            }


            return null;
        }

        IEnumerable<Contact> IEntityService<Contact>.GetAll()
        {
            throw new NotImplementedException();
        }

        FreeContent IContactService.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
