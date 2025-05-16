using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Player
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Frame { get; set; } = 0;
        public int FrameWidth { get; set; } = 16;
        public int FrameHeight { get; set; } = 16;
        public ImageSource SpriteSheet { get; }
        public Player(string spritePath)
        {
            SpriteSheet = ImageSource.FromFile(spritePath);
        }

        public RectF GetSourceRect()
        {
            return new RectF(Frame * FrameWidth, 0, FrameWidth, FrameHeight);
        }


        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
            Frame = (Frame + 1) % 4; // ciclo de 4 frames
        }

    }
}
