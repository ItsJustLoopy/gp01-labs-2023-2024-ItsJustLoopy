using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Week_4_Lab_1_2024;
using Tracker.WebAPIClient;

namespace Week_4_Lab_2_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CustomTextRenderer _customTextRenderer;
        private Viewport originalViewPort, mapViewport;

        private Texture2D characterTx, backgroundTx, dotTx;

        private SimpleSprite playerBody;
        Rectangle playerRect;

        Vector2 TextPos;
        Vector2 characterPos;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa", activityName: "GP01 2024 Week 4 Lab 2", Task: "Implementing Simple Sprites");

            _customTextRenderer = new CustomTextRenderer(GraphicsDevice);
            TextPos = new Vector2((GraphicsDevice.Viewport.Width / 2 - 80), 5);

            characterPos = new Vector2(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTx = Content.Load<Texture2D>("main");
            characterTx = Content.Load<Texture2D>("blueDot");
            playerBody = new SimpleSprite(characterTx, characterPos);
            playerRect = playerBody.BoundingRect;
            dotTx = Content.Load<Texture2D>("blueDot");

            originalViewPort = GraphicsDevice.Viewport;

            
            mapViewport = new Viewport
            {
                X = 0, 
                Y = 0,
                Width = originalViewPort.Width / 4,
                Height = originalViewPort.Height / 4
            };
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
                characterPos += new Vector2(0, -1);
            if (keyboardState.IsKeyDown(Keys.S))
                characterPos += new Vector2(0, 1);
            if (keyboardState.IsKeyDown(Keys.D))
                characterPos += new Vector2(1, 0);
            if (keyboardState.IsKeyDown(Keys.A))
                characterPos += new Vector2(-1, 0);



            playerBody.Position = characterPos;
            playerBody.BoundingRect = new Rectangle((int)playerBody.Position.X, (int)playerBody.Position.Y, playerBody.Image.Width, playerBody.Image.Height);




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Viewport = originalViewPort;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTx, originalViewPort.Bounds, Color.White);
            playerBody.draw(_spriteBatch);
            _customTextRenderer.RenderText(_spriteBatch, "s00256842-sami-benaissa", TextPos, Color.White);
            _spriteBatch.End();

            
            GraphicsDevice.Viewport = mapViewport;
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTx, mapViewport.Bounds, Color.White);

            
            Vector2 minimapPlayerPos = new Vector2(characterPos.X * 0.25f, characterPos.Y * 0.25f); // Scale down position for minimap
            _spriteBatch.Draw(dotTx, minimapPlayerPos, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

