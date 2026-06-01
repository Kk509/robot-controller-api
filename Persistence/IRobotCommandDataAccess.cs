using robot_controller_api.Models;
namespace robot_controller_api.Persistence
{
    public interface IRobotCommandDataAccess
    {
        RobotCommand CreateRobotCommand(RobotCommand command);
        void DeleteRobotCommand(int id);
        RobotCommand? GetRobotCommandById(int id);
        List<RobotCommand> GetRobotCommands();
        RobotCommand UpdateRobotCommand(RobotCommand command);
    }    
}


