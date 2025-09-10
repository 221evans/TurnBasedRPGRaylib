namespace TurnBasedRPGRaylib.Core;
using Entity_s;
using Raylib_cs;
public class Game
{
    GameState _gameState = GameState.FreeRoam;
    readonly Player _player = new();
    readonly Boar _boar = new();
    readonly ZombieBase _zombieBase = new();
   
  public Game()
  {
  }
  
  public void Draw()
  {
    _player.Draw(); 
    _boar.Draw();
    _zombieBase.Draw();
  }
 public  void Update(float deltaTime)
  {
      
      CheckCollision();
      
      if (_gameState == GameState.FreeRoam)
      {
          _player.Update(deltaTime);
          _boar.Update(deltaTime);
          _zombieBase.Update(deltaTime);
      }
      else if (_gameState == GameState.Combat)
      {
          _player.CombatUpdate(deltaTime);
          _boar.CombatUpdate(deltaTime);
          _zombieBase.CombatUpdate(deltaTime);
          
          if(Raylib.IsKeyPressed(KeyboardKey.X))
          {
              ExitCombat();
          }
          
          if (Raylib.IsKeyPressed(KeyboardKey.One))
          {
              _boar.TakeDamage(_player.Damage);
              Console.WriteLine($"Boar Health: {_boar.Health}");
              _zombieBase.TakeDamage(_player.Damage);
              Console.WriteLine($"Zombie Health: {_zombieBase.Health}");
          }

          if (_boar.Health <= 0 && _zombieBase.Health <= 0)
          {
              _gameState = GameState.FreeRoam;
              _boar.IsDead = true;
              
          }

          if (_zombieBase.Health <= 0)
          {
              _zombieBase.IsDead = true;
          }
      }
  }
 //Debug method
 private void ExitCombat()
 {
    _gameState = GameState.FreeRoam;
 }

 private void CheckCollision()
 {
     if (_boar.IsDead && _zombieBase.IsDead) return;
     
     if (Raylib.CheckCollisionRecs(_player.DestRect, _boar.DestRect))
     {
         _gameState = GameState.Combat;
     }

     if (Raylib.CheckCollisionRecs(_player.DestRect, _zombieBase.DestRect))
     {
         _gameState = GameState.Combat;
     }
 }
 public void CleanUp()
 {
     Raylib.CloseWindow();
     _player.CleanUp();
     _boar.CleanUp();
 }
}