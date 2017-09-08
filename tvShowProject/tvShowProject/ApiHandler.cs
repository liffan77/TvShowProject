using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace tvShowProject
{
    //public class ApiHandler
    //{
    //public string GetShowMetaData(string nameOfShow)
    //{
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://epguides.frecar.no/show/" + nameOfShow + "/info/");
    //    string responseString = string.Empty;

    //    try
    //    {
    //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            Stream stream = response.GetResponseStream();
    //            StreamReader reader = new StreamReader(stream);

    //            responseString = reader.ReadToEnd();
    //            reader.Close();
    //            response.Close();
    //        }
    //    }
    //    catch (Exception exception)
    //    {
    //        responseString = exception.Message;
    //        //throw;
    //    }
    //    return responseString;
    //}

    //public string GetLatestEpisodeInfo(string nameOfShow)
    //{
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://epguides.frecar.no/show/" + nameOfShow + "/last/");
    //    string responseString = string.Empty;

    //    try
    //    {
    //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            Stream stream = response.GetResponseStream();
    //            StreamReader reader = new StreamReader(stream);

    //            responseString = reader.ReadToEnd();
    //            reader.Close();
    //            response.Close();
    //        }
    //    }
    //    catch (Exception exception)
    //    {
    //        responseString = exception.Message;
    //        //throw;
    //    }
    //    return responseString;
    //}

    //public string GetAllEpisodes(string nameOfShow)
    //{
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://epguides.frecar.no/show/" + nameOfShow);
    //    string responseString = string.Empty;

    //    try
    //    {
    //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            Stream stream = response.GetResponseStream();
    //            StreamReader reader = new StreamReader(stream);

    //            responseString = reader.ReadToEnd();
    //            reader.Close();
    //            response.Close();
    //        }
    //    }
    //    catch (Exception exception)
    //    {
    //        responseString = exception.Message;
    //        //throw;
    //    }
    //    return responseString;
    //}
    //}
    //}


    public class ApiHandler
    {

        public string ShowSearch(string show)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/search/shows?q=" + show);
            string responseString = string.Empty;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    responseString = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                }
            }
            catch (Exception exception)
            {
                responseString = exception.Message;
                //throw;
            }
            return responseString;

        }

        public string GetShowDetails(string imdbId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/lookup/shows?imdb=" + imdbId);
            string responseString = string.Empty;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    responseString = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                }
            }
            catch (Exception exception)
            {
                responseString = exception.Message;
                //throw;
            }
            return responseString;
        }

    }
}
