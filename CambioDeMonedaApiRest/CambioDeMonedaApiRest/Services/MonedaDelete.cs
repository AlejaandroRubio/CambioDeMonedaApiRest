using CambioDeMonedaApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CambioDeMonedaApiRest.Services
{
    public class MonedaDelete
    {
        private const string CacheKey = "ContactStore";

        public void DeleteMoneda(MonedaRequest request)
        {
            var monedas = GetAllMonedas().ToList();

            // Encuentra la solicitud de moneda con el ID proporcionado y elimínala
            var monedaToRemove = monedas.FirstOrDefault(m => m.Id == request.Id);
            if (monedaToRemove != null)
            {
                monedas.Remove(monedaToRemove);
                SaveMonedasToCache(monedas.ToArray());
            }
        }

        private MonedaRequest[] GetAllMonedas()
        {
            // Obtén todas las solicitudes de moneda desde la caché o cualquier otro almacenamiento que estés utilizando
            var monedas = (MonedaRequest[])HttpContext.Current.Cache[CacheKey];
            return monedas ?? new MonedaRequest[0];
        }

        private void SaveMonedasToCache(MonedaRequest[] monedas)
        {
            // Guarda las solicitudes de moneda en la caché o cualquier otro almacenamiento que estés utilizando
            HttpContext.Current.Cache[CacheKey] = monedas;
        }
    }
}