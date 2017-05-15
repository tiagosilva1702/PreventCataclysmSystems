﻿using PreventCataclysmSystems.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PreventCataclysmSystems.Controllers
{
    public class HomeController : Controller
    {
        static readonly string URI = "https://homologacao.imap.org.br/prevent/api/store";

        public async Task<ActionResult> Index(/* TODO: search params */)
        {
            var src = await GetStoreAsync(URI);

            //TODO: group src for display

            ViewBag.Display = from value in
                                  src.GroupBy(element => element.Leitura.Value.Minute)
                              select new Store
                              {
                                  Leitura = value.Average(store => store.Leitura),
                                  Solo = value.Average(store => store.Solo).ToInt32(),
                                  Temperatura = value.Average(store => store.Temperatura).ToInt32(),
                                  Umidade = value.Average(store => store.Umidade).ToInt32()
                              };

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static async Task<IEnumerable<Store>> GetStoreAsync(string path)
        {
            var response = await new HttpClient().GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Store>>();
            }

            return null;
        }
    }
}