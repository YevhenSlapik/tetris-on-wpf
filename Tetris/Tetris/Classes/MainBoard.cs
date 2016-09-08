using System.Windows;
using System.Windows.Controls;
namespace Tetris.Classes
{
    /// <summary>Главное игровое поле</summary>
    public sealed class MainBoard:Board{
        
        #region Поля класса
        private int _score;  //счет
        private int _filledLines;//заполненные линии
        private Tetromino _nextFigure;//след.фигура
        private bool _move;  //можно ли двигать
        private Point _position;//позиция фигуры на момент сдвига
        private Point[] _shape;  //форма сдвигаемой фигуры
        private Tetromino _currentFigure;//текущая фигура на основной доске
        private readonly Board _nextFigureBoard;// доска со следующей фигурой
        private readonly MainWindow _window;//окно приложения
        private readonly GameOver _resultForm;//окно окончания игры 
        #endregion

        #region К-тор
        /// <summary> К-тор главной доски. 
        /// Отрисовывает фигуры при старте на привязанных гридах.
        /// Инициализирует сами гриды. </summary>
        /// <param name="tetrisGrid">Игровой грид</param>
        /// <param name="nextFigureGrid">Грид следующей фигуры</param>
        /// <param name="mainWindow">Основная форма</param>
        public MainBoard(Grid tetrisGrid, Grid nextFigureGrid, MainWindow mainWindow)
            : base(tetrisGrid)
        {
            _resultForm = new GameOver();
            _window = mainWindow;
            _currentFigure = new Tetromino();
            _nextFigure = new Tetromino();
            _nextFigureBoard = new Board(nextFigureGrid);
            _nextFigureBoard.DrawFigure(_nextFigure, 2);
            DrawFigure(_currentFigure, 1);
        } 
        #endregion

        #region Функции работы с заполненным рядом
        
        /// <summary>Проверка заполнен ли ряд, 
        /// заполнен - удалить и засчитать очки </summary>
        private void CheckRows()
        {
            for (var i = Rows - 1; i > 0; i--)
            {
                var full = true;
                for (var j = 0; j < Cols; j++)
                {
                    if (Equals(BlockControls[j, i].Background, NoBrush))
                    {
                        full = false;
                    }

                }
                if (!full)
                    continue;
                _score += 100;
                RemoveRow(i);
                _filledLines++;
                i++;
                _window.rowNum.Content = _filledLines.ToString(" 0000000000");
                _window.score.Content = _score.ToString(" 0000000000");
            }
        }

        /// <summary>Удалить ряд</summary>
        /// <param name="row">указанный ряд</param>
        private void RemoveRow(int row)
        {
            for (var i = row; i > 2; i--)
            {
                for (var j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        } 
        #endregion

        #region Функции перемещения фигуры на доске
        /// <summary> Вспомогательный метод для функций перемещения 
        /// который обновляет данные о фигуре(позиция, форма, возможность сдвига) на момент движения
        /// </summary>
        private void GetCurrentFigureInfo()
        {
            _position = _currentFigure.GetFigurePosition();//позиция на момент сдвига
            _shape = _currentFigure.GetFigureShape();//форма сдвигаемой фигуры
            _move = true;//можно ли двигать
            EraseFigure(_currentFigure, 1);//стереть на текущем положении
        }

        #region Влево
        /// <summary>
        /// Сдвинуть фигуру на доске влево
        /// </summary>
        public void CurrentFigureMoveLeft()
        {
            GetCurrentFigureInfo();
            foreach (var s in _shape)
            {

                if (((int)(s.X + _position.X) + ((Cols / 2) - 1) - 1) < 0)  //упирается  в стену - двигать нельзя
                {
                    _move = false;
                }

                else if (!Equals(BlockControls[((int)(s.X + _position.X) + ((Cols / 2) - 1) - 1),    // упирается  в фигуру  - двигать нельзя
                    (int)(s.Y + _position.Y) + 1].Background, NoBrush))  //проверка путем наличия цвета
                {
                    _move = false;
                }
            }
            if (_move) //препятсвий нет - отрисовать фигуру со сдвигом на 1 блок влево
            {
                _currentFigure.MoveLeft();
                DrawFigure(_currentFigure, 1);
            }

            else   //препятсвия есть - просто отрисовать фигуру
            {
                DrawFigure(_currentFigure, 1);
            }
        } 
        #endregion

        #region Вправо
        public void CurrentFigureMoveRight()
        {
            GetCurrentFigureInfo();
            foreach (var s in _shape)
            {
                if (((int)(s.X + _position.X) + ((Cols / 2) - 1) + 1) >= Cols)     //упирается  в стену - двигать нельзя
                {
                    _move = false;
                }
                else if (!Equals(BlockControls[((int)(s.X + _position.X) + ((Cols / 2) - 1) + 1),     // упирается  в фигуру  - двигать нельзя
                    (int)(s.Y + _position.Y) + 1].Background, NoBrush))   //проверка путем наличия цвета
                {
                    _move = false;
                }
            }
            if (_move)                  //препятсвий нет - отрисовать фигуру со сдвигом на 1 блок вправо
            {
                _currentFigure.MoveRight();
                DrawFigure(_currentFigure, 1);
            }
            else    //препятсвия есть - просто отрисовать фигуру
            {
                DrawFigure(_currentFigure, 1);
            }
        } 
        #endregion

        #region Вниз
        public void CurrentFigureMoveDown()
        {
            GetCurrentFigureInfo();
            foreach (var s in _shape)
            {
                if (((int)(s.Y + _position.Y) + 1 + 1) >= Rows) //упирается в дно
                {
                    _move = false;
                }
                else if (!Equals(BlockControls[((int)(s.X + _position.X) + ((Cols / 2) - 1)),
                    (int)(s.Y + _position.Y) + 1 + 1].Background, NoBrush))    //упирается в фигуру
                {
                    _move = false;
                    if (((int)(s.Y + _position.Y) + 1 + 1) > 3)  //упирается ли в потолок. да - конец игры
                        continue;
                    DrawFigure(_currentFigure, 1);
                    _resultForm.ShowDialog(_score.ToString(" 0000000000"));
                    if (_resultForm.DialogResult == false)
                    {
                        var x = Application.Current;
                        x.Shutdown();
                        return;
                    }
                    _window.GameStart();
                }
            }
            if (_move)
            {
                _currentFigure.MoveDown();
                DrawFigure(_currentFigure, 1);
            }
            else
            {
                DrawFigure(_currentFigure, 1);
                _nextFigureBoard.EraseFigure(_nextFigure, 2);
                CheckRows();
                _currentFigure = _nextFigure;
                _nextFigure = new Tetromino();

                //_nextFigureBoard.NextFigure = _nextFigure;
                _nextFigureBoard.DrawFigure(_nextFigure, 2);
            }

        } 
        #endregion

        #region Вращать
        public void CurrentFigureMoveRotate()
        {

            GetCurrentFigureInfo();
            var s = new Point[4];
            _shape.CopyTo(s, 0);
            
            for (var i = 0; i < s.Length; i++)
            {
                var x = s[i].X;
                s[i].X = s[i].Y * -1;
                s[i].Y = x;
                if (((int)((s[i].Y + _position.Y) + 1)) >= Rows)
                {
                    _move = false;
                }
                else if (((int)(s[i].X + _position.X) + ((Cols / 2) - 1)) < 0)
                {
                    _move = false;
                }
                else if (((int)(s[i].X + _position.X) + ((Cols / 2) - 1)) >= Cols)
                {
                    _move = false;
                }
                else if (!Equals(BlockControls[((int)(s[i].X + _position.X) + ((Cols / 2) - 1)),
                    (int)(s[i].Y + _position.Y) + 1].Background, NoBrush))
                {
                    _move = false;
                }
            }
            if (_move)
            {
                _currentFigure.Rotate();
                DrawFigure(_currentFigure, 1);
            }
            else
            {
                DrawFigure(_currentFigure, 1);
            }
        }                                                                                                
        #endregion

        #endregion
    }
}

