using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Store
    {
        private DAO neo;

        private bool sessaoInterna = false;

        public Store()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Store(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(int Acelerometro, int Solo, int Temperatura, int Umidade)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sai_IncluirStore");
            _Command.Parameters.AddWithValue("@Acelerometro", Acelerometro == 0 ? Convert.DBNull : Acelerometro);
            _Command.Parameters.AddWithValue("@Solo", Solo == 0 ? Convert.DBNull : Solo);
            _Command.Parameters.AddWithValue("@Temperatura", Temperatura == 0 ? Convert.DBNull : Temperatura);
            _Command.Parameters.AddWithValue("@Umidade", Umidade == 0 ? Convert.DBNull : Umidade);

            neo.ExecutaNonQuery(_Command);

            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public DataView Listar()
        {
            //TODO: read all store
            throw new NotImplementedException();
        }
    }
}