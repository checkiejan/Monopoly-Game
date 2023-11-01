using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SplashKitSDK;
namespace Custom
{
    public class PlayerDisplayBox: DrawObject 
    {
        private Player _player;
        private int _width;
        private int _height;
        private Color _color;
        private List<int> _moneys;
        private MoneyEffect _effect = null;
        public PlayerDisplayBox(float x, float y, string name, Player player): base(x, y, name)
        {
            _player = player;
            _width = 250;
            _height = 50;
            _color = Color.Bisque;
            _moneys = new List<int>();
        }
        public override void Draw()
        {
            UpdateEffect();
            SplashKit.FillRectangle(Color.Black, X, Y, _width, _height);
            if (_player.IsBankrupt)
            {
                SplashKit.FillRectangle(Color.LightPink, X + 1, Y + 1, _width - 2, _height - 2);
            }
            else
            {
                SplashKit.FillRectangle(_color, X + 1, Y + 1, _width - 2, _height - 2);

            }
            DrawPlayer();
            DrawEffect();
        }
        public void DrawPlayer()
        {
            SplashKit.FillRectangle(_player.Color, X +7, Y + 15, 20, 20);
            string text = string.Format("{0}: ${1}",_player.Name,_player.Money);
            float msgHeight = SplashKit.TextHeight(text, "GameFont", 15);
            SplashKit.DrawText(text, Color.Black, "GameFont", 15,X +40 , Y + (Height-msgHeight)/2);
        }
        public void NotifyMoneyChange(int money)
        {
            if(_effect == null)
            {
                _effect = new MoneyEffect(money, X, Y, Width, Height); 
            }
            else
            {
               
                _moneys.Add(money);
            }
        }
        private void UpdateEffect()
        {
            if(_effect != null)
            {
                if (_effect.Expire())
                {
                    _effect = null;
                }
            }
            else if(_moneys.Count > 0)
            {
                _effect = new MoneyEffect(_moneys[0], X, Y, Width, Height);
                _moneys.RemoveAt(0);
            }
        }
        private void DrawEffect()
        {
            if(_effect != null)
            {
                _effect.Draw();
            }
        }
        public int Width
        {
            get 
            { 
                return _width; 
            }
        }
        public bool IsAt(Point2D point)
        {
            return SplashKit.PointInRectangle(
                point, new Rectangle() { X = X, Y = Y, Width = _width, Height = _height }
            );
        }
        public int Height
        {
            get
            {
                return _height;
            }
        }
        public Player Player
        {
            get
            {
                return _player;
            }
        }
    }
}
