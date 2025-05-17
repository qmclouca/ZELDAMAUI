using Domain.Entities;
using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiGraphicsImage = Microsoft.Maui.Graphics.IImage;


namespace Domain.Services
{
    public class GameDrawable : IDrawable
    {
        private readonly Player _player;
        private readonly MauiGraphicsImage _spriteSheet;

        public GameDrawable(Player player)
        {
            _player = player;
            _spriteSheet = LoadImage("spritesheet.png"); // nome minúsculo
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (_spriteSheet != null)
            {
                var src = _player.GetSourceRect();
                var dst = new RectF(_player.X, _player.Y, 32, 32); // tamanho na tela

//                canvas.DrawImage(
//    _spriteSheet,
//    (float)src.X, (float)src.Y,           // origem da imagem
//    (float)src.Width, (float)src.Height, // tamanho da parte cortada da sprite
//    _player.X, _player.Y,                 // destino na tela
//    32, 32                                // tamanho de renderização
//);
            }
        }

        private MauiGraphicsImage LoadImage(string filename)
        {
            var file = FileSystem.OpenAppPackageFileAsync(filename).Result;
            using var stream = file;
            return PlatformImage.FromStream(stream);
        }
    }


}
