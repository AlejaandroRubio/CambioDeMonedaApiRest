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
        private const decimal TazaDeCambioUSD_EUR= 1.5m;
        private const decimal TazaDeCambioEUR_JPY = 163m;
        private const decimal TazaDeCambioUSD_JPY = 149m;
        
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
                                    Monto = 0,
                                  }
            };
        }

        public MonedaResponse ConvertirMoneda(MonedaRequest solicitud)
        {
            // Aquí utilizamos el conversor de moneda para realizar la conversión
            var respuesta = new MonedaResponse
            {
                Id = solicitud.Id,
                Divisa = solicitud.MonedaDestino,
                TazaDeCambio = TazaDeCambioFinal(solicitud),
                Monto = conversorDeMoneda(solicitud)
               
            };

            // Puedes guardar la respuesta en caché o en una base de datos según tus necesidades
           

            return new MonedaResponse
            {
                Id = respuesta.Id,
                Divisa = respuesta.Divisa,
                Monto = respuesta.Monto,
                TazaDeCambio = respuesta.TazaDeCambio
            };
        }

        public decimal TazaDeCambioFinal(MonedaRequest solicitud) { 

            if ( solicitud.MonedaOrigen== "USD"&&solicitud.MonedaDestino == "EUR") { return TazaDeCambioUSD_EUR; }
            else if (solicitud.MonedaOrigen=="EUR" && solicitud.MonedaDestino == "USD") { return TazaDeCambioUSD_EUR; }
            else if ( solicitud.MonedaOrigen == "EUR" && solicitud.MonedaDestino == "JPY") { return TazaDeCambioEUR_JPY; }
            else if (solicitud.MonedaOrigen == "JPY" && solicitud.MonedaDestino == "EUR") { return TazaDeCambioEUR_JPY; }
            else if (solicitud.MonedaOrigen == "USD" && solicitud.MonedaDestino == "JPY") { return TazaDeCambioUSD_JPY; }
            else if (solicitud.MonedaOrigen == "JPY" && solicitud.MonedaDestino == "USD") { return TazaDeCambioUSD_JPY; }
            else { return 0; }
        
        }

        public decimal conversorDeMoneda(MonedaRequest solicitud) {

            decimal MontoDeTazaDeCambio = TazaDeCambioFinal(solicitud);
            if (solicitud.MonedaOrigen == "EUR" && solicitud.MonedaDestino == "USD")
            {
                return solicitud.Monto * MontoDeTazaDeCambio;
            } else if (solicitud.MonedaOrigen == "USD" && solicitud.MonedaDestino == "EUR")
            {
                return Math.Round(solicitud.Monto / MontoDeTazaDeCambio, 2);
            } else if (solicitud.MonedaOrigen == "JPY" && solicitud.MonedaDestino == "EUR") {
            
                return Math.Round(solicitud.Monto / MontoDeTazaDeCambio, 2);
            }else if (solicitud.MonedaOrigen == "EUR" && solicitud.MonedaDestino == "JPY")
            {
                return solicitud.Monto * MontoDeTazaDeCambio;
            }else if (solicitud.MonedaOrigen == "USD" && solicitud.MonedaDestino == "JPY")
            {
                return solicitud.Monto * MontoDeTazaDeCambio;
            }else if (solicitud.MonedaOrigen == "JPY" && solicitud.MonedaDestino == "USD")
            {
                return Math.Round(solicitud.Monto / MontoDeTazaDeCambio, 2);
            }
            else { return 0; }
        }

    }
}