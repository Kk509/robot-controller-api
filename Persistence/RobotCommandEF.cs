using robot_controller_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace robot_controller_api.Persistence
{
    public class RobotCommandEF : IRobotCommandDataAccess
    {
        private readonly RobotContext _context;

        public RobotCommandEF(RobotContext context)
        {
            _context = context;
        }

        public List<RobotCommand> GetRobotCommands()
        {
            return _context.RobotCommands.ToList();
        }

        public RobotCommand? GetRobotCommandById(int id)
        {
            return _context.RobotCommands.Find(id);
        }

        public RobotCommand CreateRobotCommand(RobotCommand command)
        {
            _context.RobotCommands.Add(command);
            _context.SaveChanges();
            return command;
        }

        public RobotCommand UpdateRobotCommand(RobotCommand command)
        {
            var existingCommand = _context.RobotCommands.Find(command.Id);

            if (existingCommand == null)
            {
                return null;
            }

            existingCommand.Name = command.Name;
            existingCommand.Description = command.Description;
            existingCommand.IsMoveCommand = command.IsMoveCommand;
            existingCommand.ModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return existingCommand;
        }

        public void DeleteRobotCommand(int id)
        {
            var command = _context.RobotCommands.Find(id);

            if (command != null)
            {
                _context.RobotCommands.Remove(command);
                _context.SaveChanges();
            }
        }
    }
}