using Domain.Interfaces;

namespace ZeldaClone;

public partial class App : Application
{
    public App(IDatabaseService databaseService)
    {
        InitializeComponent();

        MainPage = new ZeldaClone.Views.GamePage(databaseService);
    }
}
