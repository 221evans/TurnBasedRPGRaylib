namespace TurnBasedRPGRaylib.Core;
using Entity_s;
using Raylib_cs;
public class Game
{
    GameState _gameState = GameState.FreeRoam;
    readonly Player _player = new();
    readonly Boar _boar = new();
    private int _playerDamage;
  public Game()
  {
      _playerDamage = _player.Damage;
  }
  
  public void Draw()
  {
    _player.Draw(); 
    _boar.Draw();
    
  }
 public  void Update(float deltaTime)
  {
      Raylib.DrawRectangle((int)_player.DestRect.X, (int)_player.DestRect.Y, (int)_player.DestRect.Width, (int)_player.DestRect.Height, Color.Red);
      Raylib.DrawRectangle((int)_boar.DestRect.X, (int)_boar.DestRect.Y, (int)_boar.DestRect.Width, (int)_boar.DestRect.Height, Color.Blue);
      if (_gameState == GameState.FreeRoam)
      {
          _player.Update(deltaTime);
          _boar.Update(deltaTime);

          if (Raylib.CheckCollisionRecs(_player.DestRect, _boar.DestRect))
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
          
          if (Raylib.IsKeyPressed(KeyboardKey.One))
          {
              _boar.TakeDamage(_playerDamage);
              Console.WriteLine($"Boar Health: {_boar.Health}");
          }

          if (_boar.Health <= 0)
          {
              _gameState = GameState.FreeRoam;
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