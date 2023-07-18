using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Threading.Tasks;

namespace EMC07.ControlsUI.ViewModel
{
    internal class MainViewModel : BaseSetupItemViewModel
    {
        private DispatcherTimer timer;

        public MainViewModel()
        {
            Title = "Проверка контролов ввода данных";

            CurDate = "--.--.--";

            CurTime = "--:--:--";

            _TextField = "QWERTY";

            _NumField = "12345";

            StartTimer();
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            CurDate = DateTime.Now.ToString("dd.MM.yy");
            OnPropertyChanged(nameof(CurDate));

            CurTime = DateTime.Now.ToString("HH:mm:ss");
            OnPropertyChanged(nameof(CurTime));
        }

        public string Title { get; private set; }

        public string CurDate { get; private set; }

        public string CurTime { get; private set; }

        #region Текстовое поле/TextField
        private string _TextField;
        public string TextField
        {
            get { return _TextField; }
            set
            {
                OnPropertyTextFieldChanged(value);
            }
        }

        private void OnPropertyTextFieldChanged(String _uiVal)
        {
            if (_uiVal != _TextField)
            {
                _TextField = _uiVal;
                //ToDo: Save to file
            }
        }
        #endregion

        #region Числовое поле/NumField
        private string _NumField;
        public string NumField
        {
            get { return _NumField; }
            set
            {
                OnPropertyNumFieldChanged(value);
            }
        }

        private void OnPropertyNumFieldChanged(String _uiVal)
        {
            if (_uiVal != _NumField)
            {
                var valid = double.TryParse(_uiVal, out double temp);

                if (valid)
                {
                    _NumField = _uiVal;
                }
                else
                {
                    Utl.MessageEr("Ошибка ввода данных");
                    OnPropertyChanged(nameof(NumField));
                }
            }
        }
        #endregion

        #region Команда закрытия
        private DelegateCommand _commandClose;
        public DelegateCommand CommandClose => _commandClose ??= new DelegateCommand(async (window) => await Close(window));

        private async Task Close(object arg)
        {
            if (arg != null && arg is Window window)
            {
                if (await Utl.YesNo("Закрыть программу?"))
                {
                    timer?.Stop();
                    window.Close();
                }
            }
        }

        #endregion
    }
}
