using Npgsql;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
    public interface IRepository
    {
        private const string CONNECTION_STRING =
            "Host=localhost;Username=postgres;Password=O0138aQzxV!#.;Database=sit331";

        public List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[]? dbParams = null)
            where T : class, new()
        {
            var entities = new List<T>();

            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand(sqlCommand, conn);

            if (dbParams != null)
            {
                cmd.Parameters.AddRange(
                    dbParams.Where(x => x.Value != null).ToArray()
                );
            }

            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var entity = new T();
                dr.MapTo(entity); 
                entities.Add(entity);
            }

            return entities;
        }
    }    
}

