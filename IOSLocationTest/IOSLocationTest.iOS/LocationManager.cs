using System;
using System.Linq;
using CoreLocation;
using UIKit;

namespace IOSLocationTest.iOS
{
    public class LocationManager
    {
        private static readonly Lazy<LocationManager> instance = new Lazy<LocationManager>(() => new LocationManager());
        public static LocationManager Instance { get { return instance.Value; } }

        protected CLLocationManager locMgr;

        public LocationManager()
        {
            this.locMgr = new CLLocationManager();
            this.locMgr.PausesLocationUpdatesAutomatically = true;// = false;

            // iOS 8 has additional permissions requirements
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                locMgr.RequestAlwaysAuthorization(); // works in background
                //locMgr.RequestWhenInUseAuthorization (); // only in foreground
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                locMgr.AllowsBackgroundLocationUpdates = false;
            }
        }

        public CLLocationManager LocMgr
        {
            get { return this.locMgr; }
        }

        public void StartLocationUpdates()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                //set the desired accuracy, in meters
                LocMgr.DesiredAccuracy = 1;
                LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    System.Diagnostics.Debug.WriteLine($"@@@@@@@@@@@@@@@@@LocationsUpdated {DateTime.Now} and count is {e.Locations.Count()}");
                };
                LocMgr.AllowsBackgroundLocationUpdates = true;
                LocMgr.PausesLocationUpdatesAutomatically = false;
                LocMgr.StartUpdatingLocation();
            }
        }
    }
}