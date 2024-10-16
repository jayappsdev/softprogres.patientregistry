namespace SoftProgres.PatientRegistry.Desktop.Models;

public class PatientOperationErrorEventArgs : EventArgs
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class PatientOperationCompletedEventArgs : EventArgs
{
    public Patient? Patient { get; set; }
}