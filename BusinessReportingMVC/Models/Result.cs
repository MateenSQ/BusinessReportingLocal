using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessReportingMVC.Models
{
    [NotMapped]
    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
