using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
	public class Configuration
	{
        [Key]
        public int ConfigId { get; set; }
		public string ConfigKey { get; set; }
		public string ConfigValue { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsEnabled { get; set; }
    }
}
