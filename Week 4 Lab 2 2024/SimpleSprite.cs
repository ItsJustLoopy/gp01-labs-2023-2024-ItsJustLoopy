using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;

namespace Week_4_Lab_1_2024
{
    internal class SimpleSprite
    {

        public CustomTextRenderer _customTextRenderer;

        public Texture2D Image;
        public Vector2 Position;
        public Rectangle BoundingRect;
        public bool Visible = true;

        public Vector2 textPosition;

        public SimpleSprite(Texture2D spriteImage,
                            Vector2 startPosition)
        {
            Image = spriteImage;
            Position = startPosition;
            BoundingRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, Image.Width, Image.Height);

        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sp)
        {
            if (Visible)
                sp.Draw(Image, Position, Color.White);
        }

        public void Move(Vector2 delta)
        {
            Position += delta;
            BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            BoundingRect.X = (int)Position.X;
            BoundingRect.Y = (int)Position.Y;
        }

        public bool CollisionDetection(SimpleSprite otherSprite)
        {
            return this.BoundingRect.Intersects(otherSprite.BoundingRect);
            
        }

        public void WriteText(Microsoft.Xna.Framework.Graphics.SpriteBatch sp, string text, GraphicsDevice graphicsDevice)
        {
            textPosition = new Vector2(this.Position.X + (this.BoundingRect.Width/4)+5, this.Position.Y-20);
            _customTextRenderer = new CustomTextRenderer(graphicsDevice);
            _customTextRenderer.RenderText(sp, $"{text}", textPosition, Microsoft.Xna.Framework.Color.White);
        }
    }

}
