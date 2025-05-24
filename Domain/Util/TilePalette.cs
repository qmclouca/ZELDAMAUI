using SkiaSharp;

namespace Domain.Util
{
    public class TilePalette
    {
        public readonly Dictionary<SKColor, (int X, int Y)> _colorToTileCoords = new()
        {
            [new SKColor(53, 53, 0)] = (0, 0), // grass
            [new SKColor(255, 255, 255)] = (1, 0), // wall
            [new SKColor(121, 35, 0)] = (6, 0), // health
            [new SKColor(112, 27, 121)] = (6, 1), // ammo
            [new SKColor(0,38,255)] = (2, 0) // player
        };
    }
}
