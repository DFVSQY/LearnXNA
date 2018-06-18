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

namespace _05_Light
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
        private VertexPositionColor[] vertexes;

        /// <summary>
        /// ���㻺��
        /// </summary>
        private VertexBuffer vertexBuffer;

        /// <summary>
        /// ��������
        /// </summary>
        private VertexDeclaration vertexDeclaration;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 640;
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

            // ��������
            effect.LightingEnabled = true;

            // �����Է���
            effect.EmissiveColor = Color.SkyBlue.ToVector3();

            // ���û�����
            effect.AmbientLightColor = Color.Purple.ToVector3();

            // ���ò��ʵ����������ϵ��
            effect.DiffuseColor = new Vector3(0.8f, 0.5f, 0.2f);

            // ���þ��淴����ɫ��ǿ��
            effect.SpecularColor = new Vector3(0.9f, 0.5f, 0.1f);
            effect.SpecularPower = 64;

            // ����light0
            effect.DirectionalLight0.Enabled = true;
            Vector3 lightDirection0 = new Vector3(10f, 0f, -10f);
            lightDirection0.Normalize();
            effect.DirectionalLight0.Direction = lightDirection0;                       // ��ķ���
            effect.DirectionalLight0.DiffuseColor = Color.White.ToVector3();            // �����������ɫ
            effect.DirectionalLight0.SpecularColor = Color.White.ToVector3();

            IsMouseVisible = true;

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
            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            // ��������
            vertexes = new VertexPositionColor[3]
            {
                new VertexPositionColor(new Vector3(-0.5f,-0.5f,0f),Color.White),
                new VertexPositionColor(new Vector3(0f,0.5f,0f),Color.White),
                new VertexPositionColor(new Vector3(0.5f,-0.5f,0f),Color.White)
            };

            // ���㻺��
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vertexes);

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
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.VertexDeclaration = vertexDeclaration;
                GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionColor.SizeInBytes);
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
                pass.End();
            }
            effect.End();

            base.Draw(gameTime);
        }
    }
}
