using System.ComponentModel.DataAnnotations;

namespace StuffIt;

public class SettingsViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Max Items must be greater than 0.")]
    public int MaxItems { get; set; }

    // Add other properties and validation attributes as needed
}
