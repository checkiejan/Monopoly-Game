using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class Button: DrawObject
    {
        private int _width, _height;
        private Color _color, _colorOnHover, _textColor;
        private int _textSize;
        public Button(float x, float y, string name, int width, int height) : base(x, y, name) 
        {
            _textSize = 15; //font size for the text
            _width = width;
            _height = height;
            _textColor = Color.Black;
            _colorOnHover = Color.AntiqueWhite; //color when the button is being hovered
            _color = Color.Aqua;
        }
        public override void Draw()
        {
            if (IsAt(SplashKit.MousePosition()))
                SplashKit.FillRectangle(_colorOnHover, X, Y, _width, _height); //when the button is being hovered
            else
                SplashKit.FillRectangle(_color, X, Y, _width, _height);
            DrawText();
        }
        private void DrawText()
        {
            float textX = X + (_width - SplashKit.TextWidth(Name, "GameFont", _textSize)) / 2;
            float textY = Y + (_height - SplashKit.TextHeight(Name, "GameFont", _textSize)) / 2;
            SplashKit.DrawText(Name, _textColor, "GameFont", _textSize, textX, textY);
        }
        public int TextSize //allow to change the font size
        {
            get
            {
                return _textSize;
            }
            set
            {
                _textSize = value;
            }
        }
        public virtual bool IsAt(Point2D point) //check if the mouse position is on the button
        {
            return SplashKit.PointInRectangle(
                point, new Rectangle() { X = X, Y = Y, Width = _width, Height = _height }
            );
        }
       
    }
}
