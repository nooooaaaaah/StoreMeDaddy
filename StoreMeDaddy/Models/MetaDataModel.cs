using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreMeDaddy.Objects;

namespace StoreMeDaddy.Models
{
    public class MetaDataModel
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(255)]

        public string FileName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public string Type { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string Version { get; set; } = "0";

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Timestamp]
        public DateTime ModifiedAt { get; set; }

        [Required]
        public DateTime AccessedAt { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserModel User { get; set; }

        [Required]
        public string Hash { get; set; }

        [Required]
        public byte[] Salt { get; set; }

        [Required]
        public byte[] IV { get; set; }
    }
}