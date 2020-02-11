using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using Engine.Base;
using System.Diagnostics;

namespace Engine
{
    public delegate void ObjectIDHandler(string ID);

    public static class GameUtilities
    {
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static GameTime Time { get; set; }
        public static float DeltaTime { get { return (float)Time.ElapsedGameTime.TotalSeconds; } }
		
		public static ContentManager Content { get; set; }
		public static ContentManager SceneContent {get;set;}
		
        public static Random Random { get; set; }
        public static SpriteFont DebugFont { get; set; }
        public static SpriteBatch DebugSpriteBatch { get; set; }

        public static Color DebugTextColor { get; set; }
        public static bool GameHasFocus { get; set; }


        public static Vector3 PickRandomPosition(int min, int max)
        {
            return new Vector3(
                Random.Next(min, max),
                Random.Next(min, max),
                Random.Next(min, max));
        }

        public static Color PickRandomColor()
        {
            return new Color(
                Random.Next(1, 255),
                Random.Next(1, 255),
                Random.Next(1, 255));
        }

        public static void SetGraphicsDeviceFor3D()
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }
    }

}
