using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX;
using Tracker.WebAPIClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Point = Microsoft.Xna.Framework.Point;

namespace Week_5_Lab_1_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CustomTextRenderer _customTextRenderer;

        private AnimatedTexture collectable, player;

        Vector2 TextPos, StarPos, PlayerPos;
        Texture2D backgroundTx, starStrip, playTx;
        Rectangle collectableRect, playerRect;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa", activityName:" GP01 2425 Week 5 Lab 1", Task: "Implementing Play logic");
            // TODO: Add your initialization logic here

            _customTextRenderer = new CustomTextRenderer(GraphicsDevice);
            TextPos = new Vector2(GraphicsDevice.Viewport.Width/3 + 50, GraphicsDevice.Viewport.Height-15);

            StarPos = new Vector2(100, 60);
            PlayerPos = new Vector2(100, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTx = Content.Load<Texture2D>(@"gameboard");
            

            collectable = new AnimatedTexture(StarPos, 0f, 0.5f,1f);
            collectable.Load(Content, "CollectableStar", 3, 3);
            starStrip = Content.Load<Texture2D>(@"CollectableStar");

            player = new AnimatedTexture(PlayerPos, 0f, 1f, 1f);
            player.Load(Content, "spindash", 6, 3);
            playTx = Content.Load<Texture2D>(@"spindash");

            playerRect = new Rectangle(PlayerPos.ToPoint(), new Point(playTx.Width/6, playTx.Height));
            collectableRect = new Rectangle(StarPos.ToPoint(), new Point(starStrip.Width/3, starStrip.Height));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            collectable.UpdateFrame(elapsed);
            player.UpdateFrame(elapsed);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                PlayerPos += new Vector2(0, -1);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                PlayerPos += new Vector2(0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                PlayerPos += new Vector2(1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                PlayerPos += new Vector2(-1, 0);

            playerRect = new Rectangle(PlayerPos.ToPoint(), new Point(playTx.Width / 6, playTx.Height));

            if (PlayerPos.X < 0)
                PlayerPos.X = 0;
            if (PlayerPos.Y < 0)
                PlayerPos.Y = 0;

            if (PlayerPos.X + playerRect.Width > _graphics.GraphicsDevice.Viewport.Width)
                PlayerPos.X = _graphics.GraphicsDevice.Viewport.Width - playerRect.Width;
            if (PlayerPos.Y + playerRect.Height > _graphics.GraphicsDevice.Viewport.Height)
                PlayerPos.Y = _graphics.GraphicsDevice.Viewport.Height - playerRect.Height;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            

            _spriteBatch.Draw(backgroundTx, GraphicsDevice.Viewport.Bounds, Color.White);
            collectable.DrawFrame(_spriteBatch, StarPos);
            player.DrawFrame(_spriteBatch, PlayerPos);
            _customTextRenderer.RenderText(_spriteBatch, "s00256842-sami-benaissa", TextPos, Color.White);
            



            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
