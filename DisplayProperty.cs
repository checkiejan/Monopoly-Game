using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class DisplayProperty : DisplayCell //strategy to 
    {
        public DisplayProperty()
        {
            _buttons = new List<Button>();
            Button mortage = new Button(960, 620, "Mortgage", 80, 30); // button of mortage
            mortage.TextSize = 12;
            _buttons.Add(mortage);
            Button redeem = new Button(1060, 620, "Redeem", 80, 30); // button of redeem
            redeem.TextSize = 12;
            _buttons.Add(redeem);
            Button buyHouse = new Button(1010, 660, "Buy House", 80, 30); // button of buy house
            buyHouse.TextSize = 12;
            _buttons.Add(buyHouse);
        }
        public override void Display(ICanBeDisplayed item)
        {
            base.Display(item);
            Cell cell = item as Cell;
            DisplayInfo(cell);
            string name = cell.Name;
            float msgWidth = SplashKit.TextWidth(name, "GameFont", 15);
            SplashKit.DrawText(name, Color.Black, "GameFont", 15, _x + (_width - msgWidth) / 2, _y + 10);
            DrawButton(item as PropertyCell);
            
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                HandleClick(new Point2D() { X = SplashKit.MouseX(), Y = SplashKit.MouseY() }, item as PropertyCell);

            }

        }
        
        public override void DisplayInfo(Cell c)
        {
            PropertyCell cell = c as PropertyCell;
            SplashKit.FillRectangle(colorValue[cell.CellColor], _x + 2, _y + 2, _width - 4, (_height - 4) * 0.2);
            string text = string.Format("Rent    ${0}", cell.TaxData[0]);
            float msgWidth = SplashKit.TextWidth(text, "GameFont", 12);
            SplashKit.DrawText(text, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 70);
            for (int i = 1; i < cell.TaxData.Count; i++) //display the tax with the respective number of house they have
            {
                text = string.Format("With {0} house          ${1}", i, cell.TaxData[i]);
                SplashKit.DrawText(text, Color.Black, "GameFont", 12, _x + 20, _y + 75 + 20 * i);

            }


            string price = string.Format("Price:    ${0}", cell.Price); // price of the property
            msgWidth = SplashKit.TextWidth(price, "GameFont", 12);
            SplashKit.DrawText(price, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 4);



            string mortage = string.Format("Mortage value:    ${0}", cell.MortageValue); //mortage value
            msgWidth = SplashKit.TextWidth(mortage, "GameFont", 12);
            SplashKit.DrawText(mortage, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 5);

            string houseprice = string.Format("Houses cost:    ${0}", cell.HousePrice); //house cost to build
            msgWidth = SplashKit.TextWidth(houseprice, "GameFont", 12);
            SplashKit.DrawText(houseprice, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 6);

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
            SplashKit.DrawText(owner, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 7); //current owner of the property


            string house = string.Format("Houses built    {0}", cell.HouseNumber);
            msgWidth = SplashKit.TextWidth(house, "GameFont", 12);
            SplashKit.DrawText(house, Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + 100 + 20 * 8);
        }
       
        public void DrawButton(PropertyCell cell)
        {

            if (cell.IsOwned && cell.Owner.IsInTurn)
            {
                if (cell.IsMortage && cell.Owner.Money > cell.RedeemPrice) //check if the owner can afford to redeem
                {
                    _buttons[1].Draw(); //redeem button
                }
                else
                {
                    _buttons[0].Draw(); //mortage button
                }
                if (!cell.IsMortage) //just only build house when the property is not mortaged
                {
                    if (cell.HouseAllowed && cell.HouseNumber < cell.TaxData.Count - 1 && cell.Owner.Money > cell.HousePrice)
                    {
                        _buttons[2].Draw();
                    }
                }
            }
        }
        public void HandleClick(Point2D pt, PropertyCell cell)
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
                if (!cell.IsMortage)
                { // check if the property allow to build house and the maximum house and the player can afford it
                    if (cell.HouseAllowed && cell.HouseNumber < cell.TaxData.Count - 1 && cell.Owner.Money > cell.HousePrice) 
                    {
                        if (_buttons[2].IsAt(pt)) //buy house
                        {
                            cell.HouseNumber += 1;
                            cell.Owner.ChangeMoney(-cell.HousePrice);
                        }
                    }
                }
            }
        }
    }
}
