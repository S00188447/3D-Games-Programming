using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DrawingModels
{
    public class Camera : GameComponent
    {
         Matrix view;
         Matrix projection;
         Vector3 UpVector;
         float NearPlane = 0.25f;
         float FarPlane = 10000;
        Vector3 position;
        Vector3 direction;
        public Camera(Game game, Vector3 position, Vector3 direction)
            : base(game)
        {
            this.position = position;
            this.direction = direction;

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            NearPlane = 1.0f;
            FarPlane = 1000.0f;
            UpVector = Vector3.Up;

            UpdateView();

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(90),
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio,
                NearPlane,
                FarPlane);

            base.Initialize();
        }

        public override void Update(GameTime gametime)
        {
            UpdateView();

            base.Update(gametime);
        }

        private void UpdateView()
        {
            View = Matrix.CreateLookAt(
               position,
                direction,
                UpVector);
        }

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

    }
}
