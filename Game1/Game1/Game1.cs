using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //array to store triangle vertices
        //vertixPostionColour -> Position (X,Y,Z), Colour (R,G,B)
        VertexPositionColor[] colourVertices;
        //shader used to render the vertices
        BasicEffect colorEffect;
        //how is the triangle transformed 
        Matrix colourWorld = Matrix.Identity;

        //Texture Variables
        VertexPositionTexture[] textureVertices;
        BasicEffect textureEffect;
        Matrix textureWorld = Matrix.Identity * Matrix.CreateTranslation(-10,0,0);
        Texture2D texture;

        //Camera
        Matrix view; //where are we? Where are we looking?
        Matrix projection; // Field of view, near and far plane, aspect ratio


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

            Updateview();

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90),
            GraphicsDevice.DisplayMode.AspectRatio,0.1f, 1000);

            base.Initialize();
        }

        void Updateview()
        {
            view = Matrix.CreateLookAt(new Vector3(0,0,10), new Vector3(0,0,-1), Vector3.Up);

        }

        void CreateColourVertices()
        {
            //create the array
            colourVertices = new VertexPositionColor[3];

            //insatiable the 3 vertices -> position and colour 
            colourVertices[0] = new VertexPositionColor(new Vector3(1, 0, 0), Color.Red);//BR
            colourVertices[1] = new VertexPositionColor(new Vector3(-1, 0, 0), Color.Green);//BL
            colourVertices[2] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Blue);//TOP

            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;
        }

        void CreateTextureVertices()
        {
            texture = Content.Load<Texture2D>("uv_texture");
            //create the array
            textureVertices = new VertexPositionTexture[3];

            //insatiable the 3 vertices -> position and colour 
            textureVertices[0] = new VertexPositionTexture(new Vector3(1, 0, 0),new Vector2(1,0));//BR
            textureVertices[1] = new VertexPositionTexture(new Vector3(-1, 0, 0), new Vector2(-1, 0));//BR
            textureVertices[2] = new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(0, 1));//BR

            textureEffect = new BasicEffect(GraphicsDevice);
            textureEffect.VertexColorEnabled = true;
        }
        protected override void LoadContent()
        {

            CreateColourVertices();

        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            Updateview();
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawColourTriangle();

            base.Draw(gameTime);
        }

        private void DrawColourTriangle()
        {
            //pass data from C# to GPU(HLSL)
            colorEffect.World = colourWorld;
            colorEffect.View = view;
            colorEffect.Projection = projection;

            //foreach pass (method) in the shader.....
            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                //apply the pass to the vertices (call the method)
                pass.Apply();

                //send the data
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, colourVertices,
                    0, colourVertices.Length / 3);
            }
        }
    }
}
