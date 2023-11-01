using Newtonsoft.Json.Linq;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    //Direction that the cell faces
    public enum Direction
    {
        South,
        North,
        West,
        East,
    }
    public abstract class Cell : DrawObject, ICanBeDisplayed //cell can be fetched to display
    {
        private int _width;
        private int _height;
        private int _position;
        protected Color _background;
        protected DrawStrategy _drawStrategy;

        public Cell(string name,float x,float y,int width, int height, Direction direction, int position): base(x,y,name)
        {
            _width = width;
            _height = height;
            _position = position;
            _background = Color.Bisque;
            if (direction == Direction.South) // set the strategy to draw based on the direction of the cell
            {
                _drawStrategy = new DrawSouth();
            }
            else if (direction == Direction.West)
            {
                _drawStrategy = new DrawWest();
            }
            else if (direction == Direction.North)
            {
                _drawStrategy = new DrawNorth();
            }
            else if (direction == Direction.East)
            {
                _drawStrategy = new DrawEast();
            }
        }
        public bool IsAt(Point2D pt) // check if the mouse is at the cell
        {
            if (pt.X >= X && pt.X <= (_width + X) && pt.Y >= Y && pt.Y <= (_height + Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Width
        {
            get 
            {
                return _width;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
        }
        public Point2D CenterPoint() //return the centre point of the cell
        {

                Rectangle r = new Rectangle();
                r.X = X;
                r.Y = Y;
                r.Width = Width;
                r.Height = Height;
                return SplashKit.RectangleCenter(r);
            
        }
        public int Position //position on th board
        {
            get { return _position; }
        }
        public override void Draw()
        {
            DrawOutline();
            SplashKit.FillRectangle(_background, X + 1, Y + 1, _width - 2, _height - 2);
            _drawStrategy.DrawName(X, Y, Width, Height, Name);
        }

        public virtual void DrawOutline() //draw the outline for the cell
        {
            SplashKit.FillRectangle(Color.Black, X, Y, _width, _height);
        }
        public abstract string Description
        {
            get;
        }
        public abstract void PlayerVisit(Player p);
        public virtual void LoadData(JObject ob){}
    }
}
