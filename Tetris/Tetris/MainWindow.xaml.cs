using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tetris.Classes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow{
        private DispatcherTimer _timer;
        private MainBoard _tetrisMainBoard;
        public MainWindow()
        {   
            InitializeComponent();
        }                                             
        public void MainWindow_Initialized(object sender,EventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick; 
            GameStart();
        }

        private void GameStart()                                  
        {
            tetrisGrid.Children.Clear();
            nextFigureGrid.Children.Clear();
            _tetrisMainBoard = new MainBoard(tetrisGrid, nextFigureGrid);
            _timer.Stop();
            _timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0,400);
            _tetrisMainBoard.CurrentFigureMoveDown();
        }
                                                                            
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if(_timer.IsEnabled)_tetrisMainBoard.CurrentFigureMoveLeft();
                    break;
                case Key.Right:
                    if (_timer.IsEnabled)                                 
                        _tetrisMainBoard.CurrentFigureMoveRight();
                    break;
                case Key.Down:
                        _tetrisMainBoard.CurrentFigureMoveDown();
                    break;
                case Key.Space:
                    _tetrisMainBoard.CurrentFigureMoveRotate();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F1:
                    GamePause();
                    break;

            }
        }
        private void GamePause()
        {
            if (_timer.IsEnabled)
            {
                  _timer.Stop();
            }
            else
            {
                _timer.Start();                         
            }
        }
    }
}
