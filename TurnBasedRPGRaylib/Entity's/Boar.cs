namespace TurnBasedRPGRaylib.Entity_s;
using Raylib_cs;
public class Boar : Entity
{
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
    }
    
}