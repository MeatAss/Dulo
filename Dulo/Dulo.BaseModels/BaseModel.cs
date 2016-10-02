using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Factories;

namespace Dulo.BaseModels
{
    public abstract class BaseModel : BaseBasis
    {
        protected Texture2D texture;

        public Body Body { get; private set; }
        protected World world;

        protected Vector2 center;

        public Vector2 Position
        {
            get
            {
                return  ConvertUnits.ToDisplayUnits(Body.Position);
            }

            set
            {
                Body.Position = ConvertUnits.ToSimUnits(value);
            }
        }

        /// <summary>
        /// Degrees!! Bleat
        /// </summary>
        public float Angle
        {
            get
            {
                return MathHelper.ToDegrees(Body.Rotation);
            }

            set
            {
                Body.Rotation = MathHelper.ToRadians(value);
            }
        }


        public BaseModel(World world, Texture2D physicalTextureMap)
        {
            this.world = world;
            Initialize(physicalTextureMap);
        }


        public override void Draw(SpriteBatch canvas)
        { 
            canvas.Draw(texture, ConvertUnits.ToDisplayUnits(Body.Position), null, Color.White, Body.Rotation, center, 1f, SpriteEffects.None, 0f);           
        }

        private void Initialize(Texture2D physicalTextureMap)
        {
            var textureVertices = CreateVerticesFromPhysicalTextureMap(physicalTextureMap);

            center = GetCenterFromVertices(textureVertices);

            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);

            var list = TriangulateVertices(textureVertices, TriangulationAlgorithm.Bayazit);

            Body = CreateBody(list);
        }

        private Vertices CreateVerticesFromPhysicalTextureMap(Texture2D physicalTextureMap)
        {
            uint[] data = new uint[physicalTextureMap.Width * physicalTextureMap.Height];
            physicalTextureMap.GetData(data);

            return PolygonTools.CreatePolygon(data, physicalTextureMap.Width, false);
        }

        
        private Vector2 GetCenterFromVertices(Vertices vertices)
        {
            Vector2 centroid = -vertices.GetCentroid();
            vertices.Translate(ref centroid);

            return -centroid;
        }

        private List<Vertices> TriangulateVertices(Vertices vertices, TriangulationAlgorithm algorithm)
        {
            List<Vertices> list = Triangulate.ConvexPartition(vertices, algorithm);

            Vector2 vertScale = new Vector2(ConvertUnits.ToSimUnits(1)) * 1f;
            list.ForEach((item) => item.Scale(ref vertScale));

            return list;
        }

        private Body CreateBody(List<Vertices> verticesList)
        {
            var result = BodyFactory.CreateCompoundPolygon(world, verticesList, 1f, BodyType.Dynamic);
            result.BodyType = BodyType.Dynamic;
            return result;
        }

    }
}
