using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
namespace Engine.Base
{
   public class CameraComponent
    {

        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        public BoundingFrustum Frusturn { get { return new BoundingFrustum(View * Projection); } }

        public float farPlane = 1000;
        public float nearPlane = 0.1f;
        public Vector3 up = Vector3.Up;

        public Vector3 Direction { get; set; }
        public Vector3 Target { get; set; }
        public int MyProperty { get; set; }


    }
}
