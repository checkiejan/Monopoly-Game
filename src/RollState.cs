using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class RollState: State
    {
       
        public RollState(NoticeBox nb): base(nb)
        {           
            Button roll = new Button(470, 400,"Roll", 60, 40);
            AddButton(roll);
        }
        public override void Draw()
        {
            
            Notice.DrawDice();
            string text = "Please roll to move";
            float msgWidth = SplashKit.TextWidth(text, "GameFont",15 );
            SplashKit.DrawText(text, Color.Black, "GameFont", 15, 280 + (420 - msgWidth) / 2, 80 + 40);
            DrawButton();
        }
        public override void ClickButton(Point2D pt)
        {
            if (_buttons[0].IsAt(pt))
            {
                Notice.RollDice();
                NoticeBox nb = Notice;
                nb.MoveCurrentPlayer(nb.DiceResult);
                Cell c = nb.CurrentPlayerCell();
                
                Player p = nb.CurrentPlayer();
                if (c is OwnableCell)
                {
                    OwnableCell oCell = (OwnableCell)c;
                   
                    if (oCell.IsOwned)
                    {
                        if(p.Money >= 0) // aftex being taxed, can continue or have to mortage
                        {
                            nb.ChangeState(new ReadyState(Notice));
                        }
                        else if(p.IsBankrupt) // end for user
                        {
                            nb.ChangeState(new BankruptState(Notice));
                        }
                        else //mortage state
                        {
                            nb.ChangeState(new MortageState(Notice));
                        }
                    } 
                    else
                    {
                        if (p.Money >= oCell.Price)
                        {
                            nb.ChangeState(new BuyState(Notice));
                        }
                        else
                        {
                            nb.ChangeState(new ReadyState(Notice));
                        } 
                       
                    } 
                }
                else if(c is TaxCell)
                {
                    if (p.Money >= 0) // aftex being taxed, can continue or have to mortage
                    {
                        nb.ChangeState(new ReadyState(Notice)); //keep on
                    }
                    else if (p.IsBankrupt) // end for user
                    {
                        nb.ChangeState(new BankruptState(Notice));
                    }
                    else //mortage state
                    {
                        nb.ChangeState(new MortageState(Notice));
                    }
                }
                else // continue
                {
                   nb.ChangeState(new ReadyState(Notice));
                }
            }
        }
    }
}
