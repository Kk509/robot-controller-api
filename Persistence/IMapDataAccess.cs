using robot_controller_api.Models;
namespace robot_controller_api.Persistence
{
    public interface IMapDataAccess
    {
        Map CreateMap(Map map);
        void DeleteMap(int id);
        Map? GetMapById(int id);
        List<Map> GetMaps();
        Map UpdateMap(Map map);
    }    
}

