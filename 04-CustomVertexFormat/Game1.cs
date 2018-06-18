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

namespace _04_CustomVertexFormat
{
    /// <summary>
    /// 自定义的顶点结构体
    /// </summary>
    public struct MyVertexPositionColor
    {
        private Vector3 _position;
        private Color _color;

        public MyVertexPositionColor(Vector3 pos, Color color)
        {
            _position = pos;
            _color = color;
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public static int SizeInBytes
        {
            get { return 16; }
        }

        public static readonly VertexElement[] vertexElements = new VertexElement[]
        {
            new VertexElement(0,0,VertexElementFormat.Vector3,VertexElementMethod.Default,VertexElementUsage.Position,0),
            new VertexElement(0,sizeof(float)*3,VertexElementFormat.Color,VertexElementMethod.Default,VertexElementUsage.Color,0)
        };
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        // 效果
        private BasicEffect effect;

        // 顶点数组
        private MyVertexPositionColor[] vertexes;

        // 顶点缓存
        private VertexBuffer vertexBuffer;

        // 顶点声明
        private VertexDeclaration vertexDeclaration;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // 窗口大小
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
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

            // 顶点声明
            vertexDeclaration = new VertexDeclaration(GraphicsDevice, MyVertexPositionColor.vertexElements);

            // 顶点数组
            vertexes = new MyVertexPositionColor[3]
            {
                new MyVertexPositionColor(new Vector3(-0.5f,-0.5f,0f),Color.Red),
                new MyVertexPositionColor(new Vector3(0f,0.5f,0f),Color.Green),
                new MyVertexPositionColor(new Vector3(0.5f,-0.5f,0f),Color.Blue)
            };

            // 顶点缓存
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(MyVertexPositionColor), 3, BufferUsage.None);
            vertexBuffer.SetData<MyVertexPositionColor>(vertexes);
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

                // 使用顶点缓存
                GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, MyVertexPositionColor.SizeInBytes);
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                // 不使用顶点缓存
                // GraphicsDevice.DrawUserPrimitives<MyVertexPositionColor>(PrimitiveType.TriangleList, vertexes, 0, 1);

                pass.End();
            }
            effect.End();

            base.Draw(gameTime);
        }
    }
}
