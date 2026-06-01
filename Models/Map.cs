using System;
using System.Collections.Generic;

namespace robot_controller_api.Models
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? Issquare { get; set; }

        public Map()
        {
        }

        public Map(
            int id,
            int columns,
            int rows,
            string name,
            DateTime createdDate,
            DateTime modifiedDate,
            string? description)
        {
            Id = id;
            Columns = columns;
            Rows = rows;
            Name = name;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Description = description;
        }
    }
}
