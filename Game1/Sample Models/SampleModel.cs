using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DrawingModels;
using Engine.Engines;
using Microsoft.Xna.Framework.Content;

namespace Sample_Models
{
    public class SampleModel: Game1
    {
        Model model;

        //transforms for the bones of the model
        Matrix[] bonesTransforms;
        //world matrix, how is this object transformed in the world space
        Matrix world = Matrix.Identity;
        string asset;

        public SampleModel(string assetName, Vector3 position)
        {
            asset = assetName;
            world = Matrix.Identity * Matrix.CreateTranslation(position);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void LoadContent(ContentManager content)
        {

            model = Content.Load<Model>(asset);//load model
            bonesTransforms = new Matrix[model.Bones.Count];//instatiable array
            model.CopyAbsoluteBoneTransformsTo(bonesTransforms);//copy bones transforms to array
        }
        public void Draw(Matrix view, Matrix projection)
        {         
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)//mesges have effects and we need to set the properties of the feffect before drawing the verices
                {
                    //effect.World = bonesTransforms(mesh.ParentBone.Index) * world; //where the object is in the game
                    effect.View = view;//where the camera is facing and what direction is it looking in
                    effect.Projection = projection;//details of the window the game is being rendered to (apsect ratio, POV)
                    effect.EnableDefaultLighting();
                }
                mesh.Draw(); //send the vertices to the GPU and the effect to render them
            }
            //base.Draw(gameTime);
        }
    }
}
