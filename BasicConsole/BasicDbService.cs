using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BasicConsole
{
    public class BasicDbService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _hostAddr = "https://localhost:44304/";

        //=============================================
        // Returns an ARRAY of Characters
        //=============================================
        public async Task<List<T>> GetCharacterByNameAsync<T>(string name)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_hostAddr}api/Character?name={name}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<T>>();
            }

            return default;
        }

        //=============================================
        // Returns a SINGLE RESULT which contains an Array
        //=============================================
        public async Task<CharDetail> GetCharacterByIdAsync(int charId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_hostAddr}api/Character?charId={charId}");

            if (response.IsSuccessStatusCode)
            {
                var newCharDetail = await response.Content.ReadAsAsync<CharDetail>();
                return newCharDetail;
            }

            return default;
        }

        //=============================================
        // Returns an ARRAY of Characters
        //=============================================
        public async Task<List<T>> GetAllCharAsync<T>()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_hostAddr}api/Character");

            //var response = await _httpClient.GetAsync();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<T>>();
            }

            return default;
        }

        //=============================================
        // Returns a SINGLE RESULT of Media
        //=============================================
        public async Task<MediaGet> GetMediaByIdAsync(int mediaId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_hostAddr}api/Media/{mediaId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<MediaGet>();
            }

            return default;
        }

        //=============================================
        // Returns an ARRAY of Media
        //=============================================
        public async Task<List<T>> GetAllMediaAsync<T>()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_hostAddr}api/Media/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<T>>();
            }

            return default;
        }

        ////=============================================
        //// Returns ???????
        ////=============================================
        //public async Task<MediaGet> PostCharMediaAsync<T>()
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync($"{_hostAddr}api/CharMedia");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<List<T>>();
        //    }

        //    return default;
        //}

        //=============================================
        // Returns ???????
        //=============================================
        public string PostCharMediaAsync(int charId, int mediaId)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{_hostAddr}api/CharMedia");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    CharId = charId,
                    MediaId = mediaId
                });

                streamWriter.Write(json);
            }
            
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    return null;
                }

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    return response;
                }
            }
            //catch (Exception ex)
            //{
            //    // Something more serious happened
            //    // like for example you don't have network access
            //    // we cannot talk about a server exception here as
            //    // the server probably was never reached
            //}
            return "Unknown Error";
        }
    }
}
