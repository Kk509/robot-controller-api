using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using robot_controller_api.Models;
namespace robot_controller_api.Controllers;

// mark it as API controller
[ApiController]
// set the base uniform resource identifier for the controller
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    // private field to hold existing commands when the application starts.
    // private static readonly List<RobotCommand> _commands = new List<RobotCommand>
    // {
    //     new RobotCommand(1, "LEFT", true, DateTime.Now, DateTime.Now, "Turn left"),
    //     new RobotCommand(2, "RIGHT", true, DateTime.Now, DateTime.Now, "Turn right"),
    //     new RobotCommand(3, "MOVE", true, DateTime.Now, DateTime.Now, "Move forward"),
    //     new RobotCommand(4, "PLACE", false, DateTime.Now, DateTime.Now, "Place robot on map"),
    //     new RobotCommand(5, "REPORT", false, DateTime.Now, DateTime.Now, "Report robot status")
    // };

    private readonly IRobotCommandDataAccess _robotCommandsRepo;

    public RobotCommandsController(IRobotCommandDataAccess robotCommandsRepo)
    {
        _robotCommandsRepo = robotCommandsRepo;
    }

    // implement the first endpoint from the list of existing endpoints.
    [HttpGet]
    public IEnumerable<RobotCommand> GetAllRobotCommands()
    {
            return _robotCommandsRepo.GetRobotCommands();
    }

    [HttpGet("move")]
    public IEnumerable<RobotCommand> GetMoveCommandsOnly()
    {
        // return a filtered _commands field here after you filter out
        return _robotCommandsRepo.GetRobotCommands()
            .Where(command => command.IsMoveCommand);
    }

    [HttpGet("{id}", Name = "GetRobotCommand")]
    public IActionResult GetRobotCommandById(int id)
    {
        var command = _robotCommandsRepo.GetRobotCommandById(id);

        if (command == null)
        {
            return NotFound();
        }

        return Ok(command);
    }

    [HttpPost]
    public IActionResult AddRobotCommand(RobotCommand newCommand)
    {   
        if (newCommand == null)
        {
            return BadRequest();
        }

        var existingCommands = _robotCommandsRepo.GetRobotCommands();

        if (existingCommands.Any(command => command.Name.ToUpper() == newCommand.Name.ToUpper()))
        {
            return Conflict();
        }

        var created = _robotCommandsRepo.CreateRobotCommand(newCommand);

        return CreatedAtRoute("GetRobotCommand", new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRobotCommand(int id, RobotCommand updatedCommand)
    {        
        // if modification fails, return BadRequest()
        if (updatedCommand == null)
        {
            return BadRequest();
        }

        // find a command by id, return NotFound() if it doesn't exist
        var existingCommand = _robotCommandsRepo.GetRobotCommandById(id);

        // try to modify an existing command with fields from updatedCommand
        if (existingCommand == null)
        {
            return NotFound();
        }

        var existingCommands = _robotCommandsRepo.GetRobotCommands();

        if (existingCommands.Any(command =>
                command.Id != id &&
                command.Name.ToUpper() == updatedCommand.Name.ToUpper()))
        {
            return Conflict();
        }

        updatedCommand.Id = id;
        _robotCommandsRepo.UpdateRobotCommand(updatedCommand);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRobotCommand(int id)
    {
        // find a command by id, return NotFound() if it doesn't exist
        var existingCommand = _robotCommandsRepo.GetRobotCommandById(id);

        if (existingCommand == null)
        {
            return NotFound();
        }

        _robotCommandsRepo.DeleteRobotCommand(id);

        // remove the command from _commands
        return NoContent();
    }
}