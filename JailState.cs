using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class JailState: State
    {
        public JailState(NoticeBox nb) : base(nb)
        {

            Button yes = new Button(400, 400, "Yes", 60, 40);
            AddButton(yes);
            Button no = new Button(550, 400, "No", 60, 40);
            AddButton(no);
            Player p = Notice.CurrentPlayer();
            p.TurnInJail += 1;
            if(p.TurnInJail > 3) // if player finishes jail, release him
            {
                p.GetOutJail();
                Notice.ChangeState(new RollState(Notice)); //change to roll state
            }
        }
        public override void Draw()
        {
            Player p = Notice.CurrentPlayer();
            string text = string.Format("Do you want to pay $200 to get out of Jail");
            float msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 60);
            
            if(p.TurnInJail < 3)
            {
                text = string.Format("You have {0} turn left to get out of Jail", 4 - p.TurnInJail); //display how many turn left to get out of jail
            }
            else
            {
                text = "You will get out in the next turn";
            }
            msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 80);
            DrawButton();
        }

        public override void ClickButton(Point2D pt)
        {
            Player p = Notice.CurrentPlayer();
            if (_buttons[0].IsAt(pt)) //yes, agree to pay money to get out of jail
            {
                p.ChangeMoney(-200);
                p.GetOutJail();
                Notice.ChangeState(new RollState(Notice));
            }
            else if (_buttons[1].IsAt(pt)) //no
            {
                Notice.ChangeState(new ReadyState(Notice));
                if(p.TurnInJail == 3) // if finish release him
                {
                    p.GetOutJail();
                }
            }
        }
    }
}
