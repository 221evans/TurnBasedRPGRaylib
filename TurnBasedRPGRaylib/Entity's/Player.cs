using System.Numerics;

namespace TurnBasedRPGRaylib.Entity_s;

using Raylib_cs;


public class Player : Entity
{

    private bool _isRunningSide;
    private bool _isFacingUp;
    private bool _isFacingDown;
    private bool _isFacingLeft;
    private bool _isIdle;
    public int Damage{get;set;}
    private Texture2D _currentTexture;
    private Texture2D _runUpTexture;
    private Texture2D _runSideTexture;
    private Texture2D _runDownTexture;
    private AnimationData _animData;
    
    

    public Player()
    {
        PositionX = 250;
        PositionY = 350;
        Origin = Vector2.Zero;
        IdleSideTexture = Raylib.LoadTexture("Assets/Player/Idle-Side-Sheet.png");
        _currentTexture = IdleSideTexture;
        _runUpTexture = Raylib.LoadTexture("Assets/Player/Run-Up-Sheet.png");
        _runDownTexture = Raylib.LoadTexture("Assets/Player/Run-Down-Sheet.png");
        _runSideTexture = Raylib.LoadTexture("Assets/Player/Run-Side-Sheet.png");
        DestRect = new Rectangle(PositionX, PositionY, 64, 64);
        SrcRect = new Rectangle(0, 0, 64, 64);
        Damage = 10;
        Rotation = 0;
        Speed = 250;


        IsInCombat = false;
        
       
        _isRunningSide = false;
        _isFacingUp = false;
        _isFacingDown = false;
        _isFacingLeft = false;
        _isIdle = false;
    }

    private void Render(float deltaTime)
    { 
        
        if (_isFacingLeft)
        {
            SrcRect.Width = -64;
            SrcRect.X = (_animData.FrameIndex + 1) * 64; 
        }
        else if (!_isFacingLeft)
        {
            SrcRect.Width = 64;
        }
        
        if (_isRunningSide)
        {
            _currentTexture = _runSideTexture;
            _animData.TotalFrames = 6;
            _animData.FrameDelay = 0.1f;
        }
        
        else if (_isFacingUp)
        {
            _currentTexture = _runUpTexture;
            _animData.TotalFrames = 6;
        }
        else if (_isFacingDown)
        {
            _currentTexture = _runDownTexture;
            _animData.TotalFrames = 6;
        }

        else if (_isIdle)
        {
            _currentTexture = IdleSideTexture;
            _animData.TotalFrames = 4;
            _animData.FrameDelay = 0.2f;
        }
        
        // Safety: avoid modulo/div-by-zero
        if (_animData.TotalFrames <= 0) _animData.TotalFrames = 1;
        if (_animData.FrameDelay <= 0f) _animData.FrameDelay = 0.10f;

        
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

    public override void Update(float deltaTime)
    {
        Move(deltaTime);
        Console.WriteLine($"Player Position: {GetPositionX()}, {GetPositionY()}");
        
    }

    public override void CombatUpdate(float deltaTime)
    {
        SetPositionX(150);
        SetPositionY(260);
       // Console.WriteLine($"Player Position: {DestRect.X}, {DestRect.Y}");
        Render(deltaTime);
        _isIdle = true;
        _isRunningSide = false;
        _isFacingLeft = false;
        _isFacingUp = false;
        _isFacingDown = false;
        _currentTexture = IdleSideTexture;
        _isIdle = false;
        
    }

    protected override void Move(float deltaTime)
    {
        
        Render(deltaTime);
        _isRunningSide = false;
        
        
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            DestRect.X += Speed * deltaTime;
            _isRunningSide = true;
            _isFacingLeft = false;
            _isFacingUp = false;
            _isFacingDown = false;
            _isIdle = false;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            DestRect.X -= Speed * deltaTime;
            _isRunningSide = true;
            _isFacingLeft = true;
            _isFacingUp = false;
            _isFacingDown = false;
            _isIdle = false;
        }
      

        else if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            DestRect.Y -= Speed * deltaTime;
            _isFacingUp = true;
            _isRunningSide = false;
            _isFacingLeft = false;
            _isIdle = false;
        }

        else if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            DestRect.Y += Speed * deltaTime;
            _isFacingDown = true;
            _isRunningSide = false;
            _isFacingLeft = false;
            _isIdle = false;
        }
        else
        {
            _isRunningSide = false;
            _isFacingLeft = false;
            _isFacingUp = false;
            _isFacingDown = false;
            _isIdle = true;
        }
    }

    public override void Draw()
    {
        Raylib.DrawTexturePro(_currentTexture, SrcRect, DestRect, Origin, Rotation, Color.White);
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