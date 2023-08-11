using Avalonia.Controls;
using AvaloniaMVVMTemplate.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AvaloniaMVVMTemplate.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly ILogger<MainViewModel> _logger;



    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }

    private WindowState _windowState;

    public WindowState WindowState
    {
        get => _windowState;
        set
        {
            if (value != WindowState.Minimized)
            {
                AppSetting.Default.MainWindowState = (int)value;
            }

            _windowState = value;
        }
    }

    public MainViewModel(ILogger<MainViewModel> logger)
    {
        _logger = logger;


        WindowWidth = AppSetting.Default.MainWindowWidth;
        WindowHeight = AppSetting.Default.MainWindowHeight;
        _windowState = (WindowState)AppSetting.Default.MainWindowState;
    }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(Progress), nameof(ProgressState))]
    private int currentStep;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(Progress), nameof(ProgressState))]
    private int totalStep;

    public double Progress => 100 * (double)CurrentStep / (double)TotalStep;
    public string ProgressState => TestCommand.IsRunning ? "正在执行" : "";

    [RelayCommand]
    private async Task Test()
    {
        _logger.LogInformation("testing");
        TotalStep = 10;
        for (int i = 0; i < 10; i++)
        {
            CurrentStep++;
            await Task.Delay(3000);
        }        
    }
}
