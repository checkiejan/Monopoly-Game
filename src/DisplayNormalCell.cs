using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{ 
    public class DisplayNormalCell : DisplayCell //this is to display cell that does not require any specific information just require the description of the cell
    { 
        public override void Display(ICanBeDisplayed item)
        {
            base.Display(item); // to draw the box
            Cell cell = item as Cell;
            DisplayInfo(cell); //just display the basic info
            string name = cell.Name;
            float msgWidth = SplashKit.TextWidth(name, "GameFont", 15);
            SplashKit.DrawText(name, Color.Black, "GameFont", 15, _x + (_width - msgWidth) / 2, _y + 10); //draw name
        }
        public override void DisplayInfo(Cell c)
        {
            string[] msgLines = c.Description.Split('\n'); // Draw text by splitting into lines
            for (int i = 0; i < msgLines.Length; i++) //draw each line
            {
                float msgWidth = SplashKit.TextWidth(msgLines[i], "GameFont", 12);
                float msgHeight = SplashKit.TextHeight(msgLines[i], "GameFont", 12);
                SplashKit.DrawText(msgLines[i], Color.Black, "GameFont", 12, _x + (_width - msgWidth) / 2, _y + msgHeight * i + 70); //
            }
        } 
    } 
}
