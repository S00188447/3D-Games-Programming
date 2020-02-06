using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DrawingModels;
using Engine.Engines;



namespace Sample_Models
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        InputManager input;
        Camera camera;

        //model to be loaded and rendered (vertices indices and effects)
        Model model;

        //transforms for the bones of the model
        Matrix[] bonesTransforms;

        //world matrix, how is this object transformed in the world space
        Matrix world = Matrix.Identity;





        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            input = new InputManager(this);
            camera = new Camera(this, new Vector3(0, 10, 30), new Vector3(0, 0, -1));



        }

        protected override void Initialize()
        {


            base.Initialize();
        }


        protected override void LoadContent()
        {
            RasterizerState rasterizer = new RasterizerState();
            rasterizer.FillMode = FillMode.WireFrame;
            rasterizer.CullMode = CullMode.None;

            GraphicsDevice.RasterizerState = rasterizer;
            //load the 3d model
            model = Content.Load<Model>("building");
            bonesTransforms = new Matrix[model.Bones.Count];
            //copy the transformation of each bone in the model
            model.CopyAbsoluteBoneTransformsTo(bonesTransforms);

            //model = Content.Load<Model>("monkey");


        }

   
        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Escape))
                Exit();

            world *= Matrix.CreateRotationY(MathHelper.ToRadians(0.1f));


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach(ModelMeshPart part in mesh.MeshParts)
                {
                    (part.Effect as BasicEffect).View = camera.View;
                    (part.Effect as BasicEffect).Projection = camera.Projection;
                    (part.Effect as BasicEffect).World = bonesTransforms[mesh.ParentBone.Index] * world;
                    (part.Effect as BasicEffect).EnableDefaultLighting();



                }
                mesh.Draw();
            }

   

            base.Draw(gameTime);
        }
    }
}
