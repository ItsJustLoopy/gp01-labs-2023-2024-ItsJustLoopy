using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Principal;
using System.Windows.Forms;
using Tracker.WebAPIClient;
using System.Drawing;

using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Point = Microsoft.Xna.Framework.Point;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Microsoft.Xna.Framework.Audio;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Week_4_Lab_1_2024
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CustomTextRenderer _customTextRenderer;

        Texture2D backgroundTx;

        SimpleSprite playerBody; 
        Vector2 _playerPosition;
        Texture2D playerBodyTx;
        Rectangle playerRect;
        string location;

        SimpleSprite playerBody2;
        Vector2 _playerPosition2;
        Texture2D playerBodyTx2;
        Rectangle playerRect2;
        string location2;

        public bool collisionOccured;

        private SoundEffect collisionSound;

        Vector2 TextPos;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa",activityName: "GP01 2024 Week 4 Lab 1",Task: "Attaching Text To Simple Sprite");
            // TODO: Add your initialization logic here
            _customTextRenderer = new CustomTextRenderer(GraphicsDevice);
            
            
            TextPos = new Vector2(GraphicsDevice.Viewport.Width/20, 5);

            backgroundTx = Content.Load<Texture2D>(@"backgroundImage");

            _playerPosition = new Vector2(100, 100);
            playerBodyTx = Content.Load<Texture2D>(@"body");
            playerBody = new SimpleSprite(playerBodyTx,_playerPosition);
            playerRect = playerBody.BoundingRect;

            _playerPosition2 = new Vector2(0, 0);
            playerBodyTx2 = Content.Load<Texture2D>(@"body2");
            playerBody2 = new SimpleSprite(playerBodyTx2, _playerPosition2);
            playerRect2 = playerBody2.BoundingRect;

            collisionOccured = false;

            collisionSound = Content.Load<SoundEffect>(@"tx0_fire1");

            base.Initialize();
        }



        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _playerPosition += new Vector2(0, -1);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _playerPosition += new Vector2(0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _playerPosition += new Vector2(1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _playerPosition += new Vector2(-1, 0);

            if (_playerPosition.X < 0)
                _playerPosition.X = 0;
            if (_playerPosition.Y < 0)
                _playerPosition.Y = 0;

            if (_playerPosition.X + playerRect.Width > _graphics.GraphicsDevice.Viewport.Width)
                _playerPosition.X = _graphics.GraphicsDevice.Viewport.Width - playerRect.Width;
            if (_playerPosition.Y + playerRect.Height > _graphics.GraphicsDevice.Viewport.Height)
                _playerPosition.Y = _graphics.GraphicsDevice.Viewport.Height - playerRect.Height;



            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _playerPosition2 += new Vector2(0, -2);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _playerPosition2 += new Vector2(0, 2);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _playerPosition2 += new Vector2(2, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _playerPosition2 += new Vector2(-2, 0);

            if (_playerPosition2.X < 0)
                _playerPosition2.X = 0;
            if (_playerPosition2.Y < 0)
                _playerPosition2.Y = 0;

            if (_playerPosition2.X + playerRect2.Width > _graphics.GraphicsDevice.Viewport.Width)
                _playerPosition2.X = _graphics.GraphicsDevice.Viewport.Width - playerRect2.Width;
            if (_playerPosition2.Y + playerRect2.Height > _graphics.GraphicsDevice.Viewport.Height)
                _playerPosition2.Y = _graphics.GraphicsDevice.Viewport.Height - playerRect2.Height;

            playerBody.Position = _playerPosition;
            playerBody.BoundingRect = new Rectangle((int)playerBody.Position.X, (int)playerBody.Position.Y, playerBody.Image.Width, playerBody.Image.Height);

            playerBody2.Position = _playerPosition2;
            playerBody2.BoundingRect = new Rectangle((int)playerBody2.Position.X, (int)playerBody2.Position.Y, playerBody2.Image.Width, playerBody2.Image.Height);

            collisionOccured = playerBody.CollisionDetection(playerBody2);

            if (collisionOccured)
                collisionSound.Play();

            location = $"x-{playerBody.Position.X}---y-{playerBody.Position.Y}";
            location2 = $"x-{playerBody2.Position.X}---y-{playerBody2.Position.Y}";
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTx, GraphicsDevice.Viewport.Bounds, Microsoft.Xna.Framework.Color.White);

            _customTextRenderer.RenderText(_spriteBatch, "s00256842-sami-benaissa", TextPos, Microsoft.Xna.Framework.Color.White);

            if(collisionOccured)
                _customTextRenderer.RenderText(_spriteBatch, "collision-ocurred",new Vector2(GraphicsDevice.Viewport.Width /2, 5), Microsoft.Xna.Framework.Color.White);
            
            playerBody.draw(_spriteBatch);
            playerBody2.draw(_spriteBatch);

            playerBody.WriteText(_spriteBatch, location, GraphicsDevice);
            playerBody2.WriteText(_spriteBatch, location2, GraphicsDevice);


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
