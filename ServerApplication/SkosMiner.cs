using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace ServerApplication
{
    /*! SkosMiner this is base class used for mining skos site and gathering information about person */
    class SkosMiner
    {
        static List<Person> cache = new List<Person>();

        public static List<Person> Cache
        {
            get { return SkosMiner.cache; }
            set { SkosMiner.cache = value; }
        }

        //! Get full site body for person url
        /*!
          \param url string Url to personal information in skos page
          \sa getFullBody()
        */
        private String getFullBody(string url)
        {
            string pageContent = null;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myres = (HttpWebResponse)myReq.GetResponse();

            using (StreamReader sr = new StreamReader(myres.GetResponseStream()))
            {
                pageContent = sr.ReadToEnd();
            }
            return pageContent;
        }
        //! Get url to personal information in skos page
        /*!
          \param name string Person name
          \param surname string Person surname
          \sa getLink()
        */
        private String getLink(string name, string surname)
        {
            char firstLetter = surname[0];



            String pageContent = null;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://skos.agh.edu.pl/search/?letter=" + firstLetter.ToString().ToUpper());
            HttpWebResponse myres = (HttpWebResponse)myReq.GetResponse();

            using (StreamReader sr = new StreamReader(myres.GetResponseStream()))
            {
                pageContent = sr.ReadToEnd();
            }

            if (pageContent.Contains(surname.Trim() + " " + name.Trim()))
            {
                Debug.WriteLine("Found!!");
                string[] sep = { "<th>", "</th>" };
                String[] str = pageContent.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                string res = "";
                foreach (string s in str)
                {
                    if (s.Contains(surname.Trim() + " " + name.Trim()))
                    {
                        res = s;
                        break;
                    }
                }
                string[] split = res.Split('\"');

                foreach (string s in split)
                {
                    if (s.Contains("http"))
                    {
                        Debug.WriteLine(s);
                        return s;
                    }
                }
                Debug.WriteLine("Found!!");

            }
            else
            {
                return "Not found";
            }
            return String.Empty;
        }
        //! Get all organizational units for person
        /*!
          \param body string full site body for personal information page
          \sa getOrgUnit()
        */
        private List<String> getOrgUnit(string body)
        {
            string[] sep = { "<tr>", "</tr>" };
            String[] str = body.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            foreach (string s in str)
            {
                if (s.Contains("Jednostka"))
                {
                    res = s;
                    break;
                }
            }
            sep[0] = "\">";
            sep[1] = "</a";
            str = res.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            List<String> orgUnits = new List<string>();
            foreach (string s in str)
            {
                if (!s.Contains(">") || !s.Contains("<"))
                {
                    orgUnits.Add(s);
                    Debug.WriteLine(s);
                }
            }

            return orgUnits;
        }
        //! Get title of person
        /*!
          \param body string full site body for personal information page
          \sa getTitle()
        */
        private String getTitle(string body)
        {
            string[] sep = { "<tr>", "</tr>" };
            String[] str = body.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            foreach (string s in str)
            {
                if (s.Contains("Stanowisko"))
                {
                    res = s;
                    break;
                }
            }
            sep[0] = "\">";
            sep[1] = "</td>";
            str = res.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            res = "";
            List<String> orgUnits = new List<string>();
            foreach (string s in str)
            {
                if (!s.Contains(">") || !s.Contains("<"))
                {
                    res = s;
                }
            }
            Debug.WriteLine(res);
            return res;
        }
        //! Get group information
        /*!
          \param body string full site body for personal information page
          \sa getGroup()
        */
        private String getGroup(string body)
        {
            string[] sep = { "<tr>", "</tr>" };
            String[] str = body.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            foreach (string s in str)
            {
                if (s.Contains("Grupa"))
                {
                    res = s;
                    break;
                }
            }
            sep[0] = "<td>";
            sep[1] = "</td>";
            str = res.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            res = "";
            List<String> orgUnits = new List<string>();
            foreach (string s in str)
            {
                if (!s.Contains(">") || !s.Contains("<"))
                {
                    res = s;
                }
            }
            Debug.WriteLine(res);
            return res;
        }
        //! Where we can find that person in workplace
        /*!
          \param body string full site body for personal information page
          \sa getWorkPlace()
        */
        private String getWorkPlace(string body)
        {
            string[] sep = { "<tr>", "</tr>" };
            String[] str = body.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            foreach (string s in str)
            {
                if (s.Contains("Miejsce"))
                {
                    res = s;
                    break;
                }
            }
            sep[0] = "<td>";
            sep[1] = "</td>";
            str = res.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            res = "";
            List<String> orgUnits = new List<string>();
            foreach (string s in str)
            {
                if (!s.Contains(">") || !s.Contains("<"))
                {
                    res = s;
                }
            }
            Debug.WriteLine(res);
            return res;
        }
        //! Get telephone number
        /*!
          \param body string full site body for personal information page
          \sa getContactInfo()
        */
        private String getContactInfo(string body)
        {
            string[] sep = { "<tr>", "</tr>" };
            String[] str = body.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            foreach (string s in str)
            {
                if (s.Contains("Telefon"))
                {
                    res = s;
                    break;
                }
            }
            sep[0] = "\">";
            sep[1] = "</td>";
            str = res.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            res = "";
            List<String> orgUnits = new List<string>();
            foreach (string s in str)
            {
                if (!s.Contains(">") || !s.Contains("<"))
                {
                    res = s;
                }
            }
            Debug.WriteLine(res);
            return res;
        }
        //! Collect all information and return it as string
        /*!
          \param name string Person name
          \param surname string Person surname
          \sa getAllInfo()
        */
        public String getAllInfo(string name, string surname)
        {
            string conn = getLink(name, surname);
            string res = conn;
            if (!conn.Equals("Not found"))
            {
                conn = getFullBody(conn);
                res = "";
                foreach (string s in getOrgUnit(conn))
                {
                    res += s + ";";
                }
                res += getTitle(conn) + ";";
                res += getGroup(conn) + ";";
                res += getWorkPlace(conn) + ";";
                res += getContactInfo(conn);
                long lastUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                Person newPerson = new Person(name.Trim(), surname.Trim(), res, lastUpdate);
                SkosMiner.Cache.Add(newPerson);
            }
            return res;
        }
    }
}