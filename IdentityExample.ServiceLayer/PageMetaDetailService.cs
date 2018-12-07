using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityExample.DomainClasses;
using IdentityExample.DataLayer;
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace IdentityExample.ServiceLayer
{
    public class PageMetaDetailService : IPageMetaDetailService
    {

        IUnitOfWork _uow;
        static IDbSet<PageMetaDetail> _pageMetaDetail;

        public PageMetaDetailService(IUnitOfWork uow)
        {
            _uow = uow;
            _pageMetaDetail = _uow.Set<PageMetaDetail>();
        }

        public List<PageMetaDetail> GetPageMetaDetailListBySearch(string term)
        {

            if (!string.IsNullOrEmpty(term))
            {
                var model = _pageMetaDetail.Where(p => p.Title.Contains(term));


                return model.ToList();
            }


            return null;
        }


        public void Create(PageMetaDetail pageMetaDetail)
        {
            _pageMetaDetail.Add(pageMetaDetail);
        }

        public void Delete(int id)
        {
            var model = _pageMetaDetail
                 .FirstOrDefault(p => p.Id == id);
            _pageMetaDetail.Remove(model);
            _uow.SaveAllChanges();
        }

        public IEnumerable<PageMetaDetail> GetAll()
        {
            var model = _pageMetaDetail.ToList();
            return model;
        }

        public PageMetaDetail GetById(int id)
        {
            var model = _pageMetaDetail
                  .FirstOrDefault(p => p.Id == id);
            return model;
        }

        public List<PageMetaDetail> GetPageMetaDetailList(int id)
        {
            var model = _pageMetaDetail.ToList();
            return model;
        }

        public List<PageMetaDetail> GetPageMetaDetailList()
        {
            var model = _pageMetaDetail.ToList();
            return model;
        }

        public void Update(PageMetaDetail pageMetaDetail)
        {
            var model = _pageMetaDetail
                  .FirstOrDefault(p => p.Id == pageMetaDetail.Id);
            model.PageUrl = pageMetaDetail.PageUrl;
            model.Title = pageMetaDetail.Title;
            model.MetaKeyWords = pageMetaDetail.MetaKeyWords;
            model.MetaDescription = pageMetaDetail.MetaDescription;

            _uow.MarkAsChanged(model);
            _uow.SaveAllChanges();

        }

        public static string UpdateMetaDetails(string pageUrl)
        {
            StringBuilder sbMetaTags = new StringBuilder();
            string metaDescribe = "";
            string keyWord = "";
            PageMetaDetail titleObject = new PageMetaDetail();

            var obj = _pageMetaDetail.FirstOrDefault(a => a.PageUrl == pageUrl);

            if (obj == null)
            {
               
                sbMetaTags.Append("<title>" + "امانت بار تهران" + "</title>");
                sbMetaTags.Append(Environment.NewLine);
                //metaDescribe = "امانت بار تهران";
                //keyWord = "امانت بار تهران";
                sbMetaTags.Append("<meta name=" + "\"description\"" + "Content =" + "\"" + "امانت بار تهران" + "\"=" + ">");
                sbMetaTags.Append(Environment.NewLine);

                sbMetaTags.Append("<meta name=" + "\"keywords\"" + "Content =" + "\"" + "امانت بار تهران" + "\"=" + ">");
            }
            else
            {
                sbMetaTags.Append("<title>" + obj.Title + "</title>");
                sbMetaTags.Append(Environment.NewLine);
                metaDescribe = obj.MetaDescription.ToString();
                keyWord = obj.MetaKeyWords.ToString();
                sbMetaTags.Append("<meta name=" + "\"description\"" + "Content =" + "\"" + metaDescribe + "\"=" + ">");
                sbMetaTags.Append(Environment.NewLine);

                sbMetaTags.Append("<meta name=" + "\"keywords\"" + "Content =" + "\"" + keyWord + "\"=" + ">");
            }

            


            return sbMetaTags.ToString();
        }
    }
}
