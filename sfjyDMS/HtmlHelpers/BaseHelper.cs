using System;
using System.Text;
using System.Web.Mvc;

namespace buwap
{
    public static class BaseHelper
    {
        public static MvcHtmlString ImagePath(this HtmlHelper html, string img_path)
        {

            if (img_path == null || img_path.Trim() == string.Empty)
                return MvcHtmlString.Create(img_path);
            else
            { 
                var helper = new UrlHelper(html.ViewContext.RequestContext);
                return MvcHtmlString.Create(helper.Content("~/Content/images/no_photo.jpg"));
            } 
        }
    }
}