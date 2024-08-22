using DigitalMusic.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalMusic.Domain.Entities
{
    public class Album : BaseGuidEntity
    {
        public string name { get; set; }
        public int year { get; set; }
        public string? cover { get; set; }
    }
}
