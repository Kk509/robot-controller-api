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

            using var cmd = new NpgsqlCommand("SELECT * FROM robot_map ORDER BY id", conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var map = new Map(
                    (int)dr["id"],
                    (int)dr["columns"],
                    (int)dr["rows"],
                    (string)dr["name"],
                    (DateTime)dr["created_date"],
                    (DateTime)dr["modified_date"],
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

            using var cmd = new NpgsqlCommand("SELECT * FROM robot_map WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new Map(
                    (int)dr["id"],
                    (int)dr["columns"],
                    (int)dr["rows"],
                    (string)dr["name"],
                    (DateTime)dr["created_date"],
                    (DateTime)dr["modified_date"],
                    dr["description"] as string
                );
            }

            return null;
        }

        public Map CreateMap(Map map)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"INSERT INTO robot_map
                  (name, rows, columns, description, created_date, modified_date)
                  VALUES
                  (@name, @rows, @columns, @description, @created_date, @modified_date)", conn);

            cmd.Parameters.AddWithValue("name", map.Name);
            cmd.Parameters.AddWithValue("rows", map.Rows);
            cmd.Parameters.AddWithValue("columns", map.Columns);
            cmd.Parameters.AddWithValue("description", (object?)map.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("created_date", DateTime.Now);
            cmd.Parameters.AddWithValue("modified_date", DateTime.Now);

            cmd.ExecuteNonQuery();
            // throw new NotImplementedException();
            return map;
        }

        public Map UpdateMap(Map map)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"UPDATE robot_map
                  SET name = @name,
                      rows = @rows,
                      columns = @columns,
                      description = @description,
                      modified_date = @modified_date
                  WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("id", map.Id);
            cmd.Parameters.AddWithValue("name", map.Name);
            cmd.Parameters.AddWithValue("rows", map.Rows);
            cmd.Parameters.AddWithValue("columns", map.Columns);
            cmd.Parameters.AddWithValue("description", (object?)map.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("modified_date", DateTime.Now);

            cmd.ExecuteNonQuery();
            // throw new NotImplementedException();
            return map;
        }

        public void DeleteMap(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM robot_map WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }    
}
