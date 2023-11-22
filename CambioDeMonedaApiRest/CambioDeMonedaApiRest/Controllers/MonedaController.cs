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

        public MonedaRequest[] Get()
        {
            return monedaRead.GetAllContacts();
        }

        public HttpResponseMessage Post(MonedaRequest contact)
        {
            this.monedaPost.SaveContact(contact);

            var response = Request.CreateResponse<MonedaRequest>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }

    }
}
