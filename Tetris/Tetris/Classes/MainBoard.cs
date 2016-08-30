using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Tetris.Classes
{
    /// <summary>Главное игровое поле</summary>
    internal sealed class MainBoard:Board
    {
        private int _score;
        private int _filledLines;
        private Tetromino _nextFigure;
        private readonly NextFigureBoard _nextFigureBoard ;
        private Tetromino _currentFigure;

        public MainBoard(Grid tetrisGrid,Grid nextFigureGrid)
        {
            Cols = tetrisGrid.ColumnDefinitions.Count;
            Rows = tetrisGrid.RowDefinitions.Count;
            _score = 0;
            _filledLines = 0;
            
            BlockControls = new Label[Cols, Rows];
            for (var i = 0; i < Cols; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label
                    {
                        Background = Brushes.Transparent,
                        BorderBrush = Brushes.Transparent,
                        BorderThickness = new Thickness(1, 1, 1, 1)
                    };
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    tetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            
            _currentFigure = new Tetromino();
            _nextFigure = new Tetromino();
            _nextFigureBoard = new NextFigureBoard(nextFigureGrid) {NextFigure = _nextFigure};
            _nextFigureBoard.DrawFigure();
            DrawFigure();
            
        }

        public int GetScore()
        {
            return _score;
        }

        public int GetFilledLines()                                                              
        {
            return _filledLines;
        }

       
        public override void DrawFigure()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            var shapeColor = _currentFigure.GetFigureColor();
            foreach (var block in shape)
            {
                BlockControls[(int) (block.X + position.X) + ((Cols/2) - 1),
                    (int) (block.Y + position.Y) + 1].Background = shapeColor;
            }
        }
        public override void EraseFigure()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            foreach (var block in shape)
            {
                BlockControls[(int) (block.X + position.X) + ((Cols/2) - 1),
                    (int)(block.Y + position.Y) + 1].Background = NoBrush;
            }
        }

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
                if (!full) continue;
                _score += 100;
                RemoveRow(i);
                _filledLines++;
                i++;
            }
        }

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

        public void CurrentFigureMoveLeft()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            var move = true;
            EraseFigure();
            foreach (var s in shape)
            {

                if (((int) (s.X + position.X) + ((Cols/2) - 1) - 1) < 0)
                {
                    move = false;
                }

                else if (!Equals(BlockControls[((int) (s.X + position.X) + ((Cols/2) - 1) - 1),
                    (int)(s.Y + position.Y) + 1].Background, NoBrush))
                {
                    move = false;
                }
            }

            if (move)
            {
                _currentFigure.MoveLeft();
                DrawFigure();
            }
            else
            {
                DrawFigure();
            }
        }

        public void CurrentFigureMoveRight()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            var move = true;
            EraseFigure();
            foreach (var s in shape)
            {
                if (((int) (s.X + position.X) + ((Cols/2) - 1) + 1) >= Cols)
                {
                    move = false;
                }
                else if (!Equals(BlockControls[((int) (s.X + position.X) + ((Cols/2) - 1) + 1),
                    (int)(s.Y + position.Y) + 1].Background, NoBrush))
                {
                    move = false;
                }
            }
            if (move)
            {
                _currentFigure.MoveRight();
                DrawFigure();
            }
            else
            {
                DrawFigure();
            }
        }

        public void CurrentFigureMoveDown()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            var move = true;
            EraseFigure();
            foreach (var s in shape)
            {
                if (((int) (s.Y + position.Y) + 1 + 1) >= Rows)
                {
                    move = false;
                }
                else if (!Equals(BlockControls[((int) (s.X + position.X) + ((Cols/2) - 1)),
                    (int)(s.Y + position.Y) + 1 + 1].Background, NoBrush))
                {
                    move = false;
                    if (((int) (s.Y + position.Y) + 1 + 1) > 3) continue;
                    DrawFigure();MessageBox.Show("       GAME OVER\n\nSCORE:" + _score.ToString(" 0000000000") + "\nLINES:" +
                                                 _filledLines.ToString("   0000000000"));
                    var x = Application.Current;
                        
                    x.Shutdown();
                    return;
                }
            }
            if (move)
            {
                _currentFigure.MoveDown();
                DrawFigure();
            }
            else
            {
                 DrawFigure();
                CheckRows();
                _currentFigure = _nextFigure;
                _nextFigure = new Tetromino();
                _nextFigureBoard.EraseFigure();
                _nextFigureBoard.NextFigure = _nextFigure;
                _nextFigureBoard.DrawFigure();
            }                                       
        }

        public void CurrentFigureMoveRotate()
        {
            var position = _currentFigure.GetFigurePosition();
            var s = new Point[4];
            var shape = _currentFigure.GetFigureShape();
            var move = true;
            shape.CopyTo(s, 0);
            EraseFigure();
            for (var i = 0; i < s.Length; i++)
            {
                var x = s[i].X;
                s[i].X = s[i].Y*-1;
                s[i].Y = x;
                if (((int) ((s[i].Y + position.Y) + 1)) >= Rows)
                {
                    move = false;
                }
                else if (((int) (s[i].X + position.X) + ((Cols/2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int) (s[i].X + position.X) + ((Cols/2) - 1)) >= Cols)
                {
                    move = false;
                }
                else if (!Equals(BlockControls[((int) (s[i].X + position.X) + ((Cols/2) - 1)),
                    (int)(s[i].Y + position.Y) + 1].Background, NoBrush))
                {
                    move = false;                                                              
                }
            }
            if (move)
            {
                _currentFigure.Rotate();
                DrawFigure();
            }
            else
            {
                DrawFigure();          
            }
        }
    }
}

