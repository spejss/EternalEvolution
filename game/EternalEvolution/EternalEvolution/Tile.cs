﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EternalEvolution
{
    public class Tile
    {

        Vector2 position;
        Rectangle sourceRect;
        string state;

        public Rectangle SourceRect
        {
            get
            {
                return sourceRect;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime, ref Player player, ref List<Mob> mobs)
        {
            if (state == "Solid")
            {
                List<Entity> entityList = new List<Entity>();
                //Tile
                Rectangle tileRect = new Rectangle((int)Position.X, (int)Position.Y, sourceRect.Width, sourceRect.Height);

                //Player
                entityList.Add(player);

                //Mobs
                entityList.AddRange(mobs);

                //Console.WriteLine(entityList.Count);
                bool[] hasToPreventMovement = new bool[entityList.Count];
                int i = 0;
                
                foreach (Entity e1 in entityList)
                {
                    if (e1.hitBox.Intersects(tileRect))
                    {
                        PreventCollision(e1.hitBox, tileRect, e1);
                        //Console.WriteLine("collision with Tile prevented");
                    }

                    foreach (Entity e2 in entityList)
                    {
                        if (e1 != e2)
                        {
                            if (e1.hitBox.Intersects(e2.hitBox) && e1.MovesToPosition((int)e2.center.X, (int)e2.center.Y))
                            {
                                //PreventCollision(e1.hitBox, e2.hitBox, e1, e2);
                                //PreventCollision(e2.hitBox, e1.hitBox, e2, e1);
                                //Console.WriteLine("collision with Tile prevented");
                                //PreventCollision(e1.hitBox, e2.hitBox, e1);
                                //PreventCollision(e2.hitBox, e1.hitBox, e2);
                                hasToPreventMovement[i] = true;
                                //e1.ableToMove = false;
                                //e2.ableToMove = false;
                            }
                            else
                            {
                                //hasToPreventMovement[i] = false;
                                //e1.ableToMove = true;
                                //e2.ableToMove = true;
                            }
                        }
                    }
                    if (hasToPreventMovement[i])
                    {
                        e1.ableToMove = false;
                        e1.Velocity = Vector2.Zero;
                        hasToPreventMovement[i] = false;
                    }
                    i++;
                }
                //Console.WriteLine("_______________________");

                /*foreach (Entity e1 in entityList)
                {
                    if (e1.hitBox.Intersects(tileRect))
                    {
                        PreventCollision(e1.hitBox, tileRect, e1);
                    }
                }

                foreach (Mob mob in mobs)
                {
                    if (player.hitBox.Intersects(mob.hitBox))
                    {
                        PreventCollision(player.hitBox, mob.hitBox, player, mob);
                        PreventCollision(mob.hitBox, player.hitBox, mob, player);
                    }
                    else if (player.hitBox.Intersects(mob.agroBox))
                    {
                        mob.playerInRange = true;
                        mob.victimX = (int)player.center.X;
                        mob.victimY = (int)player.center.Y;
                        //Console.WriteLine("position: ", player.center.X, ", ", player.center.Y);
                    }
                    else if (!player.hitBox.Intersects(mob.agroBox))
                    {
                        mob.playerInRange = false;
                    }
                }*/
            }
        }

        private void PreventCollision(Rectangle rec1, Rectangle rec2, Entity e)
        {
            if (e.Velocity.X < 0)
            {
                e.Image.Position.X = rec2.Right;
            }
            else if (e.Velocity.X > 0)
            {
                e.Image.Position.X = rec2.Left - e.Image.SourceRect.Width;
            }
            else if (e.Velocity.Y < 0)
            {
                e.Image.Position.Y = rec2.Bottom;
            }
            else
            {
                e.Image.Position.Y = rec2.Top - e.Image.SourceRect.Height;
            }

            e.Velocity = Vector2.Zero;
        }

        private void PreventCollision(Rectangle rect1, Rectangle rect2, Entity e1, Entity e2)
        {
            e1.Velocity = Vector2.Zero;
            e2.Velocity = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
