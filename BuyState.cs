using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class BuyState : State
    {
        public BuyState(NoticeBox nb) : base(nb)
        {

            Button yes = new Button(400, 400, "Yes", 60, 40);
            AddButton(yes);
            Button no = new Button(550, 400, "No", 60, 40);
            AddButton(no);
        }
        public override void Draw()
        {
            Cell c = Notice.CurrentPlayerCell(); //get the cell of the current player 
            OwnableCell oC = (OwnableCell)c;
            string text = string.Format("Do you want to buy {0}",oC.Name);
           
            float msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 60);
            text = string.Format("with the price ${0}",oC.Price);
            msgWidth = SplashKit.TextWidth(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 80);

            DrawButton();
        }

        public override void ClickButton(Point2D pt)
        {
            if (_buttons[0].IsAt(pt)) //yes
            {
                OwnableCell c = Notice.CurrentPlayerCell() as OwnableCell; //get the cell of the current player
                Player current = Notice.CurrentPlayer(); 
              
                current.BuyProperty(c); //buy the property
  
                Notice.ChangeState(new ReadyState(Notice)); //change the state to ready state
            }
            else if (_buttons[1].IsAt(pt)) //no
            {
                Notice.ChangeState(new ReadyState(Notice)); // change the state to ready state
            }
        }
    }
}
