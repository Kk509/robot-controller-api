using Npgsql;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
    public class MapADO : IMapDataAccess
    {
        private const string CONNECTION_STRING =
            "Host=localhost;Username=postgres;Password=O0138aQzxV!#.;Database=sit331";

        public List<Map> GetMaps()
        {
            var maps = new List<Map>();

            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM map ORDER BY id", conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var map = new Map(
                    (int)dr["id"],
                    (int)dr["columns"],
                    (int)dr["rows"],
                    (string)dr["Name"],
                    (DateTime)dr["createddate"],
                    (DateTime)dr["modifieddate"],
                    dr["description"] as string
                );

                maps.Add(map);
            }

            return maps;
        }

        public Map? GetMapById(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM map WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Map(
                    (int)dr["id"],
                    (int)dr["columns"],
                    (int)dr["rows"],
                    (string)dr["Name"],
                    (DateTime)dr["createddate"],
                    (DateTime)dr["modifieddate"],
                    dr["description"] as string
                );
            }

            return null;
        }

        public Map CreateMap(Map map)
        {
            // using var conn = new NpgsqlConnection(CONNECTION_STRING);
            // conn.Open();

            // using var cmd = new NpgsqlCommand(
            //     @"INSERT INTO map
            //       (""Name"", rows, columns, description, createddate, modifieddate)
            //       VALUES
            //       (@name, @rows, @columns, @description, @createddate, @modifieddate)", conn);

            // cmd.Parameters.AddWithValue("name", map.Name);
            // cmd.Parameters.AddWithValue("rows", map.Rows);
            // cmd.Parameters.AddWithValue("columns", map.Columns);
            // cmd.Parameters.AddWithValue("description", (object?)map.Description ?? DBNull.Value);
            // cmd.Parameters.AddWithValue("createddate", DateTime.Now);
            // cmd.Parameters.AddWithValue("modifieddate", DateTime.Now);

            // cmd.ExecuteNonQuery();
            throw new NotImplementedException();
        }

        public Map UpdateMap(Map map)
        {
            // using var conn = new NpgsqlConnection(CONNECTION_STRING);
            // conn.Open();

            // using var cmd = new NpgsqlCommand(
            //     @"UPDATE map
            //       SET ""Name"" = @name,
            //           rows = @rows,
            //           columns = @columns,
            //           description = @description,
            //           modifieddate = @modifieddate
            //       WHERE id = @id", conn);

            // cmd.Parameters.AddWithValue("id");
            // cmd.Parameters.AddWithValue("name", map.Name);
            // cmd.Parameters.AddWithValue("rows", map.Rows);
            // cmd.Parameters.AddWithValue("columns", map.Columns);
            // cmd.Parameters.AddWithValue("description", (object?)map.Description ?? DBNull.Value);
            // cmd.Parameters.AddWithValue("modifieddate", DateTime.Now);

            // cmd.ExecuteNonQuery();
            throw new NotImplementedException();
        }

        public void DeleteMap(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM map WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }    
}
