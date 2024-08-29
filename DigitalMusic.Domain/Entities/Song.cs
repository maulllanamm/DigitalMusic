using DigitalMusic.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusic.Domain.Entities
{
    public class Song : BaseGuidEntity
    {
        public string title { get; set; }
        public int year { get; set; }
        public string performer { get; set; }
        public string genre { get; set; }
        public int duration { get; set; }
        public Guid? album_id { get; set; }
        [ForeignKey("album_id")]
        public Album? album { get; set; }
    }
}
