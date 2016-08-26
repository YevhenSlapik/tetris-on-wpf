using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Classes
{
    class Board
    {
        private int _columnNumber;
        private int _rowNumber;
        private int _score;
        private int _filledLines;
        private Tetromino _currentFigure;
        private Label[,] _blockControls;
        public Board(Grid tetrisGrid)
        {
            _rowNumber = tetrisGrid.RowDefinitions.Count;
            _columnNumber = tetrisGrid.ColumnDefinitions.Count;
            _score = 0;
            _filledLines = 0;
            _blockControls = new Label[_rowNumber,_columnNumber];
            for (int i = 0; i < _columnNumber; i++)
            {
                for (int j = 0; j < _rowNumber; j++)
                {
                    _blockControls[i,j] = new Label();
                    _blockControls[i,j].Background = Brushes.Transparent;
                    _blockControls[i,j].BorderBrush = Brushes.Gray;
                    _blockControls[i,j].BorderThickness = new Thickness(1,1,1,1);
                    Grid.SetRow(_blockControls[i,j],j);
                    Grid.SetColumn(_blockControls[i,j], i);
                    tetrisGrid.Children.Add(_blockControls[i,j]);
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

            }
        }

        private void EraseFigure()
        {

        }

    }
}
