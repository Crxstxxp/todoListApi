
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todoListApi.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required bool IsCompleted { get; set; }

        [Required]
        [ForeignKey("Users")]
        public required string UserId { get; set; }
    }
}