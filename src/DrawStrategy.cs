using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public abstract class DrawStrategy //the strategy that each cell will use to draw based on its direction
    {
        protected int _font = 8; //common font size to draw text
        protected Dictionary<string, Color> colorValue= new Dictionary<string, Color>() // dictionary to store color of the property
        {
            {"blue", Color.CadetBlue },
            {"brown", Color.Brown },
            {"darkBlue", SplashKit.StringToColor("#6497b1") },
            {"green", Color.Green },
            {"orange", Color.Orange },
            {"pink", Color.Orchid },
            {"red", Color.Red },
            {"yellow", Color.Yellow },
        };
        public abstract void DrawName(float x, float y, int width, int height, string name); //draw name of the cell
        public abstract void DrawColorBox(float x, float y, float width, float height, string color);//draw color of property cell
    }
}
