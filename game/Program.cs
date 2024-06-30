using libs;

using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;

<<<<<<< HEAD
        engine.MainMenu();

=======
>>>>>>> MenuPlusDialog
        // Main game loop
        while (true)
        {
            if(engine.gameHasStarted){
                // Handle keyboard input
                if(Console.KeyAvailable){
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    inputHandler.Handle(keyInfo);
                }
                engine.Render();

                //game logic updates or delays to reduce cpu usage
                Thread.Sleep(150);
            } else {
                Console.Clear();
                engine.MainMenu();
            }

        }
    }
}