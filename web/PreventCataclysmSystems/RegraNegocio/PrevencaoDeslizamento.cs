using Newtonsoft.Json.Linq;
using PreventCataclysmSystems.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PreventCataclysmSystems.RegraNegocio
{
    public class PrevencaoDeslizamento
    {
        /*
        * Segundo Castro (2003) uma das principais causas de tais deslizamentos é a infiltração de água e a embebição do solo das encostas. Os deslizamentos estão
        * relacionados a três fatores: o tipo de solo, o declínio da encosta e a quantidade de água embebida no solo, que contribui para o aumento do peso da encosta.
        * Castro (2003), cita que a medida local de níveis de embebição do terreno pela água, permite antecipar os desastres iminentes.
        */

        /*
         * Variáveis necessárias:
         * Tipo de Solo
         * Declividade
         * Umidade
         * Intensidade da chuva
         * Analise: "Não ocorreu deslizamento", "Ocorreu no segundo X, umidade: Y "
         */

        /*
         * O tipo do solo será um parametro 
         */

        private const string API_KEY = "AIzaSyAjgi3Kcbe4brM3IMyzOQjwW6cMsH-vBAU";
        private const string BASE_URL = "https://fcm.googleapis.com/fcm/send";

        public async void HandleInput(Store value)
        {
            if (/*TODO: check values and send alert*/ true)
                await SendAsync(new Alert
                {
                    title = "Alerta!",
                    message = "O ambiente monitorado está prestes a colapsar!",
                    details = value.ToJson()
                });
        }

        private Task SendAsync(Alert alert)
        {
            var url = new Uri(BASE_URL);

            var jdata = JObject.FromObject(alert);

            var jfcmdata = new JObject();

            jfcmdata.Add("to", string.Format("/topics/{0}", "global")); //possibilidade de envio a vários canais
            jfcmdata.Add("data", jdata);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + API_KEY);

                    var content = new StringContent(jfcmdata.ToString(), Encoding.Default, "application/json");

                    Task.WaitAll(client.PostAsync(url, content).ContinueWith(async response =>
                    {
                        //TODO: handle success

                        await Task.FromResult(0);
                    }));

                    return Task.FromResult(0);
                }
            }
            catch (Exception faliure)
            {
                //TODO: handle faliure
                return Task.FromResult(faliure);
            }
        }
    }
}