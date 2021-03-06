﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using tvShowProject.Models;
using tvShowProject.Models.VM;

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


    //public class ApiHandler
    //{
    //    public string ShowSearch(string show)
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/search/shows?q=" + show);
    //        string responseString = string.Empty;

    //        try
    //        {
    //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                Stream stream = response.GetResponseStream();
    //                StreamReader reader = new StreamReader(stream);

    //                responseString = reader.ReadToEnd();
    //                reader.Close();
    //                response.Close();
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            responseString = exception.Message;
    //            //throw;
    //        }
    //        return responseString;

    //    }

    //    public string GetShowDetails(string imdbId)
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/lookup/shows?imdb=" + imdbId);
    //        string responseString = string.Empty;

    //        try
    //        {
    //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                Stream stream = response.GetResponseStream();
    //                StreamReader reader = new StreamReader(stream);

    //                responseString = reader.ReadToEnd();
    //                reader.Close();
    //                response.Close();
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            responseString = exception.Message;
    //            //throw;
    //        }
    //        return responseString;
    //    }

    //    public string GetEpisodes(string imdbId)
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/lookup/shows?imdb=" + imdbId);
    //        string responseString = string.Empty;

    //        try
    //        {
    //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                Stream stream = response.GetResponseStream();
    //                StreamReader reader = new StreamReader(stream);

    //                responseString = reader.ReadToEnd();
    //                reader.Close();
    //                response.Close();
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            responseString = exception.Message;
    //            //throw;
    //        }
    //        return responseString;
    //    }
    //}

    class ApiHandler
    {
        public static SearchResult[] SearchForShow(string searchString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/search/shows?q=" + searchString);
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

            SearchResult[] searchResults = JsonConvert.DeserializeObject<SearchResult[]>(responseString);
            return searchResults;
        }

        public static Episode[] GetEmbeddedEpisodes(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/shows/" + id + "/episodes");
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

            Episode[] episodes = JsonConvert.DeserializeObject<Episode[]>(responseString);
            return episodes;
        }

        public static Episode[] GetShowsEpisodes(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/shows/" + id + "/episodes");
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

            Episode[] episodes = JsonConvert.DeserializeObject<Episode[]>(responseString);
            return episodes;

        }

        public static TvShow GetTvShowAndEpisodeDetails(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://api.tvmaze.com/shows/{id}?embed=episodes");
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

            TvShow tvShow = JsonConvert.DeserializeObject<TvShow>(responseString);
            return tvShow;
        }

        public static Episode GetLatestEpisode(int? id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.tvmaze.com/shows/" + id + "/episodes");
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

            Episode[] episodes = JsonConvert.DeserializeObject<Episode[]>(responseString);
            return episodes[episodes.Length - 1];
        }

        public static bool CheckIfReleasedToday(int? id)
        {
            Episode e = ApiHandler.GetLatestEpisode(id);
            if (e.Airdate.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                return true;
            else
                return false;
        }

        public static bool CheckIfAnyEpisodeReleasedToday(int? id)
        {
            bool releasedToday = false;
            Episode[] episodes = ApiHandler.GetShowsEpisodes((int)id);

            foreach (var episode in episodes)
            {
                if (episode.Airdate.HasValue)
                {

                    if (episode.Airdate.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                        releasedToday = true;
                }
            }

            return releasedToday;
        }


    }
}
