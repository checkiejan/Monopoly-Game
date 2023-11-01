using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class DisplayStation : DisplayCell
    {
        public DisplayStation()
        {
            _buttons = new List<Button>();
            Button mortage = new Button(960, 620, "Mortgage", 80, 30); // button of mortage
            mortage.TextSize = 12;
            _buttons.Add(mortage);
            Button redeem = new Button(1060, 620, "Redeem", 80, 30); // button of redeem
            redeem.TextSize = 12;
            _buttons.Add(redeem);

        }
        public override void Display(ICanBeDisplayed item)
        {
            base.Display(item);
            Cell cell = item as Cell;
            string name = cell.Name;
            float msgWidth = SplashKit.TextWidth(name, "GameFont", 15);
            SplashKit.DrawText(name, Color.Black, "GameFont", 15, _x + (_width - msgWidth) / 2, _y + 10);
            DisplayInfo(cell);

            DrawButton(item as StationCell);

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                HandleClick(new Point2D() { X = SplashKit.MouseX(), Y = SplashKit.MouseY() }, item as StationCell);

            }
        }
        public void DisplayInfo(Cell c)
        {
            StationCell cell = c as StationCell;
            float msgWidth;
            for (int i = 0; i < cell.TaxData.Count; i++) // display all tax information of the station
            {
                string text = string.Format("if {0} owned          ${1}", i, cell.TaxData[i]);
                msgWidth = SplashKit.TextWidth(text, "GameFont", 12);
                SplashKit.DrawText(text, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 75 + 20 * i);
            }

            string price = string.Format("Price:    ${0}", cell.Price); //price of the station
            msgWidth = SplashKit.TextWidth(price, "GameFont", 12);
            SplashKit.DrawText(price, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 4);



            string mortage = string.Format("Mortage value:    ${0}", cell.MortageValue); //mortage value
            msgWidth = SplashKit.TextWidth(mortage, "GameFont", 12);
            SplashKit.DrawText(mortage, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 5);

            string owner;
            if (cell.IsOwned)
            {
                owner = string.Format("Owned by:  {0}", cell.Owner.Name);
            }
            else
            {
                owner = "Owned by none";
            }
            msgWidth = SplashKit.TextWidth(owner, "GameFont", 12);
            SplashKit.DrawText(owner, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 6); //current owner
        }
        public void DrawButton(StationCell cell)
        {

            if (cell.IsOwned && cell.Owner.IsInTurn)
            {
                if (cell.IsMortage && cell.Owner.Money > cell.RedeemPrice) //check if the owner can afford to redeem
                {
                    _buttons[1].Draw(); //redeem button
                }
                else
                {
                    _buttons[0].Draw(); //mortage
                }
            }
        }

        public void HandleClick(Point2D pt, StationCell cell)
        {
            if (cell.IsOwned && cell.Owner.IsInTurn)
            {
                if (cell.IsMortage && cell.Owner.Money > cell.RedeemPrice) //check if the owner can afford to redeem
                {
                    if (_buttons[1].IsAt(pt)) //redeem
                    {
                        cell.IsMortage = false;
                        cell.Owner.ChangeMoney(-cell.RedeemPrice);
                    }
                }
                else
                {
                    if (_buttons[0].IsAt(pt)) //mortage
                    {
                        cell.IsMortage = true;
                        cell.Owner.ChangeMoney(cell.MortageValue);
                    }
                }
            }
        }
    }
}
