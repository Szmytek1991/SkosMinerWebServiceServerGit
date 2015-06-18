using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServerApplication
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
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
            string conn = sm.getLink(name, surname);
            string res = conn;
            if (!conn.Equals("Not found"))
            {
                conn = sm.getFullBody(conn);
                res = "";
                foreach (string s in sm.getOrgUnit(conn))
                {
                    res += s + ";";
                }
                res += sm.getTitle(conn) + ";";
                res += sm.getGroup(conn) + ";";
                res += sm.getWorkPlace(conn) + ";";
                res += sm.getContactInfo(conn);
                long lastUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                Person newPerson = new Person(name.Trim(), surname.Trim(), res, lastUpdate);
                SkosMiner.Cache.Add(newPerson);

            }
            return res;
        }
    }
}
