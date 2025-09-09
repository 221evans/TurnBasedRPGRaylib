namespace TurnBasedRPGRaylib.Entity_s;
using Raylib_cs;
public class Boar : Entity
{

    private Texture2D _walkTexture;
    private Texture2D _currentTexture;
    private bool _isFacingLeft;
    private bool _isWalking;
    private bool _isIdle;
    public Rectangle CollisionRect;
    AnimationData _animData;
    public Boar()
    {
        _currentTexture = IdleSideTexture;
        IdleSideTexture = Raylib.LoadTexture("Assets/Boar/Idle-Sheet.png");
        _walkTexture = Raylib.LoadTexture("Assets/Boar/Walk-Sheet.png");
        PositionX = 350;
        PositionY = 250;
        DestRect = new Rectangle(PositionX, PositionY, 96, 64);
        SrcRect = new Rectangle(0, 0, 96, 64);
        Rotation = 0;
        Speed = 250;
        IsInCombat = false;
        Health = 100;
        CollisionRect = new Rectangle(PositionX, PositionY, 100, 100);
        _isWalking = false;
    }

    private void Render(float deltaTime)
    {
        if (IsWalking)
        {
            _currentTexture = _walkTexture;
            _animData.TotalFrames = 8;
            _animData.FrameDelay = 0.1f;
             SrcRect.Width = 80;
             SrcRect.Height = 64;
        }
        else if (!IsWalking)
        {
            _currentTexture = IdleSideTexture;
            _animData.TotalFrames = 4;
            _animData.FrameDelay = 0.2f;
            SrcRect.Width = 96;
            SrcRect.Height = 64;
        }

        if (IsFacingLeft && IsWalking)
        {
            SrcRect.Width = 80;
            
        }
        else if (!IsFacingLeft && IsWalking)
        {
            SrcRect.Width = -80;
        }
      
        
        // Animate
        Animate(deltaTime);
    }

    private void Animate(float deltaTime)
    {
        _animData.FrameCounter += deltaTime;
        if (_animData.FrameCounter >= _animData.FrameDelay)
        {
            _animData.FrameCounter -= _animData.FrameDelay;
            _animData.FrameIndex = (_animData.FrameIndex + 1) % _animData.TotalFrames;
            SrcRect.X = _animData.FrameIndex * SrcRect.Width;
        }
    }

    public override void Draw()
    {
        if (Raylib.IsTextureValid(IdleSideTexture))
        {
            Raylib.DrawTexturePro(_currentTexture, SrcRect, DestRect,Origin, Rotation, Color.White);
        }
        else
        { 
            throw new Exception("Texture not valid");
        }
    }
    
    public override void Update(float deltaTime)
    {
        Render(deltaTime);
        Move(deltaTime);
        Console.WriteLine($"Boar Position: {GetPositionX()}, {GetPositionY()}");
        
    }

    public override void CombatUpdate(float deltaTime)
    {
        SetPositionX(450);
        SetPositionY(280);
        Console.WriteLine($"Boar Position: {DestRect.X}, {DestRect.Y}");
        Render(deltaTime);
        IsFacingLeft = true;
        IsWalking = false;
        SrcRect.Width = 96;
        SrcRect.Height = 64;
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