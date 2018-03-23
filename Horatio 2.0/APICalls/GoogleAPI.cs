using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Horatio_2._0.Models;
using System.Net;

namespace Horatio_2._0.APICalls
{
    public class GoogleAPI
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Labor> GetLaborLocations(int id)
        {
            var locations = (from q in db.Labors.Include("Quest")
                             where q.Location != null && id == q.Quest_ID
                             select q).ToList();

            return locations;
        }


        public List<string> GetLatLng(string address)
        {
            List<string> latlng = new List<string>();
            string requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&senor=false", Uri.EscapeDataString(address));

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            string laborLat = lat.Value.ToString();
            string laborLng = lat.Value.ToString();
            latlng.Add(laborLat);
            latlng.Add(laborLng);

            return latlng;
        }
    }
}