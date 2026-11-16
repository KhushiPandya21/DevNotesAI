using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNotesAI.Models
{
    public class Documentation
    {
        [Key]
        public int DocumentationId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}