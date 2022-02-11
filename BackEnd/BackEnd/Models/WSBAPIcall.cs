﻿namespace BackEnd.Models
{
    public class WSBAPIcall
    {
        // NOTE: made changes and proposed changes to variable names to another pattern.

        // PROPERTIES //
        //private static HttpClient _realClient = null;	// TODO: change?
                                                        // NOTE: the idea is that you don't have to guess names if you had ten or more API classes.
        private static HttpClient _realClientWSB = null;
        
        // METHODS //
        //public static HttpClient _HttpClient          // TODO: change?
        public static HttpClient MyWSBHttp
        {
            get
            {
                // TODO: change?
                /*
                if (_realClient == null)
                {
                    _realClient = new HttpClient();
                    _realClient.BaseAddress = new Uri("https://dashboard.nbshare.io/");  // WallStreetBets API URL
                }
                return _realClientWSB;
                */
                if (_realClientWSB == null)
                {
                    _realClientWSB = new HttpClient();
                    _realClientWSB.BaseAddress = new Uri("https://dashboard.nbshare.io/");  // WallStreetBets API URL
                }
                return _realClientWSB;
            }
        }

        // function returns API result of a given Ticker
        public static async Task<WSBObject> GetWSBObject(string _ticker)
        {
            //var connection = await _HttpClient.GetAsync("/api/v1/apps/reddit");  // TODO: change?
            var connection = await MyWSBHttp.GetAsync("/api/v1/apps/reddit");
            List<WSBObject> WSBObjects = await connection.Content.ReadAsAsync<List<WSBObject>>();
            WSBObject _WSBObject = new WSBObject();
            for (int i = 0; i < WSBObjects.Count; i++)
            {
                if (_ticker.ToLower() == WSBObjects[i].ticker.ToLower())
                {
                    _WSBObject = WSBObjects[i];  
                }
            }
            return _WSBObject;
        }
    }

    public class WSBObject
    {
        // PROPERTIES //
        public int no_of_comments { get; set; }
        public string sentiment { get; set; }
        public decimal sentiment_score { get; set; }
        public string ticker { get; set; }
    }
}
