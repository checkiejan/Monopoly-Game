using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Custom
{
    public class DisplayPlayer: DisplayStrategy
    {
       
        public DisplayPlayer()
        {
            _x = 850;
            _y = 300;
            _width = 430;
            _height = 230;
        }
        public override void Display(ICanBeDisplayed item)
        {
            SplashKit.FillRectangle(Color.Black, _x, _y, _width, _height);
            SplashKit.FillRectangle(Color.White, _x + 2, _y + 2, _width - 4, _height - 4);
            SplashKit.DrawText(item.Description, Color.Black, "GameFont", 15, _x +10, _y + 10); //draw the description of the player
            DisplayProperty(item as Player); // display all the property that the player has
        }
        public void DisplayProperty(Player p)
        {
            List<OwnableCell> list = p.Property;
            int x = _x + 15;
            int width = 30;
            int height = 40;
            int y = _y + (_height -height)/2;
            foreach (OwnableCell cell in list) //take the name and color of a cell to draw a rectangle and each box is next to each other
            {
                char s = cell.Name[0]; //just draw the first character of the property
                SplashKit.FillRectangle(Color.Black, x, y, width, height);
                if (cell is StationCell) // station cell has white color background
                {
                    SplashKit.FillRectangle(Color.White, x + 2, y + 2, width - 4, height - 4); 
                }
                else
                {
                    SplashKit.FillRectangle(colorValue[(cell as PropertyCell).CellColor], x + 2, y + 2, width - 4, height - 4); //draw the color background
                }
                
                float msgWidth = SplashKit.TextWidth(s.ToString(), "GameFont", 15);
                float msgHeight = SplashKit.TextHeight(s.ToString(), "GameFont", 15);
                SplashKit.DrawText(s.ToString(), Color.Black, "GameFont", 15, x +(width - msgWidth)/2, y + (height - msgHeight)/2); //draw the character in the middile of the box
                x += width; //increase to the box
            }
        }
    }
}
