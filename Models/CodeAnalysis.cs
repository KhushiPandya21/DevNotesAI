using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNotesAI.Models
{
    public class CodeAnalysis
    {
        [Key]
        public int AnalysisId { get; set; }

        [Required]
        public int FileId { get; set; }

        public int ClassCount { get; set; }

        public int FunctionCount { get; set; }

        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(FileId))]
        public SourceFile? SourceFile { get; set; }
    }
}