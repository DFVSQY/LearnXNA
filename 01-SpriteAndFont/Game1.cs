using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace _01_SpriteAndFont
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// the manager of graphics device
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// sprite batch to draw textures and text
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// circle sprite
        /// </summary>
        private Texture2D circleSprite;

        /// <summary>
        /// squre sprite
        /// </summary>
        private Texture2D squreSprite;

        /// <summary>
        /// rotate angle
        /// </summary>
        private float angle = 0f;

        /// <summary>
        /// text font
        /// </summary>
        private SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // load sprites
            circleSprite = Content.Load<Texture2D>("sprites/circle");
            squreSprite = Content.Load<Texture2D>("sprites/squre");

            // load font
            font = Content.Load<SpriteFont>("fonts/KaiTi");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            spriteBatch.Draw(squreSprite, new Vector2(pp.BackBufferWidth / 2f, pp.BackBufferHeight / 2f), null, Color.White, angle, new Vector2(squreSprite.Width / 2f, squreSprite.Height / 2f), 1f, SpriteEffects.None, 1f);
            angle += 0.01f;
            spriteBatch.Draw(circleSprite, new Vector2(pp.BackBufferWidth / 2f, pp.BackBufferHeight / 2f), null, Color.White, 0f, new Vector2(circleSprite.Width / 2f, circleSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
            string text = string.Format("¡˜ ≈ ±º‰: {0} √Î", (int)(gameTime.TotalRealTime.TotalSeconds));
            Vector2 fontSize = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2(pp.BackBufferWidth / 2f, 0), Color.Yellow, 0f, new Vector2(fontSize.X / 2f, 0f), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
