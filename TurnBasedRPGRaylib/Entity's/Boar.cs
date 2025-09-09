namespace TurnBasedRPGRaylib.Entity_s;
using Raylib_cs;
public class Boar : Entity
{
    
    public Rectangle CollisionRect;
    public Boar()
    {
        IdleSideTexture = Raylib.LoadTexture("Assets/Boar/Idle-Sheet.png");
        PositionX = 350;
        PositionY = 250;
        DestRect = new Rectangle(PositionX, PositionY, 96, 64);
        SrcRect = new Rectangle(0, 0, 96, 64);
        Rotation = 0;
        Speed = 250;
        IsInCombat = false;
        Health = 100;
        CollisionRect = new Rectangle(PositionX, PositionY, 100, 100);
    }

    public override void Draw()
    {
        if (Raylib.IsTextureValid(IdleSideTexture))
        {
            Raylib.DrawTexturePro(IdleSideTexture, SrcRect, DestRect,Origin, Rotation, Color.White);
        }
        else
        { 
            throw new Exception("Texture not valid");
        }
    }
    
    public override void Update(float deltaTime)
    {
        Move(deltaTime);
        Console.WriteLine($"Boar Position: {GetPositionX()}, {GetPositionY()}");
    }

    public override void CombatUpdate(float deltaTime)
    {
        SetPositionX(450);
        SetPositionY(280);
        Console.WriteLine($"Boar Position: {DestRect.X}, {DestRect.Y}");
    }
    
    public override float SetPositionX(float x)
    {
        DestRect.X = x;
        return x;
    }
    
    public override float SetPositionY(float y)
    {
        DestRect.Y = y;
        return y;
    }
    
    public void CleanUp()
    {
        Raylib.UnloadTexture(IdleSideTexture);
    }
}