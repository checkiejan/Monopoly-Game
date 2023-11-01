using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class Dice : DrawObject
    {
        private int _currentNumber;

        public Dice(float x, float y, string name) : base(x, y, name)
        {
            _currentNumber = 1; //initializing the current number of 1
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(GetBitmap(), X, Y);
        }
        public int CurrentNumber
        {
            get
            {
                return _currentNumber;
            }
            set
            {
                _currentNumber = value;
            }
        }
        public void Roll()
        {
            CurrentNumber = new Random().Next(1, 7); //get random number from 1 to 6
        }
        public Bitmap GetBitmap()
        {
            return SplashKit.BitmapNamed(string.Format("{0}", CurrentNumber)); //get the bitmap based on the current number
        }
    }
}
