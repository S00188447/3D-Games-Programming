using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class RenderComponent : Component
    {
        public RenderComponent() : base(){ }
        public virtual void Draw(CameraComponent camera) { }

    }
}
