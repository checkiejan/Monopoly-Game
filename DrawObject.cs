using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
namespace Custom
{
    public abstract class DrawObject //every objects that can draw will inherit this class
    {
        private float _x; // x coordinate on the screen
        private float _y;// y coordinate on the screen
        private string _name;

        public DrawObject(float x, float y, string name)       
        {
            _x = x;
            _y = y;
            _name = name;
        }
        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public float Y 
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public bool AreYou(string name) //to identify the object
        {
            if(_name == name)
            {
                return true;
            }
            return false;
        }
        public abstract void Draw();
        public string Name { get { return _name; } }

    }
}
