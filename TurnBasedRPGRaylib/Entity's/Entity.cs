using System.Numerics;

namespace TurnBasedRPGRaylib.Entity_s;
using Raylib_cs;
public abstract class Entity
{
    public Rectangle DestRect;
    protected Rectangle SrcRect;
    protected Vector2 Origin;
    protected Texture2D IdleSideTexture;
    protected bool IsInCombat;
    protected int PositionX;
    protected int PositionY;
    protected float Rotation;
    protected int Speed;
    protected bool IsWalking;
    protected bool IsFacingLeft;
    public bool IsDead;
    
    protected Entity()
    {
        IsInCombat = false;
        IsWalking = false;
        IsFacingLeft = true;
        PositionX = 250;
        PositionY = 350;
        Rotation = 0;
        Speed = 100;
        IsDead = false;
    }

    public abstract void Draw();
    public abstract void Update(float deltaTime);

    public virtual float GetPositionX()
    {
        return DestRect.X;
    }
    public virtual float GetPositionY()
    {
        return DestRect.Y;
    }
    public abstract void CombatUpdate(float deltaTime);
    
    public abstract float SetPositionX(float x);
    public abstract float SetPositionY(float y);
    
    
    
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
                IsFacingLeft = true;
            }
            else if (DestRect.X <= 0)
            {
                DestRect.X = 0;
                Speed = -Speed;
                IsFacingLeft = false;
            }
        }
    }
}