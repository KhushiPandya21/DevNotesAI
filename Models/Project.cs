using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace DevNotesAI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProjectName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public ICollection<SourceFile> SourceFiles { get; set; } = new List<SourceFile>();

        public ICollection<Documentation> Documentations { get; set; } = new List<Documentation>();

        public ICollection<SearchHistory> SearchHistories { get; set; } = new List<SearchHistory>();
    }
}