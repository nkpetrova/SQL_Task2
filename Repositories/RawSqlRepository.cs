using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using SQLTASK2.Models;

namespace SQLTASK2.Repositories
{
    public class RawSqlBookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public RawSqlBookingRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Строка подключения не может быть неопределеннная или пустая!");
            }

            _connectionString = connectionString;
        }

        public IReadOnlyList<Booking> GetAll()
        {
            var result = new List<Booking>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Date], [Turfirma_id], [Voucher_id], [Quantity], [Price] from [Booking]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Booking(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Date"]),
                    Convert.ToInt32(reader["Turfirma_id"]),
                    Convert.ToInt32(reader["Voucher_id"]),
                    Convert.ToInt32(reader["Quantity"]),
                    Convert.ToInt32(reader["Price"])
                ));
            }

            return result;
        }

        public IReadOnlyList<Tuple<Booking, int>> GroupByDate()
        {
            var result = new List<Tuple<Booking, int>>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();

            sqlCommand.CommandText = "select [Id], [Date], [Turfirma_id], [Voucher_id], [Quantity], [Price], SUM([Price]) AS [TotalPrice] from [Booking] GROUP BY [Id], [Date], [Turfirma_id], [Voucher_id], [Quantity], [Price] ";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Booking booking = new Booking(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Date"]),
                    Convert.ToInt32(reader["Turfirma_id"]),
                    Convert.ToInt32(reader["Voucher_id"]),
                    Convert.ToInt32(reader["Quantity"]),
                    Convert.ToInt32(reader["Price"])
                    );
                result.Add(new Tuple<Booking, int>(booking, Convert.ToInt32(reader["TotalPrice"])));
            }
            return result;
        }
    }

    public class RawSqlVoucherRepository : IVoucherRepository
    {
        private readonly string _connectionString;

        public RawSqlVoucherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Voucher GetByNameOfSanatorium(string sanatorium)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Sanatorium], [Address], [Price], [Quantity] from [Voucher] where " +
                "[Sanatorium] = @sanatorium";
            sqlCommand.Parameters.Add("@sanatorium", SqlDbType.NVarChar, 100).Value = sanatorium;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Voucher(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Sanatorium"]),
                    Convert.ToString(reader["Address"]),
                    Convert.ToInt32(reader["Price"]),
                    Convert.ToInt32(reader["Quantity"])
                    );
            }
            else
            {
                return null;
            }
        }
    }


    public class RawSqlTurfirmaRepository : ITurfirmaRepository
    {
        private readonly string _connectionString;

        public RawSqlTurfirmaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void UpdateTurFirma(TurFirma turFirma)
        {
            if (turFirma == null)
            {
                throw new ArgumentNullException(nameof(turFirma));
            }
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [TurFirma] set [Name] = @name, [Commission] = @commission, [Address] = " +
                "@address where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = turFirma.Id;
            sqlCommand.Parameters.Add("@commission", SqlDbType.Int).Value = turFirma.Commission;
            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = turFirma.Name;
            sqlCommand.Parameters.Add("@address", SqlDbType.NVarChar, 100).Value = turFirma.Address;
            sqlCommand.ExecuteNonQuery();
        }

        public TurFirma GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Address], [Commission] from [TurFirma] where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new TurFirma(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["Address"]),
                    Convert.ToInt32(reader["Commission"])
                    );
            }
            else
            {
                return null;
            }
        }

        public void DeleteTurFirma(TurFirma turfirma)
        {
            if (turfirma == null)
            {
                throw new ArgumentNullException(nameof(turfirma));
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [TurFirma] where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = turfirma.Id;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
