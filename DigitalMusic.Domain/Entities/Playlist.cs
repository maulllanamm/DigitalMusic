using System.ComponentModel.DataAnnotations.Schema;
using DigitalMusic.Domain.Common;

namespace DigitalMusic.Domain.Entities;

public class Playlist : BaseGuidEntity
{
    public string name { get; set; }
    public Guid user_id { get; set; }
    [ForeignKey("user_id")] public User user { get; set; }
}