using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AgentiPolizia
{
    class DBManagerAgenti : IManagerAgenti
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProvaAgenti;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool Add(Agente item)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "insert into Agente (Nome, Cognome, CodiceFiscale, AreaGeografica, AnnoInizioAttivita) values(@nome, @cognome, @cf, @areaGeo, @annoInizio)";
                command.Parameters.AddWithValue("@nome", item.Nome);
                command.Parameters.AddWithValue("@cognome", item.Cognome);
                command.Parameters.AddWithValue("@cf", item.CodiceFiscale);
                command.Parameters.AddWithValue("@areaGeo", item.AreaGeografica);
                command.Parameters.AddWithValue("@annoInizio", item.AnnoInizioAttivita);

                int numRighe = command.ExecuteNonQuery();
                if (numRighe == 1)
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
                return false;
            }
        }

        public List<Agente> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from Agente";

                SqlDataReader reader = command.ExecuteReader();

                List<Agente> agenti = new List<Agente>();

                while (reader.Read())
                {
                    var cf = (string)reader["CodiceFiscale"];
                    var nome = (string)reader["Nome"];
                    var cognome = (string)reader["Cognome"];
                    var annoInizioAttivita = (int)reader["AnnoInizioAttivita"];
                    var areaGeo = (string)reader["AreaGeografica"];

                    Agente a = new Agente(nome, cognome, cf, areaGeo, annoInizioAttivita);
                    agenti.Add(a);
                }
                connection.Close();

                return agenti;
            }
        }

        public Agente GetByCF(string cf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from Agente where CodiceFiscale = @CodFisc";
                command.Parameters.AddWithValue("@CodFisc", cf);

                SqlDataReader reader = command.ExecuteReader();

                Agente a = null;

                while (reader.Read())
                {
                    //var cf = (string)reader["CodiceFiscale"];
                    var nome = (string)reader["Nome"];
                    var cognome = (string)reader["Cognome"];
                    var annoInizioAttivita = (int)reader["AnnoInizioAttivita"];
                    var areaGeo = (string)reader["AreaGeografica"];

                    a = new Agente(nome, cognome, cf, areaGeo, annoInizioAttivita);
                }
                connection.Close();
                return a;
            }
        }

        public List<string> GetAllAreeGeografiche()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select distinct AreaGeografica from Agente";

                SqlDataReader reader = command.ExecuteReader();

                var aree = new List<string>();

                while (reader.Read())
                {
                    var area = (string)reader["AreaGeografica"];
                    aree.Add(area);
                }
                connection.Close();
                return aree;
            }
        }

        public bool EsisteArea(string areaGeografica)
        {
            foreach (var item in GetAllAreeGeografiche())
            {
                if (item == areaGeografica)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Agente> GetByAnniDiServizio(int anniDiServizio)
        {
            int annoInizio= DateTime.Today.Year - anniDiServizio;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from Agente where AnnoInizioAttivita <=@annoInizio"; //
                command.Parameters.AddWithValue("@annoInizio", annoInizio);

                SqlDataReader reader = command.ExecuteReader();
                List<Agente> agenti = new List<Agente>();

                while (reader.Read())
                {
                    var cf = (string)reader["CodiceFiscale"];
                    var nome = (string)reader["Nome"];
                    var cognome = (string)reader["Cognome"];
                    var annoInizioAttivita = (int)reader["AnnoInizioAttivita"];
                    var areaGeo = (string)reader["AreaGeografica"];

                    Agente a = new Agente(nome, cognome, cf, areaGeo, annoInizioAttivita);
                    agenti.Add(a);
                }
                connection.Close();
                return agenti;
            }
        }

        public List<Agente> GetByAreaGeografica(string areaGeografica)
        {
            List<Agente> agenti = new List<Agente>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from Agente where AreaGeografica = @AreaGeografica";
                command.Parameters.AddWithValue("@AreaGeografica", areaGeografica);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nome = (string)reader["Nome"];
                    string cognome = (string)reader["Cognome"];
                    var codiceFiscale = (string)reader["CodiceFiscale"];
                    var annoInizioAttivita = (int)reader["AnnoInizioAttivita"];

                    Agente agente = new Agente(nome, cognome, codiceFiscale, areaGeografica, annoInizioAttivita);
                    agenti.Add(agente);
                }
                connection.Close();
            }
            return agenti;
        }
    }
}