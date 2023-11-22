using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CambioDeMonedaApiRest.Models;
using CambioDeMonedaApiRest.Services;

namespace MonedaDeCambioApiRest.Controllers
{

    public class MonedaController : ApiController
    {

        private MonedaPost monedaPost;
        private MonedaRead monedaRead;

        public MonedaController()
        {
            this.monedaPost = new MonedaPost();
            this.monedaRead = new MonedaRead();
        }

        public MonedaResponse[] Get()
        {
            // Obtener todas las solicitudes de moneda
            var solicitudes = monedaRead.GetAllContacts();

            // Crear una lista para almacenar las respuestas convertidas
            var respuestasConvertidas = new List<MonedaResponse>();

            // Iterar sobre cada solicitud y realizar la conversión
            foreach (var solicitud in solicitudes)
            {
                // Convertir la solicitud utilizando el método ConvertirMoneda
                var respuestaConvertida = monedaRead.ConvertirMoneda(new MonedaRequest
                {
                    Id = solicitud.Id,
                    MonedaOrigen = solicitud.MonedaOrigen,
                    MonedaDestino = solicitud.MonedaDestino,
                    Monto = solicitud.Monto // Puedes utilizar el monto original o el monto convertido según tus necesidades
                });

                // Agregar la respuesta convertida a la lista
                respuestasConvertidas.Add(respuestaConvertida);
            }

            // Devolver la lista de respuestas convertidas
            return respuestasConvertidas.ToArray();
        }


        public HttpResponseMessage Post(MonedaRequest contact)
        {
            this.monedaPost.SaveContact(contact);

            var response = Request.CreateResponse<MonedaRequest>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }

    }
}
