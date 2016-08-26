using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Classes
{
    public class Tetromino
    {

        private Brush[] _availableColors =
        {
            Brushes.DarkGoldenrod,
            Brushes.Crimson,
            Brushes.LightSalmon,
            Brushes.DarkBlue,
            Brushes.ForestGreen,
            Brushes.LightGreen,
            Brushes.OrangeRed,
            Brushes.Teal,
            Brushes.DarkMagenta,
            Brushes.Gold
        };
        private Point _currentPos; //позиция фигуры
        private Point[] _shape;    //форма фигуры
        private Brush _color;       //цвет фигуры
        private bool _rotatable;   //можно ли вращать        
        
        public Point GetFigurePosition()
        {
            return _currentPos;
        }
        public Brush GetFigureColor()
        {
            return _color;
        }
        public Point[] GetFigureShape()
        {
            return _shape;
        }

        public void MoveLeft()
        {
            _currentPos.X -= 1;
        }
        public void MoveRight()
        {
            _currentPos.X += 1;
        }
        public void MoveDown()
        {
            _currentPos.Y += 1;
        }
        public void Rotate()
        {
            if (_rotatable)
            {
                for (int i = 0; i < _shape.Length; i++)
                {
                    double x = _shape[i].X;
                    _shape[i].X = _shape[i].Y*-1;
                    _shape[i].Y = x;
                }
            }
        }
                               

        public Tetromino()
        {
            _currentPos = new Point(0,0);
            _color = Brushes.Transparent;
            _shape = generateShape();
        }

        private Point[] generateShape()
        {
            var randomShape = new Random().Next(0, 7);
            switch (randomShape)
            {
                case 0:    //I
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0), 
                      new Point(-1,0), 
                      new Point(1,0), 
                      new Point(2,0)
                    };   
                    
                case 1:    //J
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(1,-1), 
                      new Point(-1,0), 
                      new Point(0,0), 
                      new Point(1,0)
                    };
                case 2: // L
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0), 
                      new Point(-1,0), 
                      new Point(1,0), 
                      new Point(-1,1)
                    };
                case 3:  //o
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0), 
                      new Point(0,1), 
                      new Point(1,0), 
                      new Point(1,1)
                    };
                case 4:  //S
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0), 
                      new Point(-1,0), 
                      new Point(0,1), 
                      new Point(1,0)
                    };
                case 5: //T
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0), 
                      new Point(-1,0), 
                      new Point(0,-1), 
                      new Point(1,1)
                    };
                case 6://Z
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0), 
                      new Point(-1,0), 
                      new Point(0,1), 
                      new Point(1,1)
                    };
            }
        }

    }
}
