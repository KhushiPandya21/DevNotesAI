
using System.ComponentModel.DataAnnotations;

namespace DevNotesAI.Models
{
    public class AnalysisHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AnalysisType { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Language { get; set; } = string.Empty;

        [Required]
        public string SourceCode { get; set; } = string.Empty;

        [Required]
        public string Result { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
