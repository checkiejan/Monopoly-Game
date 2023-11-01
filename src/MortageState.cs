using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class MortageState: State
    {
        public MortageState(NoticeBox nb) : base(nb)
        {

           
        }
        public override void Draw() //force the player to mortage his properties to continue
        {
            Player p = Notice.CurrentPlayer();
            if (p.Money >= 0) //if finish change to ready state
            {
                Notice.ChangeState(new ReadyState(Notice));
            }
            else
            {
                string text = string.Format("You still owe ${0}", -p.Money);
                float msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
                SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 40);
                text = "Please mortage your property to continue";
                msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
                SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 60);
            }
          
        }

        public override void ClickButton(Point2D pt)
        {
            
        }
    }
}
