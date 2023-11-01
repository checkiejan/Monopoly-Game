using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class DrawSouth : DrawStrategy
    {
        public override void DrawName(float x, float y, int width, int height, string name)
        {
            float msgWidth = SplashKit.TextWidth(name, "GameFont", _font);
            if (msgWidth > width) // check if the name is wider the width of the box 
            {
                string[] msgline = name.Split(" "); //spllit it with " "
                float y_cord = y + 20; //y coordinate for the first line
                for (int i = 0; i < msgline.Length; i++)
                {
                    float tmp_width = SplashKit.TextWidth(msgline[i], "GameFont", _font); //width of each line
                    SplashKit.DrawText(msgline[i], Color.Black, "GameFont", _font, x + (width - tmp_width) / 2, y_cord);
                    y_cord += SplashKit.TextHeight(name, "GameFont", _font) + 2;
                }
            }
            else // if the name is not wider just draw as normal
            {
                SplashKit.DrawText(name, Color.Black, "GameFont", _font, x + (width - msgWidth) / 2, y + 20);
            }
        }
        public override void DrawColorBox(float x, float y, float width, float height, string color) //draw the color of the property
        {
            SplashKit.FillRectangle(colorValue[color], x+1, y+1, width-2, (height-2)*0.2);
        }
        
    }
}
