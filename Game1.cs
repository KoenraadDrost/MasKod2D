using MasKod2D.behaviour;
using MasKod2D.entity;
using MasKod2D.GraphFromBook;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();

            world = new World(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, GraphicsDevice);
            world.GenerateGraph();
            world.Populate();
            

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
                double x = Math.Round(newState.X / 1024.0, 1) * 10;
                double y = Math.Round(newState.Y / 1024.0, 1) * 10;
                Console.WriteLine("X: " + x + " Y: " + y);

                world.Player.End = world.Graph.GetNode((float)x, (float)y);
                foreach(MovingEntity me in world.entities)
                {
                    me.End = world.Graph.GetNode((float)x, (float)y);
                }

            }
            oldState = newState; // this reassigns the old state so that it is ready for next time
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            world.Update(0.8f, _spriteBatch);
          
            base.Draw(gameTime);
        }

    }
}
