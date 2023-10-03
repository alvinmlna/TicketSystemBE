using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
	public class Status
	{
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }
        public int StatusGroupId { get; set; }
    }
}
