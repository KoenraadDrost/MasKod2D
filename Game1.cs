using MasKod2D.behaviour;
using MasKod2D.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MasKod2D
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPosition;
        float ballSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World world;
        private MouseState oldState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
            // _graphics.PreferredBackBufferHeight / 2);
            // ballSpeed = 100f;
            int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            world = new World(width, height, GraphicsDevice);
            world.populate();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // ballTexture = Content.Load<Texture2D>("ball");
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            // if (kstate.IsKeyDown(Keys.W))
            //     ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if (kstate.IsKeyDown(Keys.S))
            //     ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if (kstate.IsKeyDown(Keys.A))
            //     ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if (kstate.IsKeyDown(Keys.D))
            //     ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
            //     ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
            // else if (ballPosition.X < ballTexture.Width / 2)
            //     ballPosition.X = ballTexture.Width / 2;

            // if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
            //     ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
            // else if (ballPosition.Y < ballTexture.Height / 2)
            //     ballPosition.Y = ballTexture.Height / 2;
            MouseState newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                world.Target.Pos = new Vector2D(newState.X, newState.Y);
            }
            oldState = newState; // this reassigns the old state so that it is ready for next time
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // _spriteBatch.Begin();
            // _spriteBatch.Draw(
            //     ballTexture, 
            //     ballPosition, 
            //     null, 
            //     Color.White, 
            //     0f, 
            //     new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), 
            //     Vector2.One, 
            //     SpriteEffects.None, 
            //     0f
            //     );
            foreach (MovingEntity me in world.entities)
            {
                //me.SB = new SeekBehaviour(me);
                //me.SB = new FleeBehaviour(me);
                me.SB = new ArriveBehaviour(me);
                me.Update(0.8f);
                _spriteBatch.Begin();
                _spriteBatch.Draw(me.Texture, new Vector2((float)me.Pos.X, (float)me.Pos.Y), Color.White);
                _spriteBatch.End();
            }

            // Target
            _spriteBatch.Begin();
            _spriteBatch.Draw(world.Target.Texture, new Vector2((float)world.Target.Pos.X, (float)world.Target.Pos.Y), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
