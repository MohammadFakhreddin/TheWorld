using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        private ILogger _logger;
        private IConfigurationRoot _config;
        
        public GeoCoordsService(ILogger<GeoCoordsService> _logger, IConfigurationRoot _config)
        {
            this._logger = _logger;
            this._config = _config;
        }

        public async Task<GeoCoordsResult> getCoordsAsync(string name)
        {
            GeoCoordsResult geoCoordResult = new GeoCoordsResult()
            {
                success = false,
                message = "Failed to get coordinates"
            };

            string apiKey = _config["Keys:BindKey"];
            string encodedName = WebUtility.UrlEncode(name);
            string url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);
            JObject results = JObject.Parse(json);
            JToken resources = results["resourceSets"][0]["resources"];
            if (!results["resourceSets"][0]["resources"].HasValues)
            {
                geoCoordResult.message = $"could not find {name} as a location";
            }
            else
            {
                string confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    geoCoordResult.message = $"could not find  a confident map for {name} as a location";
                }
                else
                {
                    JToken coords = resources[0]["geocodePoints"][0]["coordinates"];
                    geoCoordResult.latitude = (double)coords[0];
                    geoCoordResult.longtitude = (double)coords[1];
                    geoCoordResult.success = true;
                    geoCoordResult.message = "Success!";
                }
            }
            return geoCoordResult;
        }
    }
}
