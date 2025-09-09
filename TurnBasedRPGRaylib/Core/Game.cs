namespace TurnBasedRPGRaylib.Core;
using Entity_s;
using Raylib_cs;
public class Game
{
    GameState _gameState = GameState.FreeRoam;
    readonly Player _player = new();
    readonly Boar _boar = new();
    
  public Game()
  {
      
  }
  
  public void Draw()
  {
    _player.Draw(); 
    _boar.Draw();
  }
 public  void Update(float deltaTime)
  {
      _player.CollisionRect = new Rectangle(_player.GetPositionX(), _player.GetPositionY(),_player.DestRect.Width , _player.DestRect.Height);
      _boar.CollisionRect = new Rectangle(_boar.GetPositionX(), _boar.GetPositionY(), 96, 64);
      
      Raylib.DrawRectangle((int)_player.CollisionRect.X, (int)_player.CollisionRect.Y, (int)_player.CollisionRect.Width, (int)_player.CollisionRect.Height, Color.Red);
      Raylib.DrawRectangle((int)_boar.CollisionRect.X, (int)_boar.CollisionRect.Y, (int)_boar.CollisionRect.Width, (int)_boar.CollisionRect.Height, Color.Blue);
      if (_gameState == GameState.FreeRoam)
      {
          _player.Update(deltaTime);
          _boar.Update(deltaTime);

          if (Raylib.CheckCollisionRecs(_player.CollisionRect, _boar.CollisionRect))
          {
              _gameState = GameState.Combat;
          }
          
        
      }
      else if (_gameState == GameState.Combat)
      {
          _player.CombatUpdate(deltaTime);
          _boar.CombatUpdate(deltaTime);
          
          if(Raylib.IsKeyPressed(KeyboardKey.X))
          {
              ExitCombat();
          }
      }
  }
 //Debug method
 private void ExitCombat()
 {
    _gameState = GameState.FreeRoam;
 }
 
 public void CleanUp()
 {
     Raylib.CloseWindow();
     _player.CleanUp();
     _boar.CleanUp();
 }
}