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


    public static int WindowWidth { get; set; }
    public static int WindowHeight { get; set; }

    private static WindowState _windowState;

    public static WindowState WindowState
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

    static MainViewModel()
    {
        WindowWidth = AppSetting.Default.MainWindowWidth;
        WindowHeight = AppSetting.Default.MainWindowHeight;
        _windowState = (WindowState)AppSetting.Default.MainWindowState;
    }

    public MainViewModel(ILogger<MainViewModel> logger)
    {
        _logger = logger;


    }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(Progress), nameof(ProgressState), nameof(ProgressVisible))]
    private int currentStep;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(Progress), nameof(ProgressState), nameof(ProgressVisible))]
    private int totalStep;

    public double Progress => 100 * (double)CurrentStep / (double)TotalStep;
    public string ProgressState => TestCommand.IsRunning ? "正在执行" : "";
    public bool ProgressVisible => CurrentStep > 0;

    [RelayCommand]
    private async Task Test()
    {
        _logger.LogInformation("testing");
        TotalStep = 10;
        for (int i = 0; i < 10; i++)
        {
            CurrentStep++;
            await Task.Delay(1000);
        }
    }
}