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
                return (DateTime?)null;
            }
        }
    }
}
