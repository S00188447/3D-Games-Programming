using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class ScriptComponent: Component
    {
        public event ObjectIDHandler Completed;
        public virtual bool HasCompleted()
        {
            return false;
        }
    }
}
