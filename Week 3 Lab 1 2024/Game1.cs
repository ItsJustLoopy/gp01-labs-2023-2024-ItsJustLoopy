using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tracker.WebAPIClient;

namespace Week_3_Lab_1_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteFont font;

        int time;
        byte alpha = 255;

        string message = "Now you see me";
        string timeMessage = "";
        string[] messages = new string[] { "Item 1", "Item 2", "Item 3", "Item 4" };
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa", activityName: "GP01 2024 Week 3 Lab 1", Task: "Main Task Finished");
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("FadeFont");

            Vector2 messageSize = font.MeasureString(messages[0]);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int seconds = gameTime.TotalGameTime.Seconds;
            if (alpha > 0)
                alpha -= (byte)seconds;
            timeMessage = "Time elapsed " + gameTime.TotalGameTime.Seconds.ToString();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            Color Messagecolor = new Color((byte)255, (byte)255, (byte)255, alpha);
            _spriteBatch.DrawString(font, message, new Vector2(100, 100),
            Messagecolor);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
