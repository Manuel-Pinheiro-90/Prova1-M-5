using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Prova1_M_5.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace Prova1_M_5.Service
{
    public class AnagraficaService : IAnagraficaService
    {
        private readonly SqlServerServiceBase _dbService;

        public AnagraficaService(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }
        
        private const string GET_ALL_AN = "SELECT * FROM Anagrafica";





        // ////////////////////////////////////////////////////////////// GetALL//////////////////////////////////////////////////////////////////////////////////////////////
        public IEnumerable<Anagrafica> GetAll()
        {
            var result = new List<Anagrafica>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_ALL_AN, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Anagrafica
                        {
                            IdAnagrafica = reader.GetInt32(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            Indirizzo = reader.GetString(3),
                            Città = reader.GetString(4),
                            CAP = reader.GetString(5),
                            Cod_Fisc = reader.GetString(6)
                        });
                    }
                }
            }

            return result;
        }
        // ///////////////////////////////////////////////////////////// BY ID ///////////////////////////////////////////////////////////////////////////////////

        private const string GET_BY_ID_AN = "SELECT * FROM Anagrafica WHERE IdAnagrafica = @IdAnagrafica";

        public Anagrafica GetByID(int id)
        {
            Anagrafica anagrafica = null;

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_BY_ID_AN, connection))
                {
                    command.Parameters.AddWithValue("@IdAnagrafica", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            anagrafica = new Anagrafica
                            {
                                IdAnagrafica = reader.GetInt32(0),
                                Cognome = reader.GetString(1),
                                Nome = reader.GetString(2),
                                Indirizzo = reader.GetString(3),
                                Città = reader.GetString(4),
                                CAP = reader.GetString(5),
                                Cod_Fisc = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return anagrafica;
        }






        // ///////////////////////////////////////////////////////// ADD //////////////////////////////////////////////////////////////////////////////////




        private const string CREATE_AN = "INSERT INTO Anagrafica(Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) " +
                                 "OUTPUT INSERTED.IdAnagrafica " +
                                 "VALUES(@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fisc)"; //Promemoria per il futuro RICONTROLLA SEMPRE DI SCRIVERE BENE !!!!!!!!

       

        public void Add(Anagrafica anagrafica)
        {
            using var conn = _dbService.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = _dbService.GetCommand(conn, CREATE_AN);
                cmd.Transaction = transaction;
                cmd.Parameters.Add(new SqlParameter("@Cognome", anagrafica.Cognome));
                cmd.Parameters.Add(new SqlParameter("@Nome", anagrafica.Nome));
                cmd.Parameters.Add(new SqlParameter("@Indirizzo", anagrafica.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("@Città", anagrafica.Città));
                cmd.Parameters.Add(new SqlParameter("@CAP", anagrafica.CAP));
                cmd.Parameters.Add(new SqlParameter("@Cod_Fisc", anagrafica.Cod_Fisc));
                anagrafica.IdAnagrafica = (int)cmd.ExecuteScalar();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        // ///////////////////////////////////////////////////////// //////////////////////////////////////////////////////////////////////////////////



       
      // ////////////////////////////////////////////////////////////// ///////////////////////////////////////////////////////////////////////////////////////////////


     
    }
}
