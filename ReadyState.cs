using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class ReadyState : State
    {
       
        public ReadyState(NoticeBox nb) : base(nb)
        {
           
            Button ready = new Button(470, 400, "OK", 60, 40);
            AddButton(ready);
        }
        public override void Draw()
        {
            Notice.DrawDice();
            string text = "This is the end of your move";
            float msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 40);
            text = "Please pass to the next person";
            msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 60);
            DrawButton();
        }
       
        public override void ClickButton(Point2D pt)
        {
            if (_buttons[0].IsAt(pt))
            {
                Notice.NextPlayer();
                Player p = Notice.CurrentPlayer();
                if (p.IsInJail)
                {
                    Notice.ChangeState(new JailState(Notice));
                    
                }
                else
                {
                    Notice.ChangeState(new RollState(Notice));

                }

            }
        }
    }
}
