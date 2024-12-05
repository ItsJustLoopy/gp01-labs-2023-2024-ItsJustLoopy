using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tracker.WebAPIClient;

namespace Week_1_Lab_1_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D TxOpening;
        private Texture2D TxGameOver;
        bool Opening = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Activity Tracker
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "SamiBenaissa",
            activityName: "GP01 2024 Week 1 Lab 1", Task: "Week 1 Part 4 Added First Mono Game Project");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TxGameOver = Content.Load<Texture2D>("GameOverSplashScreen");
            TxOpening = Content.Load<Texture2D>("OpeningSplashScreen");

            
        }

        protected override void Update(GameTime gameTime)
        {
            // Escape sequence
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Opening logic
            if (Opening && GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                Opening = false;

                    base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);
            _spriteBatch.Begin();
            if (Opening)
            {
                _spriteBatch.Draw(TxOpening, GraphicsDevice.Viewport.Bounds, Color.White);
            }
            else
            {
                _spriteBatch.Draw(TxGameOver, GraphicsDevice.Viewport.Bounds, Color.White);
            }
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
