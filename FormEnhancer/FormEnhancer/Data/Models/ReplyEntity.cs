using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace FormEnhancer.Data.Models
{
    [Table("Replies")]
    public class ReplyEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Sectors are required.")]
        public string SectorIds { get; set; } = default!;

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Agreeing is required.")]
        public bool AgreedToTerms { get; set; }

        [Write(false)]
        [Required]
        [MinLength(1, ErrorMessage = "Sectors are required.")]
        public int[] SelectedSectorIds { get; set; } = Array.Empty<int>();
    }
}
