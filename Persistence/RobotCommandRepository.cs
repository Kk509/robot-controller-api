using Npgsql;
using robot_controller_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace robot_controller_api.Persistence
{
    public class RobotCommandRepository : IRobotCommandDataAccess, IRepository
    {
        private IRepository _repo => this;

        public List<RobotCommand> GetRobotCommands()
        {
            return _repo.ExecuteReader<RobotCommand>(
                "SELECT * FROM public.robotcommand"
            );
        }

        public RobotCommand? GetRobotCommandById(int id)
        {
            var param = new NpgsqlParameter[]
            {
                new("id", id)
            };

            return _repo.ExecuteReader<RobotCommand>(
                "SELECT * FROM public.robotcommand WHERE id = @id",
                param
            ).SingleOrDefault();
        }

        public RobotCommand CreateRobotCommand(RobotCommand newCommand)
        {
            var sqlParams = new NpgsqlParameter[]
            {
                new("name", newCommand.Name),
                new("description", (object?)newCommand.Description ?? DBNull.Value),
                new("ismovecommand", newCommand.IsMoveCommand)
            };

            return _repo.ExecuteReader<RobotCommand>(
                @"INSERT INTO robotcommand
                  (name, description, ismovecommand, createddate, modifieddate)
                  VALUES
                  (@name, @description, @ismovecommand, current_timestamp, current_timestamp)
                  RETURNING *;",
                sqlParams
            ).Single();
        }

        public RobotCommand UpdateRobotCommand(RobotCommand updatedCommand)
        {
            var sqlParams = new NpgsqlParameter[]
            {
                new("id", updatedCommand.Id),
                new("name", updatedCommand.Name),
                new("description", (object?)updatedCommand.Description ?? DBNull.Value),
                new("ismovecommand", updatedCommand.IsMoveCommand)
            };

            return _repo.ExecuteReader<RobotCommand>(
                @"UPDATE robotcommand
                  SET name=@name,
                      description=@description,
                      ismovecommand=@ismovecommand,
                      modifieddate=current_timestamp
                  WHERE id=@id
                  RETURNING *;",
                sqlParams
            ).Single();
        }

        public void DeleteRobotCommand(int id)
        {
            var param = new NpgsqlParameter[]
            {
                new("id", id)
            };

            _repo.ExecuteReader<RobotCommand>(
                "DELETE FROM robotcommand WHERE id = @id",
                param
            );
        }
    }
}