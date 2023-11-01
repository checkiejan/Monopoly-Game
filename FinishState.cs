using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class FinishState : State // the state to notify the game has ended and the winner of the game
    {
        public FinishState(NoticeBox nb) : base(nb)
        {

           
        }
        public override void Draw()
        {
            string text = "Game Over!";
            float msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 40);
            text = String.Format("Congratulation! {0}, you are the winner",Notice.CurrentPlayer().Name); //notice which playe wins
            msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 60);
           
        }

        public override void ClickButton(Point2D pt)
        {
            
        }
    }
}
