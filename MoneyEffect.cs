using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class MoneyEffect : DrawObject
    {
        private int _money;
        private DateTime _timeInitialized;
        private int _width, _height;
        private const double expireTime = 1.5; //expire time for money effect
        public MoneyEffect(int money, float x, float y, int width, int height): base(x,y,"money effect")
        {
            _money = money;
            _timeInitialized = DateTime.Now;
            _width = width;
            _height = height;
        }
        public override void Draw()
        {

            if (_money > 0) //positive amount of money
            {
                string text = string.Format("+${0}", _money);
                float msgHeight = SplashKit.TextHeight(text, "GameFont", 15);
                SplashKit.DrawText(text, Color.Green, "GameFont", 15, X + 170, Y + (_height - msgHeight) / 2);
            }
            else //negative amount of money
            {
                string text = string.Format("-${0}", -_money);
                float msgHeight = SplashKit.TextHeight(text, "GameFont", 15);

                SplashKit.DrawText(text, Color.Red, "GameFont", 15, X + 170, Y + (_height - msgHeight) / 2);
            }
        }
        public bool Expire() //check if money effect expires
        {
            var time = DateTime.Now;
            if (time.Subtract(_timeInitialized).TotalSeconds > expireTime)
            {
                return true;
            }
            return false;
        }
    }
}
