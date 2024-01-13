using System;
using System.Configuration;
using System.Web;

namespace RequestHeaderUnset
{
    public class RequestHeaderUnset : IHttpModule
    {
        #region IHttpModule implementation

        public void Filter(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpRequest request = app.Context.Request;
            string conf = ConfigurationManager.AppSettings["RequestHeaderUnsetList"];
            if (conf == null)
                return;
            string[] RequestHeaderUnsetList = conf.Split(',');
            foreach (string h in RequestHeaderUnsetList)
            {
                request.Headers.Remove(h);
            }
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Filter);
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
