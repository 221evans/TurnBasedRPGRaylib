namespace TurnBasedRPGRaylib.Core;
using Entity_s;
using Raylib_cs;
public class Game
{
    
    GameState _gameState = GameState.FreeRoam;
  Player _player = new Player();
  Boar _boar = new Boar();
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
      if (_gameState == GameState.FreeRoam)
      {
          _player.Update(deltaTime);
          _boar.Update(deltaTime);
      }
      else if (_gameState == GameState.Combat)
      {
          
      }
   
  }
}