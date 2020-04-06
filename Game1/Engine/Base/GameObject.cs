using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public class GameObject
    {
        public Scene sceme { get; set; }
        public string ID { get; set; }
        public bool Enabled { get; set; }
        public Matrix World { get; set; }

        List<Component> components = new List<Component>();
        List<string> awaitingRemoval = new List<string>();

        bool Initalized;
        public bool isInitalized { get{ return isInitalized; } }

        public GameObject()
        {
            ID = GetType().Name + Guid.NewGuid();
            Enabled = true;
            World = Matrix.Identity;
        }

        public GameObject(Vector3 position)
        {
            ID = GetType().Name + Guid.NewGuid();
            Enabled = true;
            World = Matrix.Identity * Matrix.CreateTranslation(position);
        }

        public virtual void Intialize()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Intialize();

            }
            Initalized = true;

        }

        public virtual void Intialized()
        {

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Intialized();
            }
        }

        public void Update()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Enabled)
                    components[i].Update();
            }
            for (int i = 0; i < awaitingRemoval.Count; i++)
                RemoveComponoent(awaitingRemoval[i]);
        }

        List<RenderComponent> renderComponents;

        public void Draw(CameraComponent camera)
        {
            renderComponents = components.OfType<RenderComponent>().ToList();

            for (int i = 0; i < renderComponents.Count; i++)
            {
                if (renderComponents[i].Enabled)
                    renderComponents[i].Draw(camera);

            }
            renderComponents.Clear();
        }

        public void AddComponent(Component component)
        {
            if(component !=null)
            {
                component.owner = this;

                if(isInitalized)
                {
                    component.Intialize();
                    component.Intialized();
                }

                component.OnDestroy += Component_OnDestroy;
                components.Add(component);
            }
        }

        private void Component_OnDestroy(string ID)
        {
            awaitingRemoval.Add(ID);
        }

        public event ObjectIDHandler OnDestroy;
        public void Destroy()
        {
            components.Clear();
            if (OnDestroy != null)
                OnDestroy(ID);
        }
        void RemoveComponoent(int index)
        {
            //remove at the specified index
            if(index > -1)
            components.RemoveAt(index);
        }
        public void RemoveComponoent(string ID)
        {
            //find the index of the component with the ID
            int index = components.FindIndex(c => c.ID == ID);
            //remove component(int index)
            RemoveComponoent(index);

            //for (int i = 0; i < components.Count; i++)
            //{
            //    if (components[i].ID == ID)
            //        index = i;
            //    break;
            //}
        }

        public void RemoveComponoent(Component component)
        {
            //remove the first component that mathes the component passed in
            components.Remove(component);
        }

        public float GetDistanceTo(GameObject other)
        {
            return Vector3.Distance(World.Translation, other.World.Translation);   
        }

        public bool HasComponnent<T>()
        {
            return components.Any(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));          
        }
        
        public Component GetComponent()
        {
            return components.Find(c => c.ID == ID); //returns  null if not found
        }

        public T GetComponent<T>() where T: Component
        {
            return (T)components.Find(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));
        }

        public List<T> GetComponents<T>()
        {
            return components.FindAll(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))) as List<T>;
        }
    }
}
