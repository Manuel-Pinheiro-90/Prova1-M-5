using Prova1_M_5.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Prova1_M_5.Service
{
    public class TipoViolazioneService : ITipoViolazioneService
    {
        private readonly SqlServerServiceBase _dbService;

        public TipoViolazioneService(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private const string GET_ALL_VI = "SELECT * FROM TIPO_VIOLAZIONE";

        public IEnumerable<TipoViolazione> GetAll()
        {
            var result = new List<TipoViolazione>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_ALL_VI, connection))
                using (SqlDataReader reader =  command.ExecuteReader())
                {
                    while ( reader.Read())
                    {
                        result.Add(new TipoViolazione
                        {
                            IdViolazione = reader.GetInt32(0),
                            Descrizione = reader.GetString(1)
                        });
                    }
                }
            }

            return result;
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        public TipoViolazione GetById(int id)
        {
            TipoViolazione tipoViolazione = null;

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                 connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM TIPO_VIOLAZIONE WHERE IdViolazione = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader =  command.ExecuteReader())
                    {
                        if ( reader.Read())
                        {
                            tipoViolazione = new TipoViolazione
                            {
                                IdViolazione = reader.GetInt32(0),
                                Descrizione = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return tipoViolazione;
        }
        
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        private const string CREATE_VI = @"INSERT INTO dbo.TIPO_VIOLAZIONE (Descrizione)
                               VALUES (@Descrizione);
                               SELECT SCOPE_IDENTITY();";

        public void Add(TipoViolazione tipoViolazione)
        {
            using var conn = _dbService.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = _dbService.GetCommand(conn, CREATE_VI);
                cmd.Transaction = transaction; 
                cmd.Parameters.Add(new SqlParameter("@Descrizione", tipoViolazione.Descrizione));
                tipoViolazione.IdViolazione = Convert.ToInt32(cmd.ExecuteScalar());
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }





    }










}

