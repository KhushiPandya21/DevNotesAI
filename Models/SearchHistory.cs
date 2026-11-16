using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNotesAI.Models
{
    public class SearchHistory
    {
        [Key]
        public int SearchId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string SearchTerm { get; set; } = string.Empty;

        public DateTime SearchDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}