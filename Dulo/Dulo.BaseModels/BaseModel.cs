using System;
using System.Collections.Generic;
using Dulo.BaseModels.SettingsModels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Factories;
using Dulo.BasisModels;

namespace Dulo.BaseModels
{
    public abstract class BaseModel : BaseBasis
    {
        protected Texture2D Texture { get; set; }

        public Body Body { get; private set; }

        protected World World { get; }

        protected Vector2 Center { get; private set; }

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
        
        public float Angle
        {
            get
            {
                if (Body.Rotation >= 0)
                {
                    return Body.Rotation % MathHelper.TwoPi;
                }

                return MathHelper.TwoPi - Math.Abs(Body.Rotation) % MathHelper.TwoPi;
            }
        }

        public float LinearDamping
        {
            get { return Body.LinearDamping; }

            set { Body.LinearDamping = value; }
        }

        public float AngularDamping
        {
            get { return Body.AngularDamping; }

            set { Body.AngularDamping = value; }
        }


        protected BaseModel(World world, Texture2D physicalTextureMap)
        {
            World = world;
            Initialize(physicalTextureMap);
        }

        protected BaseModel(SettingBaseModel settingBaseModel)
        {
            World = settingBaseModel.World;
            Initialize(settingBaseModel.PhysicalTextureMap);
        }

        public override void Draw(SpriteBatch canvas)
        { 
            canvas.Draw(Texture, ConvertUnits.ToDisplayUnits(Body.Position), null, Color.White, Body.Rotation, Center, 1f, SpriteEffects.None, 0f);           
        }

        public Vector2 GetDirection()
        {
            return new Vector2((float)Math.Cos(Body.Rotation - Math.PI / 2),
                (float)Math.Sin(Body.Rotation - Math.PI / 2));
        }

        public void Rotate(float speed)
        {
            Body.ApplyTorque(speed);
        }

        public void MoveTo(float speed)
        {
            Body.ApplyForce(GetDirection() * speed);
        }


        private void Initialize(Texture2D physicalTextureMap)
        {
            var textureVertices = CreateVerticesFromPhysicalTextureMap(physicalTextureMap);

            Center = GetCenterFromVertices(textureVertices);

            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);

            var list = TriangulateVertices(textureVertices, TriangulationAlgorithm.Bayazit);
            
            Body = CreateBody(list);
        }

        private Vertices CreateVerticesFromPhysicalTextureMap(Texture2D physicalTextureMap)
        {
            var data = new uint[physicalTextureMap.Width * physicalTextureMap.Height];
            physicalTextureMap.GetData(data);

            return PolygonTools.CreatePolygon(data, physicalTextureMap.Width, false);
        }

        
        private Vector2 GetCenterFromVertices(Vertices vertices)
        {
            var centroid = -vertices.GetCentroid();
            vertices.Translate(ref centroid);

            return -centroid;
        }

        private List<Vertices> TriangulateVertices(Vertices vertices, TriangulationAlgorithm algorithm)
        {
            var list = Triangulate.ConvexPartition(vertices, algorithm);

            var vertScale = new Vector2(ConvertUnits.ToSimUnits(1)) * 1f;
            list.ForEach((item) => item.Scale(ref vertScale));

            return list;
        }

        private Body CreateBody(List<Vertices> verticesList)
        {
            var result = BodyFactory.CreateCompoundPolygon(World, verticesList, 1f, BodyType.Dynamic);
            result.BodyType = BodyType.Dynamic;
            return result;
        }

    }
}
