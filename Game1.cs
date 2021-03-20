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
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                world.Target.Pos = new Vector2D(newState.X - (world.Target.Texture.Width / 2), newState.Y - (world.Target.Texture.Height / 2));
            }
            oldState = newState; // this reassigns the old state so that it is ready for next time
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (MovingEntity me in world.entities)
            {
                //me.SB = new SeekBehaviour(me);
                //me.SB = new FleeBehaviour(me);
                //me.SB = new ArriveBehaviour(me);
                me.SB = new HideBehaviour(me);
                me.Update(0.8f);
                _spriteBatch.Begin();
                _spriteBatch.Draw(me.Texture, new Vector2((float)me.Pos.X, (float)me.Pos.Y), Color.White);
                _spriteBatch.End();
            }

            foreach (StaticEntity se in world.obstacles)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(se.Texture, new Vector2((float)se.Pos.X, (float)se.Pos.Y), Color.White);
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
