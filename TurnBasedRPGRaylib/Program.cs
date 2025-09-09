using TurnBasedRPGRaylib.Core;
using TurnBasedRPGRaylib.Entity_s;
namespace TurnBasedRPGRaylib;

using Raylib_cs;

class Program
{
 
    static void Main(string[] args)
    {
      
       
        Console.WriteLine($"WorkingDir: {Environment.CurrentDirectory}");

        const int screenWidth = 800;
        const int screenHeight = 450;
        Raylib.InitWindow(screenWidth, screenHeight, "Turn Based RPG");
        Raylib.SetTargetFPS(60);
        Game game = new Game();
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            game.Update(deltaTime);
            // Update game elements here
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);
            Raylib.DrawFPS(10, 10);
            // Draw game elements here
            game.Draw();
            Raylib.EndDrawing();
        }
        
        game.CleanUp();
    }
}