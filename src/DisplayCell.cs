using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public abstract class DisplayCell: DisplayStrategy //strategy to display all types of cell
    {

        protected List<Button> _buttons;
        public DisplayCell()
        {
            _buttons = new List<Button>();
            _x = 950;
            _y = 280;
            _width = 200;
            _height = 320;
    }
        public override void Display(ICanBeDisplayed item)
        {
            SplashKit.FillRectangle(Color.Black, _x, _y, _width, _height); //draw the outline
            SplashKit.FillRectangle(Color.White, _x + 2, _y + 2, _width - 4, _height - 4); //draw the box
        }
        public virtual void DisplayInfo(Cell c)
        {

        }
    }
}
