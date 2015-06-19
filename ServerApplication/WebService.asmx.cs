using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServerApplication
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    /*! WebService class contains web service method for gaining information about person */
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        //! Get information about person
        /*!
          \param name string Person name
          \param surname string Person surname
          \sa getInformationAbout()
        */
        public string getInformationAbout(string name, string surname)
        {
            bool remove = false;
            Person pp = null;
            foreach (Person p in SkosMiner.Cache)
            {
                if (p.Name.Equals(name.Trim()) && p.Surname.Equals(surname.Trim()))
                {
                    long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    if ((now - p.LastUpdate) < 60000)
                    {
                        return p.Info;
                    }
                    else
                    {
                        remove = true;
                        pp = p;
                        break;
                    }
                }
            }
            if (remove)
            {
                SkosMiner.Cache.Remove(pp);
            }
            SkosMiner sm = new SkosMiner();

            return sm.getAllInfo(name, surname);
        }
    }
}
