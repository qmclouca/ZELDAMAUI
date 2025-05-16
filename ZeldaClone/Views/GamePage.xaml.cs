using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;

namespace ZeldaClone.Views;

public partial class GamePage : ContentPage
{
    private readonly IDatabaseService _db;
    private readonly Player _player;
    private readonly GameLoopService _gameLoop;

    public GamePage(IDatabaseService db)
    {
        InitializeComponent();

        _db = db;
        _player = new Player("spritesheet.png");

        var drawable = new GameDrawable(_player);
        gameView.Drawable = drawable;

        _gameLoop = new GameLoopService(gameView, drawable);
        _gameLoop.Start();

        LoadGame();
    }

    private async void LoadGame()
    {
        var data = await _db.GetLastPlayerAsync();
        if (data != null)
        {
            _player.X = data.X;
            _player.Y = data.Y;
        }
    }

    private async void SaveGame()
    {
        var playerData = new PlayerData
        {
            X = _player.X,
            Y = _player.Y,
            Health = 100,
            EquippedItem = "Sword"
        };

        await _db.SavePlayerAsync(playerData);
    }
}