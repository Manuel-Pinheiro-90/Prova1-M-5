using Prova1_M_5.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Prova1_M_5.Service
{
    public class VerbaleService : IVerbaleService
    {
        private readonly SqlServerServiceBase _dbService;

        public VerbaleService(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }
       
        /// ///////////////////////////////////////////////////////////GETALL con aggiunta di dati anagrafici e violazione/////////////////////////////////////////////////////////////////////////////
       

        private const string GET_ALL_VE = @"
    SELECT v.IdVerbale, v.DataViolazione, v.IndirizzoViolazione, v.Nominativo_Agente, v.DataTrascrizioneVerbale, v.Importo, v.DecurtamentoPunti,
           a.IdAnagrafica, a.Cognome, a.Nome, a.Indirizzo, a.Città, a.CAP, a.Cod_Fisc,
           t.IdViolazione, t.Descrizione 
    FROM Verbale v
    JOIN Anagrafica a ON v.IdAnagrafica = a.IdAnagrafica
    JOIN TIPO_VIOLAZIONE t ON v.IdViolazione = t.IdViolazione";

        public IEnumerable<VerbaleViewModel> GetAll()
        {
            var result = new List<VerbaleViewModel>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_ALL_VE, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var verbale = new VerbaleViewModel
                        {
                            IdVerbale = reader.GetInt32(0),
                            DataViolazione = reader.GetDateTime(1),
                            IndirizzoViolazione = reader.GetString(2),
                            Nominativo_Agente = reader.GetString(3),
                            DataTrascrizioneVerbale = reader.GetDateTime(4),
                            Importo = reader.GetDecimal(5),
                            DecurtamentoPunti = reader.GetInt32(6),

                            IdAnagrafica = reader.GetInt32(7),
                            Cognome = reader.GetString(8),
                            Nome = reader.GetString(9),
                            Indirizzo = reader.GetString(10),
                            Città = reader.GetString(11),
                            CAP = reader.GetString(12),
                            Cod_Fisc = reader.GetString(13),

                            IdViolazione = reader.GetInt32(14),
                            Descrizione = reader.GetString(15)
                        };
                        result.Add(verbale);
                    }
                }
            }

            return result;
        }



        /// ////////////////////////////////////////////////////////////////GET BY ID//////////////////////////////////////////////////////////////////////////////

        private const string GET_BY_ID_VE = "SELECT * FROM VERBALE WHERE IdVerbale = @Id";


        public Verbale GetById(int id)
        {
            Verbale verbale = null;

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_BY_ID_VE, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            verbale = new Verbale
                            {
                                IdVerbale = reader.GetInt32(0),
                                DataViolazione = reader.GetDateTime(1),
                                IndirizzoViolazione = reader.GetString(2),
                                Nominativo_Agente = reader.GetString(3),
                                DataTrascrizioneVerbale = reader.GetDateTime(4),
                                Importo = reader.GetDecimal(5),
                                DecurtamentoPunti = reader.GetInt32(6),
                                IdAnagrafica = reader.GetInt32(7),
                                IdViolazione = reader.GetInt32(8)
                            };
                        }
                    }
                }
            }

            return verbale;
        }


        // /////////////////////////////////////////////// ADD //////////////////////////////////////////////////////////



        private const string CREATE_VERBALE = @"
   INSERT INTO Verbale(DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafica, IdViolazione) 
    OUTPUT INSERTED.IdVerbale 
    VALUES(@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IdAnagrafica, @IdViolazione)";

        public void Add(Verbale verbale)
        {
            using var conn = _dbService.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmd = _dbService.GetCommand(conn, CREATE_VERBALE);
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@DataViolazione", verbale.DataViolazione));
                cmd.Parameters.Add(new SqlParameter("@IndirizzoViolazione", verbale.IndirizzoViolazione));
                cmd.Parameters.Add(new SqlParameter("@Nominativo_Agente", verbale.Nominativo_Agente));
                cmd.Parameters.Add(new SqlParameter("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale));
                cmd.Parameters.Add(new SqlParameter("@Importo", verbale.Importo));
                cmd.Parameters.Add(new SqlParameter("@DecurtamentoPunti", verbale.DecurtamentoPunti));
                cmd.Parameters.Add(new SqlParameter("@IdAnagrafica", verbale.IdAnagrafica));
                cmd.Parameters.Add(new SqlParameter("@IdViolazione", verbale.IdViolazione));

                verbale.IdVerbale = (int)cmd.ExecuteScalar();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        // /////////////////////////////////////////////// QUERY GENERALI //////////////////////////////////////////////////////////

        private const string GET_VERBALI_PER_TRASGRESSORE = @"
    SELECT a.Cognome, a.Nome, COUNT(v.IdVerbale) AS TotaleVerbali
    FROM Verbale v
    JOIN Anagrafica a ON v.IdAnagrafica = a.IdAnagrafica
    GROUP BY a.Cognome, a.Nome";

        private const string GET_PUNTI_DECURTATI_PER_TRASGRESSORE = @"
    SELECT a.Cognome, a.Nome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati
    FROM Verbale v
    JOIN Anagrafica a ON v.IdAnagrafica = a.IdAnagrafica
    GROUP BY a.Cognome, a.Nome";

        private const string GET_VIOLAZIONI_SUPERANO_10_PUNTI = @"
    SELECT v.Importo, a.Cognome, a.Nome, v.DataViolazione, v.DecurtamentoPunti
    FROM Verbale v
    JOIN Anagrafica a ON v.IdAnagrafica = a.IdAnagrafica
    WHERE v.DecurtamentoPunti > 10";

        private const string GET_VIOLAZIONI_IMPORTO_MAGGIORE_400 = @"
    SELECT v.Importo, a.Cognome, a.Nome, v.DataViolazione, v.DecurtamentoPunti
    FROM Verbale v
    JOIN Anagrafica a ON v.IdAnagrafica = a.IdAnagrafica
    WHERE v.Importo > 400";


         /////////////////////////////////////////////////////////GetVerbaliPerTrasgressor//////////////////////////////////////////////////////////////////

     public IEnumerable<VerbaliPerTrasgressoreViewModel> GetVerbaliPerTrasgressore()
        {
            var result = new List<VerbaliPerTrasgressoreViewModel>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_VERBALI_PER_TRASGRESSORE, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new VerbaliPerTrasgressoreViewModel
                        {
                            Cognome = reader.GetString(0),
                            Nome = reader.GetString(1),
                            TotaleVerbali = reader.GetInt32(2)
                        });
                    }
                }
            }

            return result;
        }

        // /////////////////////////////////////////////////////////GetPuntiDecurtatiPerTrasgressore//////////////////////////////////////////////////////////////////
        public IEnumerable<PuntiDecurtatiPerTrasgressoreViewModel> GetPuntiDecurtatiPerTrasgressore()
        {
            var result = new List<PuntiDecurtatiPerTrasgressoreViewModel>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_PUNTI_DECURTATI_PER_TRASGRESSORE, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new PuntiDecurtatiPerTrasgressoreViewModel
                        {
                            Cognome = reader.GetString(0),
                            Nome = reader.GetString(1),
                            TotalePuntiDecurtati = reader.GetInt32(2)
                        });
                    }
                }
            }

            return result;
        }
        /// ////////////////////////////////////////// GetViolazioniSuperano10Punti/////////////////////////////////////////////////////////////////////////

        public IEnumerable<ViolazioniSuperano10PuntiViewModel> GetViolazioniSuperano10Punti()
        {
            var result = new List<ViolazioniSuperano10PuntiViewModel>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_VIOLAZIONI_SUPERANO_10_PUNTI, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ViolazioniSuperano10PuntiViewModel
                        {
                            Importo = reader.GetDecimal(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            DataViolazione = reader.GetDateTime(3),
                            DecurtamentoPunti = reader.GetInt32(4)
                        });
                    }
                }
            }

            return result;
        }


        /// //////////////////////////////////////////////GetViolazioniImportoMaggiore400/////////////////////////////////////////////////////////////////////

        public IEnumerable<ViolazioniImportoMaggiore400ViewModel> GetViolazioniImportoMaggiore400()
        {
            var result = new List<ViolazioniImportoMaggiore400ViewModel>();

            using (SqlConnection connection = new SqlConnection(_dbService.GetConnection().ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GET_VIOLAZIONI_IMPORTO_MAGGIORE_400, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ViolazioniImportoMaggiore400ViewModel
                        {
                            Importo = reader.GetDecimal(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            DataViolazione = reader.GetDateTime(3),
                            DecurtamentoPunti = reader.GetInt32(4)
                        });
                    }
                }
            }

            return result;
        }
    }
}


















    


    





































