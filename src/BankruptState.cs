using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class BankruptState: State
    {
        public BankruptState(NoticeBox nb) : base(nb)
        {

            Button ready = new Button(470, 400, "OK", 60, 40); //button to switch to ready state
            AddButton(ready);
        }
        public override void Draw() //print the notice that the current player is bankrupt
        {
           
            string text = "You are bankrupt!"; 
            float msgWidth = SplashKit.TextWidth(text, "GameFont", 15); // the width of the text
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 40); //draw the text at middle of the board
            text = "Game Over";
            msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 60);//draw the text at middle of the board
            DrawButton();
        }

        public override void ClickButton(Point2D pt)
        {
            if (_buttons[0].IsAt(pt)) // check if the button is clicked
            {
                Notice.NextPlayer(); //after the player get noticed change the player
                Notice.ChangeState(new RollState(Notice));

            }
        }
    }
}
