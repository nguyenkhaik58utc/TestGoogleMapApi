using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestGoogleMapApi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        private void BindGMap(DataTable datatable)
        {
            try
            {
                List<ProgramAddresses> AddressList = new List<ProgramAddresses>();
                foreach (DataRow dr in datatable.Rows)
                {
                    string FullAddress = dr["Address"].ToString() + " " + dr["City"].ToString() + ", " + dr["Country"].ToString() + " " + dr["StateName"].ToString() + " " + dr["ZipCode"].ToString();
                    ProgramAddresses MapAddress = new ProgramAddresses();
                    MapAddress.description = FullAddress;
                    var locationService = new GoogleLocationService();
                    var point = locationService.GetLatLongFromAddress(FullAddress);
                    MapAddress.lat = point.Latitude;
                    MapAddress.lng = point.Longitude;
                    AddressList.Add(MapAddress);
                }
                string jsonString = JsonSerializer<List<ProgramAddresses>>(AddressList);
                ScriptManager.RegisterArrayDeclaration(Page, "markers", jsonString);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "GoogleMap();", true);
            }
            catch (Exception ex)
            {
            }
        }
    }
}