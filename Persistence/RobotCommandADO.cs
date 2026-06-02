using Npgsql;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
    public class RobotCommandADO : IRobotCommandDataAccess
    {
        private const string CONNECTION_STRING =
            "Host=localhost;Username=postgres;Password=O0138aQzxV!#.;Database=sit331";

        public List<RobotCommand> GetRobotCommands()
        {
            var robotCommands = new List<RobotCommand>();

            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM robot_command ORDER BY id", conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var robotCommand = new RobotCommand(
                    (int)dr["id"],
                    (string)dr["Name"],
                    (bool)dr["is_move_command"],
                    (DateTime)dr["created_date"],
                    (DateTime)dr["modified_date"],
                    dr["description"] as string
                );

                robotCommands.Add(robotCommand);
            }

            return robotCommands;
        }

        public RobotCommand? GetRobotCommandById(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM robot_command WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new RobotCommand(
                    (int)dr["id"],
                    (string)dr["Name"],
                    (bool)dr["is_move_command"],
                    (DateTime)dr["created_date"],
                    (DateTime)dr["modified_date"],
                    dr["description"] as string
                );
            }

            return null;
        }

        public RobotCommand CreateRobotCommand(RobotCommand command)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"INSERT INTO robot_command 
                  (""Name"", description, is_move_command, created_date, modified_date)
                  VALUES 
                  (@name, @description, @is_move_command, @created_date, @modified_date)", conn);

            cmd.Parameters.AddWithValue("name", command.Name);
            cmd.Parameters.AddWithValue("description", (object?)command.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_move_command", command.IsMoveCommand);
            cmd.Parameters.AddWithValue("created_date", DateTime.Now);
            cmd.Parameters.AddWithValue("modified_date", DateTime.Now);

            cmd.ExecuteNonQuery();
            // throw new NotImplementedException();
            return command;
        }

        public RobotCommand UpdateRobotCommand(RobotCommand command)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"UPDATE robot_command
                  SET ""Name"" = @name,
                      description = @description,
                      is_move_command = @is_move_command,
                      modified_date = @modified_date
                  WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("id", command.Id);
            cmd.Parameters.AddWithValue("name", command.Name);
            cmd.Parameters.AddWithValue("description", (object?)command.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("is_move_command", command.IsMoveCommand);
            cmd.Parameters.AddWithValue("modified_date", DateTime.Now);

            cmd.ExecuteNonQuery();
            // throw new NotImplementedException();
            return command;
        }

        public void DeleteRobotCommand(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM robot_command WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }    
}

