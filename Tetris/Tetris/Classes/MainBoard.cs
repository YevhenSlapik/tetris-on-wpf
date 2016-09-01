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
        private readonly NextFigureBoard _nextFigureBoard;// доска со следующей фигурой
        private Tetromino _currentFigure;//текущая фигура на основной доске
        private readonly MainWindow _window;//окно приложения
        private readonly GameOver _resultForm;//окно окончания игры 
        #endregion

        #region К-тор
        /// <summary> К-тор главной доски. Отрисовывает фигуры при старте на привязанных гридах. </summary>
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
            _nextFigureBoard = new NextFigureBoard(nextFigureGrid);
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

        #region Влево
        /// <summary>
        /// Сдвинуть фигуру на доске влево
        /// </summary>
        public void CurrentFigureMoveLeft()
        {
            var position = _currentFigure.GetFigurePosition();//позиция на момент сдвига
            var shape = _currentFigure.GetFigureShape();//форма сдвигаемой фигуры
            var move = true;//можно ли двигать
            EraseFigure(_currentFigure, 1);//стереть на текущем положении
            foreach (var s in shape)
            {

                if (((int)(s.X + position.X) + ((Cols / 2) - 1) - 1) < 0)  //упирается  в стену - двигать нельзя
                {
                    move = false;
                }

                else if (!Equals(BlockControls[((int)(s.X + position.X) + ((Cols / 2) - 1) - 1),    // упирается  в фигуру  - двигать нельзя
                    (int)(s.Y + position.Y) + 1].Background, NoBrush))  //проверка путем наличия цвета
                {
                    move = false;
                }
            }

            if (move) //препятсвий нет - отрисовать фигуру со сдвигом на 1 блок влево
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
            var position = _currentFigure.GetFigurePosition(); //позиция на момент сдвига
            var shape = _currentFigure.GetFigureShape();  //форма сдвигаемой фигуры
            var move = true; //можно ли двигать
            EraseFigure(_currentFigure, 1);
            foreach (var s in shape)
            {
                if (((int)(s.X + position.X) + ((Cols / 2) - 1) + 1) >= Cols)     //упирается  в стену - двигать нельзя
                {
                    move = false;
                }
                else if (!Equals(BlockControls[((int)(s.X + position.X) + ((Cols / 2) - 1) + 1),     // упирается  в фигуру  - двигать нельзя
                    (int)(s.Y + position.Y) + 1].Background, NoBrush))   //проверка путем наличия цвета
                {
                    move = false;
                }
            }
            if (move)                  //препятсвий нет - отрисовать фигуру со сдвигом на 1 блок вправо
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

            var position = _currentFigure.GetFigurePosition();      //позиция на момент сдвига
            var shape = _currentFigure.GetFigureShape();        //форма сдвигаемой фигуры
            var move = true;  //можно ли двигать
            EraseFigure(_currentFigure, 1); //стереть на текущем положении
            foreach (var s in shape)
            {
                if (((int)(s.Y + position.Y) + 1 + 1) >= Rows) //упирается в дно
                {
                    move = false;
                }
                else if (!Equals(BlockControls[((int)(s.X + position.X) + ((Cols / 2) - 1)),
                    (int)(s.Y + position.Y) + 1 + 1].Background, NoBrush))    //упирается в фигуру
                {
                    move = false;
                    if (((int)(s.Y + position.Y) + 1 + 1) > 3)  //упирается ли в потолок. да - конец игры
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
            if (move)
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
            var position = _currentFigure.GetFigurePosition();
            var s = new Point[4];
            var shape = _currentFigure.GetFigureShape();
            var move = true;
            shape.CopyTo(s, 0);
            EraseFigure(_currentFigure, 1);
            for (var i = 0; i < s.Length; i++)
            {
                var x = s[i].X;
                s[i].X = s[i].Y * -1;
                s[i].Y = x;
                if (((int)((s[i].Y + position.Y) + 1)) >= Rows)
                {
                    move = false;
                }
                else if (((int)(s[i].X + position.X) + ((Cols / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(s[i].X + position.X) + ((Cols / 2) - 1)) >= Cols)
                {
                    move = false;
                }
                else if (!Equals(BlockControls[((int)(s[i].X + position.X) + ((Cols / 2) - 1)),
                    (int)(s[i].Y + position.Y) + 1].Background, NoBrush))
                {
                    move = false;
                }
            }
            if (move)
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

