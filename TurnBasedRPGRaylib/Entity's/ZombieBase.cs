namespace TurnBasedRPGRaylib.Entity_s;
using Raylib_cs;

public class ZombieBase : Entity
{
    private Texture2D _walkTexture;
    private Texture2D _currentTexture;
    public int Health { get; set; }
    public int Damage { get; set; }
    AnimationData _animData;

    public ZombieBase()
    {
        _currentTexture = IdleSideTexture;
        IdleSideTexture = Raylib.LoadTexture("Assets/ZombieBase/Idle-Sheet.png");
        _walkTexture = Raylib.LoadTexture("Assets/ZombieBase/Run-Sheet.png");
        PositionX = 450;
        PositionY = 250;
        DestRect = new Rectangle(PositionX, PositionY, 64, 64);
        SrcRect = new Rectangle(0, 0, 64, 64);
        Rotation = 0;
        Speed = 250;
        Health = 50;
        Damage = 10;
        IsInCombat = false;
        IsDead = false;
        IsWalking = false;
        IsFacingLeft = true;
    }

    private void Render(float deltaTime)
    {
        if (IsDead) return;
        
        if (IsWalking)
        {
            _currentTexture = _walkTexture;
            _animData.TotalFrames = 6;
            _animData.FrameDelay = 0.1f;
        } else if (!IsWalking)
        {
            _currentTexture = IdleSideTexture;
            _animData.TotalFrames = 4;
            _animData.FrameDelay = 0.2f;
        }

        if (IsFacingLeft && IsWalking)
        {
            SrcRect.Width = -64;
        }
        else if (!IsFacingLeft && IsWalking)
        {
            SrcRect.Width = 64;
        }
        else if (IsFacingLeft && !IsWalking)
        {
            SrcRect.Width = -64;
        }
        
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
        if (IsDead) return;

        if (Raylib.IsTextureValid(_currentTexture))
        {
            Raylib.DrawTexturePro(_currentTexture, SrcRect, DestRect, Origin, Rotation, Color.White);
        }
    }

    public override void Update(float deltaTime)
    {
        if (IsDead) return;
        Render(deltaTime);
        Move(deltaTime);
        Console.WriteLine($"Zombie Position: {GetPositionX()}, {GetPositionY()}");
        Raylib.DrawRectangle((int)DestRect.X, (int)DestRect.Y, (int)DestRect.Width, (int)DestRect.Height, Color.Purple);
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

    public int TakeDamage(int damage)
    {
        Health -= damage;
        return Health;
    }
    public override void CombatUpdate(float deltaTime)
    {
        if (IsDead) return;
        SetPositionX(470);
        SetPositionY(380);
        Render(deltaTime);
        IsFacingLeft = true;
        IsWalking = false;
    }

   
    
    public void CleanUp()
    {
        Raylib.UnloadTexture(IdleSideTexture);
        Raylib.UnloadTexture(_walkTexture);
    }
}