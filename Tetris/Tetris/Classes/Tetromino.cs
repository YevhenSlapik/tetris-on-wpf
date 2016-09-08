using System;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Classes
{
    /// <summary>Класс который отвечает за генерацию фигур</summary>
    public class Tetromino 
    {
        /// <summary>
        /// К-тор фигурки. Инициализирует  3 поля фигурки -  текущую позицию (0,0), начальный цвет, форму и окончательный цвет
        /// </summary>
        public Tetromino()
        {
            _currentPos = new Point(0, 0);
            _color = Brushes.Transparent;
            _shape = GenerateShape();
        }

        #region Поля класса
        private static readonly Random PropRandomizer = new Random();    //для случайного отбора фигур
        private readonly Brush[] _availableColors =   //Доступные цвета для фигур
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
        private readonly Point[] _shape;    //форма фигуры
        private Point _currentPos; //позиция фигуры
        private Brush _color;       //цвет фигуры
        private bool _rotatable;   //можно ли вращать         
        
       
        /// <summary> геттер позиции фигуры </summary>
        /// <returns>позицию фигуры</returns>
        public Point GetFigurePosition()
        {
            return _currentPos;
        }
        /// <summary> Воззвращает цвет фигуры </summary>
        /// <returns></returns>
        public Brush GetFigureColor()
        {
            return _color;
        }
        /// <summary> Воззвращает форму фигуры в виде массива поинтов </summary>
        /// <returns>массив поинтов</returns>
        public Point[] GetFigureShape()
        {
            return _shape;
        }
        
        #endregion

        #region Сдвиг положения фигуры
        /// <summary> Сдвиг положения фигуры налево на 1</summary>
        public void MoveLeft()
        {
            _currentPos.X -= 1;
        }
        /// <summary> Сдвиг положения фигуры направо на 1</summary>
        public void MoveRight()
        {
            _currentPos.X += 1;
        }
        /// <summary> Сдвиг положения фигуры вниз на 1</summary>
        public void MoveDown()
        {
            _currentPos.Y += 1;
        }
        ///<summary> Вращение  фигуры по часовой стрелке на 90 градусов</summary>
        public void Rotate()
        {
            if (_rotatable)
            {
                for (int i = 0; i < _shape.Length; i++)
                {
                    double x = _shape[i].X;
                    _shape[i].X = _shape[i].Y * -1;
                    _shape[i].Y = x;
                }
            }
        } 
        #endregion
                               
        #region Генератор фигуры
        /// <summary> Случайным образом генерирует цвет и  форму фигуры в виде массива поинтов </summary>
        /// <returns>форму фигуры в виде массива поинтов</returns>
        private Point[] GenerateShape()
        {
            var randomShape = PropRandomizer.Next(0, 7);
            switch (randomShape)
            {
                case 0:    //I
                    _rotatable = true;
                    _color = _availableColors[PropRandomizer.Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,-1),
                        new Point(-1,-1),
                        new Point(1,-1),
                        new Point(2,-1)
                    };

                case 1:    //J
                    _rotatable = true;
                    _color = _availableColors[PropRandomizer.Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                     new Point(-1,-1),
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0)
                    };
                case 2: // L
                    _rotatable = true;
                    _color = _availableColors[PropRandomizer.Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 3:  //O
                    _rotatable = false;
                    _color = _availableColors[PropRandomizer.Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0),
                        new Point(0,-1),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 4:  //S
                    _rotatable = true;
                    _color = _availableColors[PropRandomizer.Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,-1)
                    };
                case 5: //T
                    _rotatable = true;
                    _color = _availableColors[PropRandomizer.Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,0)
                    };
                case 6://Z
                    _rotatable = true;
                    _color = _availableColors[new Random().Next(0, _availableColors.Length)];
                    return new Point[]
                    {
                      new Point(0,-1),                              
                      new Point(-1,-1),
                        new Point(0,0),
                        new Point(1,0)
                    };
                default:
                    return null;
            }
        } 
        #endregion
    }
}
