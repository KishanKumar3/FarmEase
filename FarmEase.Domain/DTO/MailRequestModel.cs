using System.ComponentModel.DataAnnotations;

namespace FarmEase.Domain.DTO;

public class MailRequestModel
{
    [Required]
    public string ToEmail { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Message { get; set; }
}
