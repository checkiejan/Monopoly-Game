using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public abstract class DisplayStrategy //strategy to display the information
    {
        protected int _x;
        protected int _y;
        protected int _width;
        protected int _height;
        protected Dictionary<string, Color> colorValue = new Dictionary<string, Color>() // dictionary to store color of the property
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
        public abstract void Display(ICanBeDisplayed item); 
    }
}
