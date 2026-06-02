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
                "SELECT * FROM public.robot_command"
            );
        }

        public RobotCommand? GetRobotCommandById(int id)
        {
            var param = new NpgsqlParameter[]
            {
                new("id", id)
            };

            return _repo.ExecuteReader<RobotCommand>(
                "SELECT * FROM public.robot_command WHERE id = @id",
                param
            ).SingleOrDefault();
        }

        public RobotCommand CreateRobotCommand(RobotCommand newCommand)
        {
            var sqlParams = new NpgsqlParameter[]
            {
                new("name", newCommand.Name),
                new("description", (object?)newCommand.Description ?? DBNull.Value),
                new("is_move_command", newCommand.IsMoveCommand)
            };

            return _repo.ExecuteReader<RobotCommand>(
                @"INSERT INTO robot_command
                  (""Name"", description, is_move_command, created_date, modified_date)
                  VALUES
                  (@name, @description, @is_move_command, current_timestamp, current_timestamp)
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
                new("is_move_command", updatedCommand.IsMoveCommand)
            };

            return _repo.ExecuteReader<RobotCommand>(
                @"UPDATE robot_command
                  SET ""Name""=@name,
                      description=@description,
                      is_move_command=@is_move_command,
                      modified_date=current_timestamp
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
                "DELETE FROM robot_command WHERE id = @id",
                param
            );
        }
    }
}