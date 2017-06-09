using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace PreventCataclysmSystems
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public static class Extensions
    {
        public static string ToJson(this object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static int ToInt32(this object o)
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime? ToDateTime(this object o)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime RoundToNearestInterval(this DateTime dt, TimeSpan d)
        {
            int f = 0;

            double m = (double)(dt.Ticks % d.Ticks) / d.Ticks;

            if (m >= 0.5)
                f = 1;

            return new DateTime(((dt.Ticks / d.Ticks) + f) * d.Ticks);
        }
    }
}
