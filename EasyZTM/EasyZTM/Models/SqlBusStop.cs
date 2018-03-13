using SQLite;

namespace EasyZTM.Models
{
    [Table("BusStop")]
    public class SqlBusStop
    {
        [PrimaryKey, Unique]
        public int StopId { get; set; }

        public string Description { get; set; }

        public bool isFavourite { get; set; } = false;
    }
}
