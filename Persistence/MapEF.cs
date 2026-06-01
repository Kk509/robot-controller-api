using robot_controller_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace robot_controller_api.Persistence
{
    public class MapEF : IMapDataAccess
    {
        private readonly RobotContext _context;

        public MapEF(RobotContext context)
        {
            _context = context;
        }

        public List<Map> GetMaps()
        {
            return _context.Maps.ToList();
        }

        public Map? GetMapById(int id)
        {
            return _context.Maps.Find(id);
        }

        public Map CreateMap(Map map)
        {
            _context.Maps.Add(map);
            _context.SaveChanges();
            return map;
        }

        public Map UpdateMap(Map map)
        {
            var existingMap = _context.Maps.Find(map.Id);

            if (existingMap == null)
            {
                return null;
            }

            existingMap.Name = map.Name;
            existingMap.Rows = map.Rows;
            existingMap.Columns = map.Columns;
            existingMap.Description = map.Description;
            existingMap.ModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return existingMap;
        }

        public void DeleteMap(int id)
        {
            var map = _context.Maps.Find(id);

            if (map != null)
            {
                _context.Maps.Remove(map);
                _context.SaveChanges();
            }
        }
    }
}