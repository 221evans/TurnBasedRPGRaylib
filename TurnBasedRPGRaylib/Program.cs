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
        Player player = new Player();
        Boar boar = new Boar();
        Raylib.SetTargetFPS(60);
        
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            // Update game elements here
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);
            Raylib.DrawFPS(10, 10);
            // Draw game elements here
            player.Update(deltaTime);
            boar.Update(deltaTime);
            player.Draw();
            boar.Draw();
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }
}