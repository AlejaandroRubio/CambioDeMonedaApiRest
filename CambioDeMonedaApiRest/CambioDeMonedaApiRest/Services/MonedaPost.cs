using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CambioDeMonedaApiRest.Models;


namespace CambioDeMonedaApiRest.Services
{
    public class MonedaPost
    {
        private const string CacheKey = "ContactStore";
        private static int idCounter = 0;

        public bool SaveContact(MonedaRequest requestM)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    requestM.Id = GetNextId();

                    var currentData = ((MonedaRequest[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(requestM);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        private int GetNextId()
        {
            return System.Threading.Interlocked.Increment(ref idCounter);
        }


    }
}