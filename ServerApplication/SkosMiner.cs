using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace ServerApplication
{
    class SkosMiner
    {
        public String getFullBody(string url)
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
        public String getLink(string name, string surname)
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
        public List<String> getOrgUnit(string body)
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
        public String getTitle(string body)
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
        public String getGroup(string body)
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
        public String getWorkPlace(string body)
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
        public String getContactInfo(string body)
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
    }
}