using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window game = new Window("Robot Dodge", 800, 600); //creating a new window 600 by 600 pixels
        RobotDodge robotDodge = new RobotDodge(game);
        
        //running the game till user closes the window or presses ESC key
        while(!game.CloseRequested && robotDodge.Quit != true) 
        {
            SplashKit.ProcessEvents(); //calling procedure for processing events
            robotDodge.HandleInput();
            robotDodge.Update();
            robotDodge.Draw();    
        }
        game.Close();                //closing the window once the user wants to quit
    }
}
