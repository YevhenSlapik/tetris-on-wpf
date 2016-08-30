using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Grid;
using DevExpress.XtraSpellChecker.Parser;

namespace Tetris.Classes
{
    internal class Board
    {
        private int Rows;
        private int Cols;
        private int _score;
        private int _filledLines;
        private Tetromino _currentFigure;
        private Label[,] BlockControls;
        private Brush _noBrush = Brushes.Transparent;

        public Board(Grid tetrisGrid)
        {
            Cols = tetrisGrid.ColumnDefinitions.Count;
            Rows = tetrisGrid.RowDefinitions.Count;
            _score = 0;
            _filledLines = 0;
            BlockControls = new Label[Cols, Rows];
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = Brushes.Transparent;
                    BlockControls[i, j].BorderBrush = Brushes.Transparent;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    tetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            _currentFigure = new Tetromino();
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

        private void DrawFigure()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            var shapeColor = _currentFigure.GetFigureColor();
            foreach (var block in shape)
            {
                BlockControls[(int) (block.X + position.X) + ((Cols/2) - 1),
                    (int) (block.Y + position.Y) + 2].Background = shapeColor;
            }
        }

        private void EraseFigure()
        {
            var position = _currentFigure.GetFigurePosition();
            var shape = _currentFigure.GetFigureShape();
            foreach (var block in shape)
            {
                BlockControls[(int) (block.X + position.X) + ((Cols/2) - 1),
                    (int) (block.Y + position.Y) + 2].Background = _noBrush;
            }
        }

        private void CheckRows()
        {
            bool full;
            for (int i = Rows - 1; i > 0; i--)
            {
                full = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (BlockControls[j, i].Background == _noBrush)
                    {
                        full = false;
                    }

                }
                if (full)
                {
                    _score += 100;
                    RemoveRow(i);
                    _filledLines++;
                    i++;
                }
            }
        }

        private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }

        public void CurrentFigureMoveLeft()
        {
            Point Position = _currentFigure.GetFigurePosition();
            Point[] Shape = _currentFigure.GetFigureShape();
            bool move = true;
            EraseFigure();
            foreach (Point S in Shape)
            {

                if (((int) (S.X + Position.X) + ((Cols/2) - 1) - 1) < 0)
                {
                    move = false;
                }

                else if (BlockControls[((int) (S.X + Position.X) + ((Cols/2) - 1) - 1),
                    (int) (S.Y + Position.Y) + 2].Background != _noBrush)
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
            var Position = _currentFigure.GetFigurePosition();
            var Shape = _currentFigure.GetFigureShape();
            bool move = true;
            EraseFigure();
            foreach (Point S in Shape)
            {
                if (((int) (S.X + Position.X) + ((Cols/2) - 1) + 1) >= Cols)
                {
                    move = false;
                }
                else if (BlockControls[((int) (S.X + Position.X) + ((Cols/2) - 1) + 1),
                    (int) (S.Y + Position.Y) + 2].Background != _noBrush)
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
            Point Position = _currentFigure.GetFigurePosition();
            Point[] Shape = _currentFigure.GetFigureShape();
            bool move = true;
            EraseFigure();
            foreach (Point S in Shape)
            {
                if (((int) (S.Y + Position.Y) + 2 + 1) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int) (S.X + Position.X) + ((Cols/2) - 1)),
                    (int) (S.Y + Position.Y) + 2 + 1].Background != _noBrush)
                {
                    move = false;
                    if (((int) (S.Y + Position.Y) + 2 + 1) <= 1)
                    {
                        MessageBox.Show("       GAME OVER\n\nSCORE:" + _score.ToString(" 0000000000") + "\nLINES:" +
                                        _filledLines.ToString("   0000000000"));
                        Application X = Application.Current;
                        X.Shutdown();
                    }
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

                _currentFigure = new Tetromino();
            }
        }

        public void CurrentFigureMoveRotate()
        {
            Point Position = _currentFigure.GetFigurePosition();
            Point[] S = new Point[4];
            Point[] Shape = _currentFigure.GetFigureShape();
            bool move = true;
            Shape.CopyTo(S, 0);
            EraseFigure();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y*-1;
                S[i].Y = x;
                if (((int) ((S[i].Y + Position.Y) + 2)) >= Rows)
                {
                    move = false;
                }
                else if (((int) (S[i].X + Position.X) + ((Cols/2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int) (S[i].X + Position.X) + ((Cols/2) - 1)) >= Cols)
                {
                    move = false;
                }
                else if (BlockControls[((int) (S[i].X + Position.X) + ((Cols/2) - 1)),
                    (int) (S[i].Y + Position.Y) + 2].Background != _noBrush)
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

