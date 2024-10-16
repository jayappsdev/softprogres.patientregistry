using CommunityToolkit.Mvvm.ComponentModel;

namespace SoftProgres.PatientRegistry.Desktop.Models;

public partial class Patient : ObservableObject
{
    [ObservableProperty]
    private long _id;

    [ObservableProperty]
    private string _birthNumber = string.Empty;

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private Sex _sex;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _phoneNumber = string.Empty;

    [ObservableProperty]
    private string _streetAndNumber = string.Empty;

    [ObservableProperty]
    private string _city = string.Empty;

    [ObservableProperty]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    private string _state = string.Empty;

    [ObservableProperty]
    private string _workplace = string.Empty;

    [ObservableProperty]
    private int? _age;

    [ObservableProperty]
    private DateTime? _dateOfBirth;
}