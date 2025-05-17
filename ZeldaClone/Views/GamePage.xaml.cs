using Domain.Entities;
using Domain.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace ZeldaClone.Views;

public partial class GamePage : ContentPage
{
    private readonly IDatabaseService _db;
    private SKBitmap _spriteSheet;
    private int _frameWidth = 16;
    private int _frameHeight = 16;
    private int _currentFrame = 0;
    private Player _player;
    
    public GamePage(IDatabaseService db)
    {
        InitializeComponent();

        _db = db;
        _player = new Player("spritesheet.png");

        LoadSpriteSheet();

        this.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(100), () =>
        {
            _currentFrame = (_currentFrame + 1) % 4;
            canvasView.InvalidateSurface();
            return true;
        });

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

    private void LoadSpriteSheet()
    {       
        using var stream = FileSystem.OpenAppPackageFileAsync("spritesheet.png").Result;
        _spriteSheet = SKBitmap.Decode(stream);
    }

    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);

        if (_spriteSheet == null) return;

        var src = new SKRectI(
            _currentFrame * _frameWidth, 0,
            (_currentFrame + 1) * _frameWidth, _frameHeight);
        var dest = new SKRect(_player.X, _player.Y,
                       _player.X + 128, _player.Y + 128);
        canvas.DrawBitmap(_spriteSheet, src, dest);
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

    private void MoveUp(object sender, EventArgs e)
    {
        _player.Y -= 5;
        canvasView.InvalidateSurface();
    }

    private void MoveDown(object sender, EventArgs e)
    {
        _player.Y += 5;
        canvasView.InvalidateSurface();
    }

    private void MoveLeft(object sender, EventArgs e)
    {
        _player.X -= 5;
        canvasView.InvalidateSurface();
    }

    private void MoveRight(object sender, EventArgs e)
    {
        _player.X += 5;
        canvasView.InvalidateSurface();
    }
}