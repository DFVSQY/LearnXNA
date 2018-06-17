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

namespace _02_Triangles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        /// <summary>
        /// ��������
        /// </summary>
        private VertexPositionColor[] vertexes;

        /// <summary>
        /// ���㻺��
        /// </summary>
        private VertexBuffer verBuffer;

        /// <summary>
        /// ��������
        /// </summary>
        private VertexDeclaration verDeclaration;

        /// <summary>
        /// ����Ч��������shader1.1ģ��
        /// ֧�ֶ�����ɫ�����������������
        /// </summary>
        private BasicEffect effect;

        /// <summary>
        /// ͼ���豸
        /// </summary>
        private GraphicsDevice device;

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
            device = graphics.GraphicsDevice;

            effect = new BasicEffect(device, null);
            effect.VertexColorEnabled = true;

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
            // TODO: use this.Content to load your game content here

            // ������������
            verDeclaration = new VertexDeclaration(device, VertexPositionColor.VertexElements);

            // ���ö�������
            vertexes = new VertexPositionColor[9];

            // ��һ��������
            vertexes[0].Position = new Vector3(1.5f, 0f, 0f);
            vertexes[0].Color = Color.Red;
            vertexes[1].Position = new Vector3(-1.5f, 0f, 0f);
            vertexes[1].Color = Color.Green;
            vertexes[2].Position = new Vector3(0f, 1.5f, 0f);
            vertexes[2].Color = Color.Blue;

            // �ڶ���������
            vertexes[3].Position = new Vector3(0f, -1.5f, 0f);
            vertexes[3].Color = Color.Red;
            vertexes[4].Position = new Vector3(-3f, -1.5f, 0f);
            vertexes[4].Color = Color.Green;
            vertexes[5].Position = new Vector3(-1.5f, 0f, 0f);
            vertexes[5].Color = Color.Blue;

            // ������������
            vertexes[6].Position = new Vector3(3f, -1.5f, 0f);
            vertexes[6].Color = Color.Red;
            vertexes[7].Position = new Vector3(0, -1.5f, 0f);
            vertexes[7].Color = Color.Green;
            vertexes[8].Position = new Vector3(1.5f, 0f, 0f);
            vertexes[8].Color = Color.Blue;

            // ��������䶥�㻺����
            verBuffer = new VertexBuffer(device, typeof(VertexPositionColor), 9, BufferUsage.None);
            verBuffer.SetData<VertexPositionColor>(vertexes);

            // ģ�;���
            Matrix worldMatrix = Matrix.Identity;

            // �ӵ����
            Vector3 camPos = new Vector3(0f, 0f, 8f);
            Vector3 camTarget = Vector3.Zero;
            Vector3 camUp = Vector3.Up;
            Matrix viewMatrix = Matrix.CreateLookAt(camPos, camTarget, camUp);

            // ͶӰ����
            float viewAngle = MathHelper.PiOver4;
            float aspectRatio = device.Viewport.AspectRatio;
            float nearPlane = 1.0f;
            float farPlane = 50f;
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearPlane, farPlane);

            // ����ģ���ӵ�ͶӰ����
            effect.World = worldMatrix;
            effect.View = viewMatrix;
            effect.Projection = projectionMatrix;
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
            device.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.VertexDeclaration = verDeclaration;
                device.Vertices[0].SetSource(verBuffer, 0, VertexPositionColor.SizeInBytes);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
                pass.End();
            }
            effect.End();

            base.Draw(gameTime);
        }
    }
}
