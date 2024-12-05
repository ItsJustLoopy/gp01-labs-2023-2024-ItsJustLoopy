using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tracker.WebAPIClient;

namespace Week_3_Lab_2_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CustomTextRenderer _customTextRenderer;

        Texture2D txSignPost, txPlayer, txBackground;
        Vector2 signPostPos, playerPos;
        Rectangle signPostRect, playerRect;

        Vector2 TextPos;

        bool collisionOccured;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa", activityName: "GP01 2024 Week 3 Lab 2", Task: "week 3 Lab 2 finished");
            _customTextRenderer = new CustomTextRenderer(GraphicsDevice);
            collisionOccured = false;
            TextPos = new Vector2(50, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            txBackground = Content.Load<Texture2D>(@"Background");
            txPlayer = Content.Load<Texture2D>(@"body");
            txSignPost = Content.Load<Texture2D>(@"Sign_yield");

            playerPos = new Vector2(100, 100);
            signPostPos = new Vector2(0, 0);

            playerRect = new Rectangle(playerPos.ToPoint(), new Point(txPlayer.Width, txPlayer.Height));
            signPostRect = new Rectangle(signPostPos.ToPoint(), new Point(txSignPost.Width, txSignPost.Height));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                playerPos += new Vector2(0, -1);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                playerPos += new Vector2(0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                playerPos += new Vector2(1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                playerPos += new Vector2(-1,0);

            playerRect = new Rectangle(playerPos.ToPoint(), new Point(txPlayer.Width, txPlayer.Height));

            if (playerPos.X < 0)
                playerPos.X = 0;
            if (playerPos.Y < 0)
                playerPos.Y = 0;

            if (playerPos.X + playerRect.Width > _graphics.GraphicsDevice.Viewport.Width)
                playerPos.X = _graphics.GraphicsDevice.Viewport.Width - playerRect.Width;
            if (playerPos.Y + playerRect.Height > _graphics.GraphicsDevice.Viewport.Height)
                playerPos.Y = _graphics.GraphicsDevice.Viewport.Height - playerRect.Height;

            // TODO: Add your update logic here
            if (playerRect.Intersects(signPostRect))
                collisionOccured = true;
            else collisionOccured = false;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(txBackground, GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.Draw(txPlayer, playerPos, Color.White);
            _spriteBatch.Draw(txSignPost, signPostPos, Color.White);
            if (collisionOccured)
                _customTextRenderer.RenderText(_spriteBatch, "collision", TextPos, Color.White);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
