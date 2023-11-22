using CambioDeMonedaApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CambioDeMonedaApiRest.Services;

namespace CambioDeMonedaApiRest.Services
{
    public class MonedaRead
    {

        private const string CacheKey = "ContactStore";
        private const decimal MontoDeTazaDeCambio= 1.5m;
        
        public MonedaRequest[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (MonedaRequest[])ctx.Cache[CacheKey];
            }

            return new MonedaRequest[]
            {
                new MonedaRequest { Id = MonedaPost.GetNextId(),
                                    MonedaOrigen = "Placeholder",
                                    MonedaDestino = "Placeholder",
                                    Monto = 0,}
            };
        }

        public MonedaResponse ConvertirMoneda(MonedaRequest solicitud)
        {
            // Aquí utilizamos el conversor de moneda para realizar la conversión
            var respuesta = new MonedaResponse
            {
                Id = solicitud.Id,
                MonedaOrigen = solicitud.MonedaOrigen,
                MonedaDestino = solicitud.MonedaDestino,
                Monto = conversorDeMoneda(),
                TazaDeCambio = MontoDeTazaDeCambio
            };

            // Puedes guardar la respuesta en caché o en una base de datos según tus necesidades
           

            return new MonedaResponse
            {
                Id = respuesta.Id,
                MonedaOrigen = respuesta.MonedaOrigen,
                MonedaDestino = respuesta.MonedaDestino,
                Monto = respuesta.Monto,
                TazaDeCambio = respuesta.TazaDeCambio
            };
        }

        public decimal conversorDeMoneda() {

            MonedaRequest[] monedas = GetAllContacts();


            return monedas[MonedaPost.GetIdValue()].Monto * MontoDeTazaDeCambio;

            




        }




    }
}