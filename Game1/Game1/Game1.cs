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
            base.Initialize();
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
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
