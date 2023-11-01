using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class DisplayInfo
    {
        ICanBeDisplayed _item;
        DisplayStrategy _strategy;
        public DisplayInfo()
        {
            _item = null;
        }
        public void Draw()
        {
            SplashKit.DrawText("Click cell or player to see information", Color.Black, "GameFont",15, 900,250);
            if (_item != null) //if the item needed to display is not nulll then display
            {
                _strategy.Display(_item); 
            }
        }
        public void AddDisplay(ICanBeDisplayed display) //Add item to display its information
        {
            _item = display;
        }
        public void StrategyDisplay(DisplayStrategy strategy) //to change the strategy of display
        {
            _strategy = strategy;
        }
    }
}
