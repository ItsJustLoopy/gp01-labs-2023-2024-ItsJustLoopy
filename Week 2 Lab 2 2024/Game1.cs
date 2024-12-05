using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX;
using SharpDX.Direct2D1;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using Tracker.WebAPIClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Week_2_Lab_2_2024;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Audio;

namespace Week_2_Lab_2_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;

        private CustomTextRenderer _customTextRenderer;

        // Variables for the Dot
        Texture2D dot;
        Texture2D dot2;
        Microsoft.Xna.Framework.Color dotColor;
        Microsoft.Xna.Framework.Color dot2Color;
        Rectangle dotRect;
        Rectangle dot2Rect;
        int dotSize;
        int dot2Size;

        //Text stuff
        Vector2 TextPos;

        //sound stuff
        private SoundEffect collisionSound;

        // Variables for the Background 
        Texture2D background;
        Rectangle backgroundRect;

        // Variables to hold the display properties
        int displayWidth;
        int displayHeight;

        // Variables to hold the color change
        byte redComponent = 100;
        byte blueComponent = 0;
        byte greenComponent = 0;
        byte alphaComponent = 50;

        byte redComponent2 = 0;
        byte blueComponent2 = 100;
        byte greenComponent2 = 0;
        byte alphaComponent2 = 50;

        bool collisionOcurred = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa", activityName: "GP01 2024 Week 2 Lab 2", Task: "Collision and sound");

            displayWidth = GraphicsDevice.Viewport.Width;
            displayHeight = GraphicsDevice.Viewport.Height;
            // TODO: Add your initialization logic here

            TextPos = new Vector2(100, 100);
            _customTextRenderer = new CustomTextRenderer(GraphicsDevice);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            dot = Content.Load<Texture2D>("WhiteDot");
            dot2 = Content.Load<Texture2D>("WhiteDot");

            dotSize = 40;
            dotRect = new Microsoft.Xna.Framework.Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2, dotSize, dotSize);

            dot2Size = 40;
            dot2Rect = new Microsoft.Xna.Framework.Rectangle(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2, dotSize, dotSize);


            background = Content.Load<Texture2D>("background");
            backgroundRect = new Microsoft.Xna.Framework.Rectangle(0, 0, displayWidth, displayHeight);

            // load the audio content
            collisionSound = Content.Load<SoundEffect>(@"tx0_fire1");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState CurrentKeyState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.B))
                blueComponent++;
            if (Keyboard.GetState().IsKeyDown(Keys.G))
                greenComponent++;
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                redComponent++;
            if (Keyboard.GetState().IsKeyDown(Keys.T))
                alphaComponent++;


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                dotRect.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                dotRect.Y++;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                dotRect.X++;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                dotRect.X--;

            if (dotRect.X < 0)
                dotRect.X = 0;
            if (dotRect.Y < 0)
                dotRect.Y = 0;

            if (dotRect.X + dotRect.Width > _graphics.GraphicsDevice.Viewport.Width)
                dotRect.X = _graphics.GraphicsDevice.Viewport.Width - dotRect.Width;
            if (dotRect.Y + dotRect.Height > _graphics.GraphicsDevice.Viewport.Height)
                dotRect.Y = _graphics.GraphicsDevice.Viewport.Height - dotRect.Height;



            if (Keyboard.GetState().IsKeyDown(Keys.W))
                dot2Rect.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                dot2Rect.Y++;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                dot2Rect.X++;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                dot2Rect.X--;

            if (dot2Rect.X < 0)
                dot2Rect.X = 0;
            if (dot2Rect.Y < 0)
                dot2Rect.Y = 0;

            if (dot2Rect.X + dot2Rect.Width > _graphics.GraphicsDevice.Viewport.Width)
                dot2Rect.X = _graphics.GraphicsDevice.Viewport.Width - dot2Rect.Width;
            if (dot2Rect.Y + dot2Rect.Height > _graphics.GraphicsDevice.Viewport.Height)
                dot2Rect.Y = _graphics.GraphicsDevice.Viewport.Height - dot2Rect.Height;

            if (dot2Rect.Intersects(dotRect))
            {
                collisionOcurred = true;
                collisionSound.Play();
            }
            else collisionOcurred = false;

            dotColor = new Color(redComponent, greenComponent, blueComponent, alphaComponent);
            dot2Color = new Color(redComponent2, greenComponent2, blueComponent2, alphaComponent2);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);


            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, backgroundRect, Color.White);
            if (collisionOcurred)
            {
                _customTextRenderer.RenderText(_spriteBatch, "collision", TextPos, Color.White);
            }

            _spriteBatch.Draw(dot, dotRect, dotColor);
            _spriteBatch.Draw(dot2, dot2Rect, dot2Color);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

       
    }
}
