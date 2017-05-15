using Newtonsoft.Json;
using PreventCataclysmSystems.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace PreventCataclysmSystems.Controllers
{
    public class StoreController : ApiController
    {
        public IEnumerable<Store> Get()
        {
            return from value in
                       new DsAdmin.Store().Listar().Table.AsEnumerable()
                   select new Store
                   {
                       Acelerometro = JsonConvert.DeserializeObject<Aceleracao>(value["Acelerometro"].ToString()),
                       Solo = value["Solo"].ToInt32(),
                       Temperatura = value["Temperatura"].ToInt32(),
                       Umidade = value["Umidade"].ToInt32(),
                       Leitura = value["Leitura"].ToDateTime()
                   };
        }

        public void Post(Store value)
        {
            new DsAdmin.Store().Incluir(JsonConvert.SerializeObject(value.Acelerometro), value.Solo, value.Temperatura, value.Umidade);
        }
    }
}
