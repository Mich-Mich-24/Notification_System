// Defines the namespace where the ErrorViewModel class resides
namespace Notification_System.Models
{
    // Represents a model for displaying error information in views
    public class ErrorViewModel
    {
        // Gets or sets the unique request ID associated with the error
        // The '?' indicates this property can be null (nullable reference type)
        public string? RequestId { get; set; }

        // Computed property that determines whether the RequestId should be displayed
        // Returns true if RequestId is not null or empty, false otherwise
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
