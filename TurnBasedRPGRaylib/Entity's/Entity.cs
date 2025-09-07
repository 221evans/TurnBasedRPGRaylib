using System.Numerics;

namespace TurnBasedRPGRaylib.Entity_s;
using Raylib_cs;
public class Entity
{
    protected Rectangle DestRect;
    protected Rectangle SrcRect;
    protected Vector2 Origin;
    protected Texture2D IdleSideTexture;
    protected bool IsInCombat;
    protected int Health;
    protected int PositionX = 250;
    protected int PositionY = 350;
    protected float Rotation = 0;
    protected int Speed = 100;
    protected bool IsWalking;
    
    public Entity()
    {
        IsInCombat = false;
        Health = 50;
        IsWalking = false;
    }

    public virtual void Draw() {}
    public virtual void Update(float deltaTime) {}

    protected virtual void Move(float deltaTime)
    {
        if (!IsInCombat)
        {
            IsWalking = true;
            DestRect.X += -Speed * deltaTime;

            if (DestRect.X >= 600)
            {
                DestRect.X = 600;
                Speed = -Speed;
            }
            else if (DestRect.X <= 0)
            {
                DestRect.X = 0;
                Speed = -Speed;
            }
        }
    }
}