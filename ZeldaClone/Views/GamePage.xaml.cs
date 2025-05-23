using Domain.Entities;
using Domain.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Timers;

namespace ZeldaClone.Views;

public partial class GamePage : ContentPage
{
    private readonly IDatabaseService _db;
    private System.Timers.Timer _moveTimer;
    private string _currentDirection;
    private SKBitmap _spriteSheet;
    private SKBitmap _levelBitmap;
    private int _frameWidth = 16;
    private int _frameHeight = 16;
    private int _currentFrame = 0;
    private int _moveDelta = 10;
    private Player _player;
    
    public GamePage(IDatabaseService db)
    {
        InitializeComponent();

        _db = db;
        _player = new Player("spritesheet.png");
        _moveTimer = new System.Timers.Timer(20);
        _moveTimer.Elapsed += OnMoveTimerElapsed;
        _moveTimer.AutoReset = true;
        
        LoadResources();
    }
    private async void LoadResources() 
    {
        await LoadMap();
        LoadSpriteSheet();
    }

    private void OnMoveTimerElapsed(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            MovePlayer(_currentDirection);
            canvasView.InvalidateSurface();
        });
    }

    private void MovePlayer(string Direction)
    {
        switch (Direction)
        {
            case "down": _player.Y += _moveDelta; break;
            case "up": _player.Y -= _moveDelta; break;
            case "left": _player.X -= _moveDelta; break;
            case "right": _player.X += _moveDelta; break;
        }
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

    private async Task LoadMap()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("level1.png");
        _levelBitmap = SKBitmap.Decode(stream);
    }

    private async void LoadSpriteSheet()
    {       
        using var stream = await FileSystem.OpenAppPackageFileAsync("spritesheet.png");
        _spriteSheet = SKBitmap.Decode(stream);
    }

    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);
        if (_levelBitmap != null)
        {
            canvas.DrawBitmap(_levelBitmap, new SKRect(0, 0, e.Info.Width, e.Info.Height));
        }

        if (_spriteSheet == null) return;

        var src = new SKRectI(
            _currentFrame * _frameWidth, 0,
            (_currentFrame + 1) * _frameWidth, _frameHeight);
        var dest = new SKRect(_player.X, _player.Y,
                       _player.X + 64, _player.Y + 64);
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

    private void MoveUp_Pressed(object sender, EventArgs e)
    {
        _currentDirection = "up";
        _moveTimer.Start();
    }

    private void MoveLeft_Pressed(object sender, EventArgs e)
    {
        _currentDirection = "left";
        _moveTimer.Start();
    }

    private void MoveRight_Pressed(object sender, EventArgs e)
    {
        _currentDirection = "right";
        _moveTimer.Start();
    }

    private void MoveDown_Pressed(object sender, EventArgs e)
    {
        _currentDirection = "down";
        _moveTimer.Start();
    }

    private void Move_Released(object sender, EventArgs e)
    {
        _moveTimer.Stop();
    }
}