using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CambioDeMonedaApiRest.Models
{
    public class MonedaResponse
    {

        public int Id { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal Monto { get; set; }
        public decimal TazaDeCambio { get; set;}

    }
}