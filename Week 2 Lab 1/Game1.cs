using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Security.Principal;
using System.Windows.Forms;
using Tracker.WebAPIClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Week_2_Lab_1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _txBackground, _txCircle, _txBox, _txDownArrow, _txRightArrow;

        private Vector2 _centreOrigin;
        SpriteEffects _rightArrowEffect = SpriteEffects.None;
        float _rotation = 0f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ActivityAPIClient.Track(StudentID: "S00256842", StudentName: "Sami Benaissa",activityName: "GP01 2024 Week 2 Lab 1",Task: "Right Arrow Rotated");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _txBackground = Content.Load<Texture2D>(@"Yellow Box");
            _txCircle = Content.Load<Texture2D>(@"see through circle");
            _txBox = Content.Load<Texture2D>(@"Magenta Box");
            _txDownArrow = Content.Load<Texture2D>(@"Down Arrow");
            _txRightArrow = Content.Load<Texture2D>(@"Right Arrow");

            _centreOrigin = _txRightArrow.Bounds.Center.ToVector2();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            KeyboardState CurrentKeyState = Keyboard.GetState();

            if (CurrentKeyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)
                && _rightArrowEffect == SpriteEffects.None)
            { 
                _rightArrowEffect = SpriteEffects.FlipVertically;
            }
            else if (CurrentKeyState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F)
                && _rightArrowEffect == SpriteEffects.FlipVertically)
            {
                _rightArrowEffect = SpriteEffects.None;
            }
            // TODO: Add your update logic here

            if (CurrentKeyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                _rotation -= .01f;

            if (CurrentKeyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E))
                _rotation += .01f;

            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Color AlphaColor = new Color(Color.White, 255);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            _spriteBatch.Draw(_txBackground, GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.Draw(_txCircle, new Vector2(100,100), Color.White);
            _spriteBatch.Draw(_txBox, new Vector2(150,150), AlphaColor);
            _spriteBatch.Draw(_txRightArrow, GraphicsDevice.Viewport.Bounds.Center.ToVector2(),null, Color.White, _rotation, _centreOrigin, 1f, _rightArrowEffect, 0.5f);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
