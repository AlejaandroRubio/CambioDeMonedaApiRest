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
        private MonedaDelete monedaDelete;

        public MonedaController()
        {
            this.monedaPost = new MonedaPost();
            this.monedaRead = new MonedaRead();
            this.monedaDelete = new MonedaDelete();
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


        public HttpResponseMessage Post(MonedaRequest request)
        {
            this.monedaPost.SaveContact(request);

            var response = Request.CreateResponse<MonedaRequest>(System.Net.HttpStatusCode.Created, request);

            return response;
        }

        public HttpResponseMessage Delete(MonedaRequest request)
        {
            // Aquí deberías implementar la lógica para eliminar la solicitud de moneda con el ID proporcionado
            // Puedes utilizar el método DeleteContact de monedaPost o cualquier otro método que tengas implementado
            // en tu lógica de servicios para eliminar una solicitud de moneda.

            var monedaDelete = new MonedaDelete();
            monedaDelete.DeleteMoneda(request);

            var response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }
    }
}
