using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using robot_controller_api.Models;

namespace robot_controller_api.Controllers;

[ApiController]
[Route("api/maps")]
public class MapsController : ControllerBase
{
    private readonly IMapDataAccess _mapRepo;

    public MapsController(IMapDataAccess mapRepo)
    {
        _mapRepo = mapRepo;
    }
    // private static readonly List<Map> _maps = new List<Map>
    // {
    //     new Map(1, 5, 5, "MOON", DateTime.Now, DateTime.Now, "Square moon map"),
    //     new Map(2, 10, 8, "MARS", DateTime.Now, DateTime.Now, "Rectangular map"),
    //     new Map(3, 3, 3, "EUROPA", DateTime.Now, DateTime.Now, "Small square map")
    // };

    [HttpGet]
    public IEnumerable<Map> GetAllMaps()
    {
        return _mapRepo.GetMaps();
    }

    [HttpGet("square")]
    public IEnumerable<Map> GetSquareMaps()
    {
        return _mapRepo.GetMaps()
            .Where(map => map.Rows == map.Columns);
    }

    [HttpGet("{id}", Name = "GetMap")]
    public IActionResult GetMapById(int id)
    {
        var map = _mapRepo.GetMapById(id);

        if (map == null)
        {
            return NotFound();
        }

        return Ok(map);
    }

    [HttpPost]
    public IActionResult AddMap(Map newMap)
    {
        if (newMap == null)
        {
            return BadRequest();
        }

        if (newMap.Rows <= 0 || newMap.Columns <= 0)
        {
            return BadRequest();
        }

        var existingMaps = _mapRepo.GetMaps();

        if (existingMaps.Any(m => m.Name.ToUpper() == newMap.Name.ToUpper()))
        {
            return Conflict();
        }

        var created = _mapRepo.CreateMap(newMap);

        return CreatedAtRoute("GetMap", new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMap(int id, Map updatedMap)
    {
        var existingMap = _mapRepo.GetMapById(id);

        if (existingMap == null)
        {
            return NotFound();
        }

        if (updatedMap == null)
        {
            return BadRequest();
        }

        if (updatedMap.Rows <= 0 || updatedMap.Columns <= 0)
        {
            return BadRequest();
        }

        var existingMaps = _mapRepo.GetMaps();

        if (existingMaps.Any(m => m.Id != id && m.Name.ToUpper() == updatedMap.Name.ToUpper()))
        {
            return Conflict();
        }

        updatedMap.Id = id;
        _mapRepo.UpdateMap(updatedMap);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMap(int id)
    {
        var map = _mapRepo.GetMapById(id);

        if (map == null)
        {
            return NotFound();
        }

        _mapRepo.DeleteMap(id);

        return NoContent();
    }

    [HttpGet("{id}/{x}-{y}")]
    public IActionResult CheckCoordinate(int id, int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return BadRequest();
        }

        var map = _mapRepo.GetMapById(id);

        if (map == null)
        {
            return NotFound();
        }

        bool isOnMap = x < map.Columns && y < map.Rows;

        return Ok(isOnMap);
    }
}