using System.Numerics;

namespace TurnBasedRPGRaylib.Entity_s;

using Raylib_cs;


public class Player : Entity
{
    
    public Player()
    {
        PositionX = 250;
        PositionY = 350;
        Origin = new Vector2(64, 64);
        IdleSideTexture = Raylib.LoadTexture("Assets/Player/Idle-Side-Sheet.png");
        DestRect = new Rectangle(PositionX, PositionY, 64, 64);
        SrcRect = new Rectangle(0, 0, 64, 64);
        Rotation = 0;
        Speed = 250;
        IsInCombat = false;
        Health = 100;
    }
    
    public override void Update(float deltaTime)
    {
        Move(deltaTime);
        Console.WriteLine($"Player Position: {DestRect.X}, {DestRect.Y}");
    }
   protected override void Move(float deltaTime)
    {

        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            DestRect.X += Speed * deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            DestRect.X -= Speed * deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            DestRect.Y -= Speed * deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            DestRect.Y += Speed * deltaTime;
        }
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
}