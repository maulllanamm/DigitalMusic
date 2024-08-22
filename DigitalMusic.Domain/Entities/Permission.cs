using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalMusic.Domain.Entities
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string http_method { get; set; }
        public string path { get; set; }
        [JsonIgnore]
        public List<RolePermission> role_permissions { get; set; }
    }
}
