using Project.Infrastructure.Commands;
using Project.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Project.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties
        private DispatcherTimer FlyTimer = new DispatcherTimer();
        private Random Random = new Random();

        private int _PlaneColumn = 1;
        public int PlaneColumn
        {
            get => _PlaneColumn;
            set => Set(ref _PlaneColumn, value);
        }

        private int _HelicopterColumn = 1;
        public int HelicopterColumn
        {
            get => _HelicopterColumn;
            set => Set(ref _HelicopterColumn, value);
        }

        private int _HelicopterRow = 0;
        public int HelicopterRow
        {
            get => _HelicopterRow;
            set => Set(ref _HelicopterRow, value);
        }
        #endregion

        #region Commands
        public ICommand FlyLeftCommand { get; }
        public ICommand FlyRightCommand { get; }
        private void FlyLeftCommandExecuted(object p)
        {
            PlaneColumn = 0;
        }
        private void FlyRightCommandExecuted(object p)
        {
            PlaneColumn = 1;
        }
        #endregion

        public MainWindowViewModel()
        {
            FlyLeftCommand = new ActionCommand(FlyLeftCommandExecuted, null);
            FlyRightCommand = new ActionCommand(FlyRightCommandExecuted, null);

            FlyTimer.Interval = TimeSpan.FromMilliseconds(250);
            FlyTimer.Tick += new EventHandler(FlyTimerTick);
            FlyTimer.Start();
        }

        #region Methods
        public void FlyTimerTick(object sender, EventArgs e)
        {
            HelicopterRow++;
            if ((HelicopterRow == 4) && (PlaneColumn == HelicopterColumn))
            {
                App.Current.Shutdown();
            }
            if (HelicopterRow > 4)
            {
                HelicopterRow = 0;
                HelicopterColumn = Random.Next(0, 2);
            }
        }
        #endregion
    }
}
