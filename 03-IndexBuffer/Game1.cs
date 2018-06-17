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

namespace _03_IndexBuffer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        /// <summary>
        /// 效果
        /// </summary>
        private BasicEffect effect;

        /// <summary>
        /// 顶点数组
        /// </summary>
        private VertexPositionColor[] vertexes = new VertexPositionColor[9];

        /// <summary>
        /// 顶点缓存
        /// </summary>
        private VertexBuffer vertexBuffer;

        /// <summary>
        /// 顶点声明
        /// </summary>
        private VertexDeclaration vertexDeclaration;

        /// <summary>
        /// 顶点索引缓存
        /// </summary>
        private IndexBuffer indexBuffer;

        /// <summary>
        /// 颜色列表
        /// </summary>
        private Color[] colors = new Color[8] 
        {
            Color.Pink,Color.Plum,Color.PowderBlue,Color.Purple,
            Color.RosyBrown,Color.RoyalBlue,Color.Salmon,Color.SkyBlue
        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;

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
            effect = new BasicEffect(GraphicsDevice, null);
            effect.VertexColorEnabled = true;

            IsMouseVisible = true;
            Window.AllowUserResizing = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            // 顶点数组
            vertexes[0].Position = Vector3.Zero;
            vertexes[0].Color = Color.Red;
            for (int i = 0; i <= 7; i++)
            {
                vertexes[i + 1].Position = new Vector3((float)Math.Sin(i * MathHelper.PiOver4), (float)Math.Cos(i * MathHelper.PiOver4), 0f);
                vertexes[i + 1].Color = colors[i];
            }

            // 顶点缓存
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 9, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vertexes);

            // 顶点声明
            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            // 索引缓存
            int[] indices = new int[] 
            {
                0,1,2,
                0,2,3,
                0,3,4,
                0,4,5,
                0,5,6,
                0,6,7,
                0,7,8,
                0,8,1
            };
            indexBuffer = new IndexBuffer(GraphicsDevice, typeof(int), 24, BufferUsage.None);
            indexBuffer.SetData<int>(indices);
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
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.VertexDeclaration = vertexDeclaration;
                GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionColor.SizeInBytes);
                GraphicsDevice.Indices = indexBuffer;
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 9, 0, 8);
                pass.End();
            }
            effect.End();

            base.Draw(gameTime);
        }
    }
}
