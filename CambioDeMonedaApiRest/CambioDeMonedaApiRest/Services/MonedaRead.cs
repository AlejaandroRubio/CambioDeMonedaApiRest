using CambioDeMonedaApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CambioDeMonedaApiRest.Services
{
    public class MonedaRead
    {

        private const string CacheKey = "ContactStore";
        private static int idCounter = 0;
        public MonedaRequest[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (MonedaRequest[])ctx.Cache[CacheKey];
            }

            return new MonedaRequest[]
            {
                new MonedaRequest { Id = GetNextId(),
                                    MonedaOrigen = "Placeholder",
                                    MonedaDestino = "Placeholder",
                                    Monto = 0}
            };
        }

        private int GetNextId()
        {
            return System.Threading.Interlocked.Increment(ref idCounter);
        }



    }
}