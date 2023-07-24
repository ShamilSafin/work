using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia;
using Avalonia.Data;
using System;
using System.ComponentModel;
using Avalonia.VisualTree;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using EMC07.ControlsUI.Views;
using EMC07.ControlsUI.CommandBinding;
using System.Data.Common;
using ReactiveUI;
using System.Windows.Input;

namespace EMC07.ControlsUI
{
    /// <summary>
    /// Interaction logic for NumpadTouchScreen.xaml
    /// </summary>
    public partial class NumpadTouchScreen : Window
    {
        public enum ContentType
        {
            Nul,
            IPAddress,
            RealNum,
            Inf
        }

        private static Window? _InstanceObject;

        private static ClassChangeNumpad? InstanceChangeKeyboard;

        private static NumpadCaption _NumpadCaption;

        private static TextBox _TextBoxMain;     //TextBox с которого получаем данные
        private static TextBox _TextBoxInternal; //TextBox внутренний

        private static string sMainValue;
        private static string sInternalValue;
        private static bool bCmdEnter;

        private BindingValue<string> _bindingValue;
        public static ContentType ContentTp;

        #region AvaloniaProperty ControlEnableProperty

        public static readonly AvaloniaProperty<bool> ControlEnableProperty 
            = AvaloniaProperty.RegisterAttached<NumpadTouchScreen, Control, bool>("ContentType",false);
        //new DirectPropertyMetadata<bool>(default, null));//??
        
        public static bool GetControlEnable(AvaloniaObject obj)
        {
            // return obj.GetValue(ControlEnableProperty);
            return true;
        }

        public static void SetControlEnable(AvaloniaObject obj, bool value)
        {
            obj.SetValue(ControlEnableProperty, value);
        }

        private static void ControlEnablePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs e)
        {
            var host = sender as Control;
            if (host != null)
            {
                host.GotFocus += OnGotFocus;
            }
        }
        #endregion


        #region DependencyProperty ContentProperty
        public static readonly AvaloniaProperty ContentTypeProperty
            = AvaloniaProperty.RegisterAttached<NumpadTouchScreen, Control, ContentType>("ContentType", ContentType.RealNum,
                false, BindingMode.Default);//,ContentTypePropertyChanged);
        public static ContentType GetContentType(AvaloniaObject obj)
        {
            return (ContentType)obj.GetValue(ContentTypeProperty);
        }


        public static void SetContentType(AvaloniaObject obj, ContentType value)
        {
            obj.SetValue(ContentTypeProperty, value);
        }


        private static void ContentTypePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs e)
        {
            /*
            var s = sender as TextBox;
            var bindExp = s?.GetBindingExpression(NumpadTouchScreen.ContentTypeProperty);
            bindExp?.UpdateTarget();
             */            
            var s = sender as TextBox;
            var propertyName = "ContentType";
            var propertyInfo = s.GetType().GetProperty(propertyName);
            var bindingExpression = propertyInfo?.GetValue(s)?.GetType().GetProperty("BindingExpression")?.GetValue(propertyInfo.GetValue(s));
            var bindExp = bindingExpression?.GetType().GetProperty("Value")?.GetValue(bindingExpression);
            bindExp?.GetType().GetMethod("UpdateTarget")?.Invoke(bindExp, null);

        }
        #endregion


        private static int CaretPos;

        private readonly DispatcherTimer timerBeforeClose;

        public NumpadTouchScreen()
        {
            AvaloniaXamlLoader.Load(this); 
            SetCommandBinding();

            #region Caption привязка данных, отображения названия изменяемого поля
            if (_NumpadCaption == null)
                _NumpadCaption = new NumpadCaption();

            DataContext = _NumpadCaption;
            #endregion

            InstanceChangeKeyboard = (ClassChangeNumpad)Resources["ChangeKeyboard"];//ClassChangeNumpad
            InstanceChangeKeyboard.BackgroundKey = (SolidColorBrush)Resources["ColorKey"];
            InstanceChangeKeyboard.BackgroundFixKey = (SolidColorBrush)Resources["ColorFixKey"];

            this.KeyDown += NumpadTouchScreen_KeyDown;

            bCmdEnter = false;

            _TextBoxInternal = this.FindControl<TextBox>("TextBoxInternal");

            timerBeforeClose = new DispatcherTimer();
            timerBeforeClose.Tick += TimerBeforeCloseTick;
            timerBeforeClose.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        void NumpadTouchScreen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)      
            {
                case Key.Escape:
                    var btnClose = this.FindControl<Button>("btnClose");
                    if (btnClose.Command != null)
                        btnClose.Command.Execute(null);
                    break;

                case Key.Enter:
                    var btnEnter = this.FindControl<Button>("btnEnter");
                    if (btnEnter.Command != null)
                        btnEnter.Command.Execute(null);
                    break;
            }
        }

        private static string TouchScreenText
        {
            get { return sInternalValue; }
            set
            {
                _TextBoxInternal.Text = value;
                sInternalValue = value;
            }
        }

        private void TimerBeforeCloseTick(object sender, EventArgs e)
        {
            TouchScreenText = "";
            CaretPos = _TextBoxInternal.CaretIndex = 0;
            timerBeforeClose.Stop();
        }

        private void MouseLeftButtonDown(object sender, PointerPressedEventArgs e)
        {
            timerBeforeClose.Start();
        }

        private void MouseLeftButtonUp(object sender, PointerPressedEventArgs e)
        {
            timerBeforeClose.Stop();
        }
        //Изменил приватность пока на время
        public static async Task RunCommand(object sender)//, RoutedEventArgs e)
        {
            var e = (RoutedEventArgs)sender;
            TouchScreenText = _TextBoxInternal.Text;
            var source = e.Source as Control;
            if (source.Name == "Cmd1")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd1 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;
                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd2")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd2 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;
                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd3")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd3 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;
                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd4")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd4 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd5")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd5 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd6")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd6 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd7")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd7 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd8")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd8 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd9")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd9 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "Cmd0")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_Cmd0 +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }

            else if (source.Name == "CmdDecimalSeparator")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_CmdDecimalSeparator +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;
                TouchScreenTextCaret(0);
            }
            else if (source.Name == "CmdEpsilon")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_CmdEpsilon +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "CmdPlus")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_CmdPlus +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }
            else if (source.Name == "CmdMinus")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenText = TouchScreenText.Substring(0, CaretPos) + InstanceChangeKeyboard.Key_CmdMinus +
                                  TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);
                CaretPos++;

                TouchScreenTextCaret(0);
            }

            else if (source.Name == "CmdBackspace")
            {
                UpdateCaretPos(ref CaretPos);

                if (!string.IsNullOrEmpty(TouchScreenText) && CaretPos != 0)
                {
                    TouchScreenText = TouchScreenText.Substring(0, CaretPos - 1) +
                                      TouchScreenText.Substring(CaretPos, TouchScreenText.Length - CaretPos);

                    CaretPos--;

                    TouchScreenTextCaret(0);
                }
                else
                {
                    TouchScreenTextCaret(0);
                }
            }
            else if (source.Name == "CmdCaretLeft")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenTextCaret(-1);
            }
            else if (source.Name == "CmdCaretRight")
            {
                UpdateCaretPos(ref CaretPos);

                TouchScreenTextCaret(1);
            }
            else if (source.Name == "CmdEnter")
            {
                if (_InstanceObject != null)
                {
                    bCmdEnter = true;

                    if (_TextBoxInternal != null)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(_TextBoxInternal.Text.Trim()))
                            {
                                sInternalValue = _TextBoxInternal.Text.Trim();
                            }
                            else
                            {
                                sInternalValue = _TextBoxInternal.Text.Trim();
                            }
                        }
                        catch
                        {
                            sInternalValue = sMainValue;
                        }
                    }

                    CloseCommand(null);
                }
            }
        }
       

        public static async Task CloseCommand(object sender)//, RoutedEventArgs e)
        {
            var e = (RoutedEventArgs)sender;
            if (_InstanceObject != null)
            {
                _InstanceObject.Close();
                _InstanceObject = null;

                InstanceChangeKeyboard = null;
                _TextBoxInternal = null;
            }

            //var root = _TextBoxMain as AvaloniaObject;
            var root = _TextBoxMain as IHostedVisualTreeRoot;
            while (root != null && !(root is Window))
            {
                //root = root.GetValue(ParentProperty);
                root = root.Host?.GetVisualParent() as IHostedVisualTreeRoot;
            }
            

            if (root == null)
            {
                var w = new MainWindow();
                Application.Current.Run(w);
                //AvaloniaObject scope = FocusManager.GetFocusScope(_TextBoxMain);
                //FocusManager.SetFocusedElement(scope, w);
            }
            else
            {
                var w = root as AvaloniaObject;
                if (w != null)
                {
                   // w.SetValue(Avalonia.VisualTree.Visual.FocusableProperty, true);
                }
                //AvaloniaObject scope = FocusManager.Scope(_TextBoxMain);
                //FocusManager.Focus(w, NavigationMethod.Unspecified, scope);
            }


            if (bCmdEnter && sInternalValue != sMainValue)
            {
                if (_TextBoxMain != null)
                {
                    _TextBoxMain.Text = sInternalValue;
                  //  var bindingExpression = _TextBoxMain.GetObservable(TextBox.TextProperty);
                  //  if (bindingExpression != null)
                  //      bindingExpression.UpdateSource();
                }
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(AvaloniaObject depObj) where T : AvaloniaObject
        {
            if (depObj != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < count; i++)
                {
                    AvaloniaObject child = VisualTreeHelper.GetChildWithIndex(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            _TextBoxMain = sender as TextBox;
            if (_TextBoxMain == null)
                return;

            //TagKeyboard tag = null;

            try
            {
                //tag = (TagKeyboard)_TextBoxMain.Tag;

                sMainValue = _TextBoxMain.Text;

                sInternalValue = _TextBoxMain.Text;
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка преобразования в TagKeyboard");
            }

            //Проверка существования объекта tag и проверка требуемого уровня доступа
            //if (tag == null)
            //{
            //    Close
            //    (null, null);
            //    return;
            //}

            //if (!PW.Access(tag.Level))
            //{
            //    CloseCommand(null, null);
            //    return;
            //}

            //if (tag.Sim && !IVKMode.Simul)
            //{
            //    Utl.Message("Редактирование только в режиме имитации");
            //    CloseCommand(null, null);
            //    return;
            //}

            if (_InstanceObject == null)
            {
                ContentTp = GetContentType(_TextBoxMain);
                _InstanceObject = new NumpadTouchScreen();

                if (_TextBoxMain.Parent is StackPanel)
                {
                    foreach (TextBlock tb in FindVisualChildren<TextBlock>(_TextBoxMain.Parent))
                    {
                        if (_NumpadCaption != null)
                            _NumpadCaption.SetCaption(tb.Text);

                        break;
                    }
                }

                _TextBoxInternal.Text = sMainValue;

                if (_TextBoxInternal.Text.Length != 0)
                {
                    CaretPos = _TextBoxInternal.Text.Length;
                    TouchScreenTextCaret(0);
                }
                else
                {
                    CaretPos = 0;
                    TouchScreenText = "";
                    TouchScreenTextCaret(0);
                }

                LocatedKeyboardInToCorner();
                _InstanceObject.ShowDialog(_InstanceObject);//??
            }
        }

        private static void TouchScreenTextCaret(int caret)
        {
            int _CaretPos = CaretPos + caret;

            if (_CaretPos >= 0 && _CaretPos <= _TextBoxInternal.Text.Length)
            {
                CaretPos = _CaretPos;

                _TextBoxInternal.Focus();
                _TextBoxInternal.CaretIndex = CaretPos;
            }
            else
            {
                _TextBoxInternal.Focus();
                _TextBoxInternal.CaretIndex = CaretPos;
            }
        }

        private static void UpdateCaretPos(ref int caret)
        {
            caret = _TextBoxInternal.CaretIndex;
        }

        //Поместить виртуальну клавиатуру в правый нижний угол окна
        private static void LocatedKeyboardInToCorner()
        {
            var mainWindow = 
                (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            if (mainWindow != null)
            {
                _InstanceObject.Width = mainWindow.Width;
                _InstanceObject.Height = mainWindow.Height;
                _InstanceObject.Position = new PixelPoint(
                    (int)mainWindow.Position.X,
                    (int)mainWindow.Position.Y);
            }

            
            /*
            _InstanceObject.Left = Application.Current.MainWindow.Left;
            _InstanceObject.Top = Application.Current.MainWindow.Top;
            */
        }

        #region Объявление комманд RoutedUICommand

        public static readonly RoutedEvent Cmd1 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd1", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd2 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd2", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd3 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd3", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd4 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd4", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd5 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd5", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd6 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd6", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd7 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd7", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd8 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd8", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd9 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd9", RoutingStrategies.Bubble);
        public static readonly RoutedEvent Cmd0 = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("Cmd0", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdDecimalSeparator = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdDecimalSeparator ", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdEpsilon = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdEpsilon", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdPlus = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdPlus", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdMinus = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdMinus", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdBackspace = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdBackspace", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdCaretLeft = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdCaretLeft", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdCaretRight = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdCaretRight", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdEnter = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdEnter", RoutingStrategies.Bubble);
        public static readonly RoutedEvent CmdClose = RoutedEvent.Register<NumpadTouchScreen, RoutedEventArgs>("CmdClose", RoutingStrategies.Bubble);


        #endregion

        #region реализация комманд SetCommandBinding
        
        private static void SetCommandBinding()
        {

            var viewModel = new HelpCommandBinding();
            var Cb1 = viewModel.Cmd1.Subscribe(_ => { });
            var Cb2 = viewModel.Cmd2.Subscribe(_ => { });
            var Cb3 = viewModel.Cmd3.Subscribe(_ => { });
            var Cb4 = viewModel.Cmd4.Subscribe(_ => { });
            var Cb5 = viewModel.Cmd5.Subscribe(_ => { });
            var Cb6 = viewModel.Cmd6.Subscribe(_ => { });
            var Cb7 = viewModel.Cmd7.Subscribe(_ => { });
            var Cb8 = viewModel.Cmd8.Subscribe(_ => { });
            var Cb9 = viewModel.Cmd9.Subscribe(_ => { });
            var Cb0 = viewModel.Cmd0.Subscribe(_ => { });
            var CbDecimalSeparator = viewModel.CmdDecimalSeparator.Subscribe(_ => { });
            var CbEpsilon = viewModel.CmdEpsilon.Subscribe(_ => { });
            var CbPlus = viewModel.CmdPlus.Subscribe(_ => { });
            var CbMinus = viewModel.CmdMinus.Subscribe(_ => { });
            var CbBackspace = viewModel.CmdBackspace.Subscribe(_ => { });
            var CbCaretLeft = viewModel.CmdCaretLeft.Subscribe(_ => { });
            var CbCaretRight = viewModel.CmdCaretRight.Subscribe(_ => { });
            var CbEnter = viewModel.CmdEnter.Subscribe(_ => { });
            var CbClose = viewModel.CmdClose.Subscribe(_ => { });


            /*
            AvaloniaProperty.RegisterAttached<NumpadTouchScreen>("Cb1");
            CommandManager.RegisterClassCommandBinding(typeof(NumpadTouchScreen), Cb2);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb3);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb4);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb5);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb6);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb7);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb8);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb9);
            RoutedEvent.Register(typeof(NumpadTouchScreen), Cb0);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbDecimalSeparator);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbEpsilon);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbPlus);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbMinus);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbBackspace);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbCaretLeft);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbCaretRight);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbEnter);
            RoutedEvent.Register(typeof(NumpadTouchScreen), CbClose);
            */
        }

        #endregion

        private void Grid_PreviewMouseLeftButtonDown(object sender, PointerPressedEventArgs e)
        {
            if (e.ClickCount == 1) // for double-click, remove this condition if only want single click
            {
                var point = e.GetPosition(this);
                var grid = (Grid)sender;

                int row = 0;
                int col = 0;
                double accumulatedHeight = 0.0;
                double accumulatedWidth = 0.0;

                // calc row mouse was over
                foreach (var rowDefinition in grid.RowDefinitions)
                {
                    accumulatedHeight += rowDefinition.ActualHeight;
                    if (accumulatedHeight >= point.Y)
                        break;
                    row++;
                }

                // calc col mouse was over
                foreach (var columnDefinition in grid.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }

                if (!(row == 1 && col == 1))
                {
                    CloseCommand(null);
                }
            }
        }

        private class NumpadCaption : INotifyPropertyChanged
        {
            public NumpadCaption()
            {
                _Caption = "";
            }

            public void SetCaption(string sCaption)
            {
                _Caption = sCaption;
                OnPropertyChanged("Caption");
            }

            private string _Caption;
            public string Caption
            {
                get { return _Caption; }
            }

            public string this[string propertyName]
            {
                get { return null; }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

