using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong_Arena
{
    /*
     * An Object is a sprite that spawns at a specific location
     */
    public class Object
    {
        protected Texture2D texture;
        protected Vector2 location;
        protected Rectangle sourceRectangle;
        protected float rotation;
        protected int height;
        protected int width;
        protected int totalFrames;
        protected int displayedFrame;
        protected int frameWidth;
        protected string name;

        //topLeft, topRight, bottomRight, bottomLeft
        private Vector2[] corners = new Vector2[4];
        private struct border
        {
            public Vector2 vec1, vec2, axis;
            public border(Vector2 v1, Vector2 v2)
            {
                vec1 = v1; vec2 = v2; axis = v2 - v1;
            }
        };
        /*
         * Object Constructer -- textureheight and -width
         */
        public Object(string n, Vector2 loc, int h, int w)
        {
            if (name == null)
            {
                Console.Write("Object.name is not initialized");
            }
            this.name = n;
            this.location = loc;
            this.height = h;
            this.width = w;
            sourceRectangle = new Rectangle(1, 0, w, h);
            corners = new Vector2[] {
            new Vector2(loc.X, loc.Y),
            new Vector2(loc.X + width, loc.Y),
            new Vector2(loc.X + width, loc.Y + height),
            new Vector2(loc.X, loc.Y + height),
            };
        }

        /*
         * Object Constructer -- Use for static framebased Objects
         */
        public Object(string n, Vector2 loc, int h, int w, int totalframes, int displayedframe)
        {
            if (name == null)
            {
                Console.Write("Object.name is not initialized");
            }
            this.name = n;
            this.location = loc;
            this.height = h;
            this.width = w;
            totalFrames = totalframes;
            displayedFrame = displayedframe;
            frameWidth = width / totalframes;
            sourceRectangle = new Rectangle(displayedframe * frameWidth, 0, w, h);
            corners = new Vector2[] {
            new Vector2(loc.X, loc.Y),
            new Vector2(loc.X + width, loc.Y),
            new Vector2(loc.X + width, loc.Y + height),
            new Vector2(loc.X, loc.Y + height),
            };
        }

        /*
         * Checks if this Object collides with the tested Object
         * As collision is less commmon it is not based on detecting collision, but on detecting if there's no collision to get maximum performance
         */
        public bool CollidesWith(Object collider)
        {
            border[] borders = { new border(corners[0], corners[1]), new border(corners[1], corners[2]), new border(corners[2], corners[3]), new border(corners[3], corners[0]),
                                 new border(collider.corners[0], collider.corners[1]), new border(collider.corners[1], collider.corners[2]), new border(collider.corners[2], collider.corners[3]), new border(collider.corners[3], collider.corners[0]) };
            List<float> thisScalarProjections = new List<float>();
            List<float> colliderScalarProjections = new List<float>();
            bool collision = true;
            //loop through borders and store the min and max scalarprojection of this and collider allong border
            for (int b = 0; b < borders.Length; b++)
            {
                //get min and max scalarprojection of this.corners[t] along border[b]
                for (int t = 0; t < corners.Length; t++)
                {
                    thisScalarProjections.Add(Vector2.Dot(corners[t] - borders[b].vec1, borders[b].axis) / borders[b].axis.Length());
                }
                //get min and max scalarprojection of collider.corners[t] along border[b]
                for (int c = 0; c < collider.corners.Length; c++)
                {
                    colliderScalarProjections.Add(Vector2.Dot(collider.corners[c] - borders[b].vec1, borders[b].axis) / borders[b].axis.Length());
                }
                //if there's a gap between this and collider found on border[b], set collision to false, exit the loop and return collision
                if (thisScalarProjections.Max() <= colliderScalarProjections.Min() || thisScalarProjections.Min() >= colliderScalarProjections.Max())
                {
                    collision = false; break;
                }
                //clear lists
                thisScalarProjections.Clear();
                colliderScalarProjections.Clear();
            }
            return collision;
        }

        /*
         * Rotate at certain angle in radials
         */
        public void Rotate(float angle)
        {
            for (int i = 0; i < corners.Length; i++)
            {
                Vector2 p = corners[i] - location;
                float x = p.X;
                float y = p.Y;
                p.X = (float)(x * Math.Cos(angle) - y * Math.Sin(angle));
                p.Y = (float)(x * Math.Sin(angle) + y * Math.Cos(angle));
                corners[i] = location + p;
            }
            rotation = angle;
        }

        /*
         * Move a certain distance
         */
        public void Move(Vector2 destination)
        {
            location += destination;
            for (int i = 0; i < corners.Length; i++) { corners[i] += destination; }
        }

        /*
         * Get
         */
        public string getName() { return name; }
        public Texture2D getTexture() { return texture; }
        public Vector2 getLocation() { return location; }
        public Rectangle getSourceRectangle() { return sourceRectangle; }
        public float getRotation() { return rotation; }

        /*
         * Set
         */
        public void setTexture(Texture2D tex) { texture = tex; }
    }
}