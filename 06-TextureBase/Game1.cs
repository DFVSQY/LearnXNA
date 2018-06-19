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

namespace _06_TextureBase
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        /// <summary>
        /// Ч��
        /// </summary>
        private BasicEffect effect;

        /// <summary>
        /// ��������
        /// </summary>
        private VertexPositionTexture[] vertices;

        /// <summary>
        /// ���㻺��
        /// </summary>
        private VertexBuffer vertexBuffer;

        /// <summary>
        /// ��������
        /// </summary>
        private VertexDeclaration vertexDeclaration;

        /// <summary>
        /// ����
        /// </summary>
        private Texture2D texture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // ���ڴ�С
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

            // ����������ͼ
            effect = new BasicEffect(GraphicsDevice, null);
            effect.TextureEnabled = true;

            GraphicsDevice.RenderState.CullMode = CullMode.None;

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

            // ��������
            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionTexture.VertexElements);

            // ��������
            vertices = new VertexPositionTexture[122];
            for (int i = 0; i <= 60; i++)                        // 360��50�ݵȷ�
            {
                // �Ƕ�
                float theta = (float)(2 * Math.PI * i) / 60;

                // �¶���
                // vertices[2 * i].Position = new Vector3((float)Math.Cos(theta), -1f, -(float)Math.Sin(theta));
                vertices[2 * i].Position = new Vector3((float)Math.Sin(theta), -1f, -(float)Math.Cos(theta));
                vertices[2 * i].TextureCoordinate = new Vector2(i / 60f, 1f);

                // �϶���
                //vertices[2 * i + 1].Position = new Vector3((float)Math.Cos(theta), 1f, -(float)Math.Sin(theta));
                vertices[2 * i + 1].Position = new Vector3((float)Math.Sin(theta), 1f, -(float)Math.Cos(theta));
                vertices[2 * i + 1].TextureCoordinate = new Vector2(i / 60f, 0f);
            }

            //���㻺��
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), 122, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionTexture>(vertices);

            // ����
            texture = Content.Load<Texture2D>("textures/scene");
            effect.Texture = texture;

            // ģ�;���
            effect.World = Matrix.Identity;

            // �ӵ����
            Vector3 camPos = new Vector3(0f, 5f, 5f);
            Vector3 camTarget = Vector3.Zero;
            Matrix viewMatrix = Matrix.CreateLookAt(camPos, camTarget, Vector3.Up);
            effect.View = viewMatrix;

            // ͶӰ����
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 0.1f, 10f);
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.VertexDeclaration = vertexDeclaration;
                GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionTexture.SizeInBytes);
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2 * 60);
                pass.End();
            }
            effect.End();

            base.Draw(gameTime);
        }
    }
}
