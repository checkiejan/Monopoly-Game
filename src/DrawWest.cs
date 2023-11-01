using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class DrawWest : DrawStrategy
    {
        public override void DrawName(float x, float y, int width, int height, string name)
        {
            float msgWidth = SplashKit.TextWidth(name, "GameFont", _font);


            if (msgWidth + 25 > width)// check if the name is wider the width of the box 
            {
                string[] msgline = name.Split(" "); // split it with " "
                float y_cord = y + 5; //y coordinate for the first line
                for (int i = 0; i < msgline.Length; i++)
                {
                    SplashKit.DrawText(msgline[i], Color.Black, "GameFont", _font, x + 5, y_cord);
                    y_cord += SplashKit.TextHeight(name, "GameFont", _font) + 2;
                }
            }
            else // if the name is not wider just draw as normal
            {
                SplashKit.DrawText(name, Color.Black, "GameFont", _font, x + 5, y + 5);
            }
        }
        public override void DrawColorBox(float x, float y, float width, float height, string color) //draw the color of the property
        {
            
            SplashKit.FillRectangle(colorValue[color], x + 1 + (width - 2) * 0.8 , y + 1, (width-2)*0.2, height-2);
        }
    }
}
