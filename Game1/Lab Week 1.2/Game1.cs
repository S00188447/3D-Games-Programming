using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab_Week_1._2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //Colo Vertices
        //array to store triangle vertices
        //VertexPositionColor -> Position (X,Y,Z), Color (R,G,B)
        VertexPositionColor[] colorVertices;
        //Shader used to render the vertices
        BasicEffect colorEffect;
        //How is the triangle transformed?
        Matrix colorWorld = Matrix.Identity;

        

        //Texture Vertices
        VertexPositionTexture[] textureVertices;
        BasicEffect textureEffect;
        Matrix textureWorld = Matrix.Identity * Matrix.CreateTranslation(-2, 0, 0);
        Texture2D texture;

        //Camera 
        Matrix view;//where are we and where are we looking?
        Matrix projection;//FOV, near and far plane, aspect ratio

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            UpdateView();

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(90),
                GraphicsDevice.DisplayMode.AspectRatio,
                0.1f,
                1000);

            base.Initialize();
        }

        void UpdateView()
        {
            view = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, -1), Vector3.Up);
        }

        void CreateTextureVertices()
        {
            texture = Content.Load<Texture2D>("uv_texture");
            //create the array
            textureVertices = new VertexPositionTexture[3];

            //instantiate the 3 vertices -> position and color
            textureVertices[0] = new VertexPositionTexture(new Vector3(1, 0, 0), new Vector2(1, 1));//BR
            textureVertices[1] = new VertexPositionTexture(new Vector3(-1, 0, 0), new Vector2(0, 1));//BL
            textureVertices[2] = new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(0.5f, 0.5f));//TOP

            textureEffect = new BasicEffect(GraphicsDevice);
            textureEffect.TextureEnabled = true;
            textureEffect.Texture = texture;
        }

        void CreateColorVertices()
        {
            //create the array
            colorVertices = new VertexPositionColor[3];

            //instantiate the 3 vertices -> position and color

            //triangle 1
            colorVertices[0] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Red);//BL
            colorVertices[1] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Red);//TL
            colorVertices[2] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Red);//BR

            ////traingle 2
            //colorVertices[3] = new VertexPositionColor(new Vector3(2, -1, 0), Color.Green);//BL
            //colorVertices[4] = new VertexPositionColor(new Vector3(0, 2, -1), Color.Green);//TL
            //colorVertices[5] = new VertexPositionColor(new Vector3(0, -1, -3), Color.Green);//BR

            ////triangle 3
            //colorVertices[6] = new VertexPositionColor(new Vector3(0, 2, -1), Color.Blue);//BL
            //colorVertices[7] = new VertexPositionColor(new Vector3(-2, -1, 0), Color.Blue);//TL
            //colorVertices[8] = new VertexPositionColor(new Vector3(0, -1, -3), Color.Blue);//BR

            //3d triangle
            ////triangle 1
            //colorVertices[0] = new VertexPositionColor(new Vector3(-2, -1, 0), Color.Red);//BL
            //colorVertices[1] = new VertexPositionColor(new Vector3(0, 2, -1), Color.Red);//TL
            //colorVertices[2] = new VertexPositionColor(new Vector3(2, -1, 0), Color.Red);//BR

            ////traingle 2
            //colorVertices[3] = new VertexPositionColor(new Vector3(2, -1, 0), Color.Green);//BL
            //colorVertices[4] = new VertexPositionColor(new Vector3(0, 2, -1), Color.Green);//TL
            //colorVertices[5] = new VertexPositionColor(new Vector3(0, -1, -3), Color.Green);//BR

            ////triangle 3
            //colorVertices[6] = new VertexPositionColor(new Vector3(0, 2, -1), Color.Blue);//BL
            //colorVertices[7] = new VertexPositionColor(new Vector3(-2, -1, 0), Color.Blue);//TL
            //colorVertices[8] = new VertexPositionColor(new Vector3(0, -1, -3), Color.Blue);//BR

            ////traingle 4
            //colorVertices[9] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red);//BL
            //colorVertices[10] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Green);//TL
            //colorVertices[11] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Yellow);//BR

            short[] Indices = new short[12];

            Indices[0] = 0;
            Indices[1] = 1;
            Indices[2] = 2;

            Indices[3] = 2;
            Indices[4] = 1;
            Indices[5] = 3;

            Indices[6] = 0;
            Indices[7] = 0;
            Indices[8] = 0;

            Indices[9] = 0;
            Indices[10] = 0;
            Indices[11] = 0;

            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;
        }

        protected override void LoadContent()
        {
            CreateColorVertices();
            CreateTextureVertices();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                colorWorld *= Matrix.CreateRotationY(MathHelper.ToRadians(1));
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                colorWorld *= Matrix.CreateRotationY(MathHelper.ToRadians(-1));
            }

            UpdateView();

            textureWorld *= Matrix.CreateRotationY(MathHelper.ToRadians(0.5f));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawColorTriangle();
            DrawTextureTriangle();

            base.Draw(gameTime);
        }

        private void DrawColorTriangle()
        {
            //pass data from C# to GPU (HLSL)
            colorEffect.World = colorWorld;
            colorEffect.View = view;
            colorEffect.Projection = projection;

            //foreach pass (method) in the shader....
            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                //apply the pass to the vertices (call the method)
                pass.Apply();

                //send the data
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    colorVertices,
                    0,
                    colorVertices.Length / 3);
            }
        }



        private void DrawTextureTriangle()
        {
            textureEffect.World = textureWorld;
            textureEffect.View = view;
            textureEffect.Projection = projection;

            foreach (EffectPass pass in textureEffect.CurrentTechnique.Passes)
            {
                //apply the pass to the vertices (call the method)
                pass.Apply();

                //send the data
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList,
                    textureVertices,
                    0,
                    textureVertices.Length / 3);
            }
        }
    }
}
