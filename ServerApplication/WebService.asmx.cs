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
            //test git
            SkosMiner sm = new SkosMiner();
            string conn = sm.getLink(name, surname);
            string res = conn;
            if (!conn.Equals("Not found"))
            {
                conn = sm.getFullBody(conn);
                res = "";
                foreach (string s in sm.getOrgUnit(conn))
                {
                    res += s + "|";
                }
                res += sm.getTitle(conn) + "|";
                res += sm.getGroup(conn) + "|";
                res += sm.getWorkPlace(conn) + "|";
                res += sm.getContactInfo(conn);
            }
            return res;
        }
    }
}
