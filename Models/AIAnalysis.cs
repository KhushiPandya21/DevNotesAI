using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNotesAI.Models
{
    public class AIAnalysis
    {
        [Key]
        public int AIAnalysisId { get; set; }

        [Required]
        public int FileId { get; set; }

        public string Summary { get; set; } = string.Empty;

        public string Explanation { get; set; } = string.Empty;

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(FileId))]
        public SourceFile? SourceFile { get; set; }
    }
}