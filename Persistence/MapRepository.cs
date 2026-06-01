using Npgsql;
using robot_controller_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace robot_controller_api.Persistence
{
    public class MapRepository : IMapDataAccess, IRepository
    {
        private IRepository _repo => this;

        // GET all maps
        public List<Map> GetMaps()
        {
            return _repo.ExecuteReader<Map>(
                "SELECT * FROM public.map ORDER BY id"
            );
        }

        // GET map by id
        public Map? GetMapById(int id)
        {
            var param = new NpgsqlParameter[]
            {
                new("id", id)
            };

            return _repo.ExecuteReader<Map>(
                "SELECT * FROM public.map WHERE id = @id",
                param
            ).SingleOrDefault();
        }

        // CREATE map
        public Map CreateMap(Map map)
        {
            var sqlParams = new NpgsqlParameter[]
            {
                new("name", map.Name),
                new("columns", map.Columns),
                new("rows", map.Rows),
                new("description", (object?)map.Description ?? DBNull.Value)
            };

            return _repo.ExecuteReader<Map>(
                @"INSERT INTO map
                  (name, columns, rows, description, createddate, modifieddate)
                  VALUES
                  (@name, @columns, @rows, @description, current_timestamp, current_timestamp)
                  RETURNING *;",
                sqlParams
            ).Single();
        }

        // UPDATE map
        public Map UpdateMap(Map map)
        {
            var sqlParams = new NpgsqlParameter[]
            {
                new("id", map.Id),
                new("name", map.Name),
                new("columns", map.Columns),
                new("rows", map.Rows),
                new("description", (object?)map.Description ?? DBNull.Value)
            };

            return _repo.ExecuteReader<Map>(
                @"UPDATE map
                  SET name = @name,
                      columns = @columns,
                      rows = @rows,
                      description = @description,
                      modifieddate = current_timestamp
                  WHERE id = @id
                  RETURNING *;",
                sqlParams
            ).Single();
        }

        // DELETE map
        public void DeleteMap(int id)
        {
            var param = new NpgsqlParameter[]
            {
                new("id", id)
            };

            _repo.ExecuteReader<Map>(
                "DELETE FROM map WHERE id = @id",
                param
            );
        }
    }
}