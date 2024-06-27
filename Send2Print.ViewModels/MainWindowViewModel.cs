using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Send2Print.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        OpenOptionsCommand = new RelayCommand(OpenOptionsWindow);
    }

    public IRelayCommand OpenOptionsCommand { get; }

    private void OpenOptionsWindow()
    {
        var optionsWindow = new Views.OptionsWindow();
        optionsWindow.Show();
    }
}