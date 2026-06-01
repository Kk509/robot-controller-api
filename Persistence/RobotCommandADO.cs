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

            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand ORDER BY id", conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var robotCommand = new RobotCommand(
                    (int)dr["id"],
                    (string)dr["Name"],
                    (bool)dr["ismovecommand"],
                    (DateTime)dr["createddate"],
                    (DateTime)dr["modifieddate"],
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

            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new RobotCommand(
                    (int)dr["id"],
                    (string)dr["Name"],
                    (bool)dr["ismovecommand"],
                    (DateTime)dr["createddate"],
                    (DateTime)dr["modifieddate"],
                    dr["description"] as string
                );
            }

            return null;
        }

        public RobotCommand CreateRobotCommand(RobotCommand command)
        {
            // using var conn = new NpgsqlConnection(CONNECTION_STRING);
            // conn.Open();

            // using var cmd = new NpgsqlCommand(
            //     @"INSERT INTO robotcommand 
            //       (""Name"", description, ismovecommand, createddate, modifieddate)
            //       VALUES 
            //       (@name, @description, @ismovecommand, @createddate, @modifieddate)", conn);

            // cmd.Parameters.AddWithValue("name", command.Name);
            // cmd.Parameters.AddWithValue("description", (object?)command.Description ?? DBNull.Value);
            // cmd.Parameters.AddWithValue("ismovecommand", command.IsMoveCommand);
            // cmd.Parameters.AddWithValue("createddate", DateTime.Now);
            // cmd.Parameters.AddWithValue("modifieddate", DateTime.Now);

            // cmd.ExecuteNonQuery();
            throw new NotImplementedException();
        }

        public RobotCommand UpdateRobotCommand(RobotCommand command)
        {
            // using var conn = new NpgsqlConnection(CONNECTION_STRING);
            // conn.Open();

            // using var cmd = new NpgsqlCommand(
            //     @"UPDATE robotcommand
            //       SET ""Name"" = @name,
            //           description = @description,
            //           ismovecommand = @ismovecommand,
            //           modifieddate = @modifieddate
            //       WHERE id = @id", conn);

            // cmd.Parameters.AddWithValue("id");
            // cmd.Parameters.AddWithValue("name", command.Name);
            // cmd.Parameters.AddWithValue("description", (object?)command.Description ?? DBNull.Value);
            // cmd.Parameters.AddWithValue("ismovecommand", command.IsMoveCommand);
            // cmd.Parameters.AddWithValue("modifieddate", DateTime.Now);

            // cmd.ExecuteNonQuery();
            throw new NotImplementedException();
        }

        public void DeleteRobotCommand(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM robotcommand WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }    
}

