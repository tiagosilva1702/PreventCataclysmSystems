using System;
namespace PreventCataclysmSystems.Models
{
    public class Store
    {
        public int Temperatura { get; set; }

        public int Umidade { get; set; }

        public Aceleracao Acelerometro { get; set; }

        public int Solo { get; set; }

        public DateTime? Leitura { get; set; }
    }

    public class Aceleracao
    {
        public float x { get; set; }

        public float y { get; set; }

        public float z { get; set; }
    }
}