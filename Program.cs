using System;
using System.Collections.Generic;
using System.IO;
using Custom;
using SplashKitSDK;

public class Program
{
   
    public static void Main()
    {
       
    
        Window window = new Window("Shape Drawer", 1300, 700);
        GameManger game = new GameManger();
    
        do
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                game.HandleInput(new Point2D() { X=SplashKit.MouseX(), Y= SplashKit.MouseY() });
               
            }
            game.Update();
            game.Draw();
           
            SplashKit.RefreshScreen();
        } while (!window.CloseRequested);
      
    }
}
