using PreventCataclysmSystems.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PreventCataclysmSystems.Controllers
{
    public class StoreController : ApiController
    {
        // GET: api/Store
        public IEnumerable<Store> Get()
        {
            return from value in 
                       new DsAdmin.Store().Listar().Table.AsEnumerable()
                   select new Store
                   {
                       // TOOD: set data
                   };
        }

        // POST: api/Store
        public void Post(Store value)
        {
            //TODO: insert values in database
            new DsAdmin.Store().Incluir(value.Acelerometro, value.Solo, value.Temperatura, value.Umidade);
        }
    }
}
