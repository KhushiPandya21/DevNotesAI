using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNotesAI.Models
{
    public class SourceFile
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Language { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public int LinesOfCode { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }

        public CodeAnalysis? CodeAnalysis { get; set; }

        public ICollection<AIAnalysis> AIAnalyses { get; set; } = new List<AIAnalysis>();
    }
}