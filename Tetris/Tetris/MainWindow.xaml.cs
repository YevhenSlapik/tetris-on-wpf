using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tetris.Classes;

namespace Tetris
{
    /// <summary>
    /// Окно приложения
    /// </summary>
    public partial class MainWindow
    {
        private DispatcherTimer _timer;  //Таймер
        private MainBoard _tetrisMainBoard; //Основная игровая доска
        /// <summary>
        /// К-тор окна приложения
        /// </summary>
        public MainWindow()
        {   
            InitializeComponent();
        }                                             
         /// <summary>
         ///  Запуск таймера и игры при инициализации
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        public void MainWindow_Initialized(object sender,EventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick; 
            GameStart();
        }
        /// <summary>
        /// старт игры. 
        /// чистка гридов,перезапуск таймера, обнуление счета
        /// </summary>
        public void GameStart()                                  
        {
            tetrisGrid.Children.Clear();
            nextFigureGrid.Children.Clear();
            _tetrisMainBoard = new MainBoard(tetrisGrid, nextFigureGrid, this );
            _timer.Stop();
            _timer.Start();
            score.Content = "0000000000";
            rowNum.Content = "0000000000";
            pauseLabel.Visibility = Visibility.Hidden;}
        /// <summary>
        /// интервал таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0,400);
            _tetrisMainBoard.CurrentFigureMoveDown();
        }
        /// <summary>
        /// Привязка клавиш управления к клавиатуре
        /// </summary>
        /// <param name="sender">форма</param>
        /// <param name="e">нажатая клавиша</param>                                                           
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
                switch (e.Key)
                {
                    case Key.Left:
                        if (_timer.IsEnabled)
                            _tetrisMainBoard.CurrentFigureMoveLeft();
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
         /// <summary>
         /// Пауза в игре
         /// </summary>
        public void GamePause()
        {
            if (_timer.IsEnabled)
            {
                  _timer.Stop();
                pauseLabel.Visibility = Visibility.Visible;   //Лейбл паузы
                
            }
            else
            {
                _timer.Start();   
                pauseLabel.Visibility = Visibility.Hidden;      
            }
        }
        /// <summary>
        /// При потере фокуса - остановка игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _timer.Stop();
            pauseLabel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// При получении фокуса - продолжение игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _timer.Start();
            pauseLabel.Visibility = Visibility.Hidden;
        }

    }
}
                         