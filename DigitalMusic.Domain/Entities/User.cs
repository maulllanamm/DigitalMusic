using DigitalMusic.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusic.Domain.Entities
{
    public class User : BaseGuidEntity
    {
        public string username { get; set; }
        public string password_salt { get; set; }
        public string password_hash { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public int role_id { get; set; }

        [ForeignKey("role_id")]
        public Role role { get; set; }
        public string? refresh_token { get; set; }
        public DateTimeOffset? refresh_token_created { get; set; }
        public DateTimeOffset? refresh_token_expires { get; set; }
        public string? verify_token { get; set; }
        public DateTimeOffset? verify_date { get; set; }
        public string? password_reset_token { get; set; }
        public DateTimeOffset? password_reset_expires { get; set; }
    }
}
