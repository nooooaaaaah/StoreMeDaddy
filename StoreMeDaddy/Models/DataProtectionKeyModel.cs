// using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.AspNetCore.DataProtection;

// namespace StoreMeDaddy.Models
// {
//     public class DataProtectionKeyModel : IDataProtectionKey
//     {
//         [Key]
//         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//         public Guid Id { get; set; }

//         [Required]
//         public DateTimeOffset CreationDate { get; set; }

//         public DateTimeOffset? ExpirationDate { get; set; }

//         [Required]
//         public byte[] FriendlyName { get; set; }

//         [Required]
//         public byte[] Xml { get; set; }
//     }
// }