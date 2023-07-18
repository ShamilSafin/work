using Avalonia;
using Avalonia.Media;
using System.Globalization;

namespace EMC07.ControlsUI
{
    public class ClassChangeNumpad : AvaloniaObject
    {
        // public static DependencyProperty Key_Cmd1_Property = DependencyProperty.Register("Key_Cmd1", typeof(String),
        //     typeof(ClassChangeNumpad));

        public static readonly AvaloniaProperty<string> Key_Cmd1Property =AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd1));

        public static readonly AvaloniaProperty<string> Key_Cmd2Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd2));

        public static readonly AvaloniaProperty<string> Key_Cmd3Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd3));

        public static readonly AvaloniaProperty<string> Key_Cmd4Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd4));

        public static readonly AvaloniaProperty<string> Key_Cmd5Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd5));

        public static readonly AvaloniaProperty<string> Key_Cmd6Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd6));

        public static readonly AvaloniaProperty<string> Key_Cmd7Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd7));

        public static readonly AvaloniaProperty<string> Key_Cmd8Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd8));

        public static readonly AvaloniaProperty<string> Key_Cmd9Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd9));

        public static readonly AvaloniaProperty<string> Key_Cmd0Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_Cmd0));

        public static readonly AvaloniaProperty<string> Key_CmdDecimalSeparator_Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_CmdDecimalSeparator_Property));

        public static readonly AvaloniaProperty<string> Key_CmdEpsilon_Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_CmdEpsilon_Property));

        public static readonly AvaloniaProperty<string> Key_CmdPlus_Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_CmdPlus_Property));

        public static readonly AvaloniaProperty<string> Key_CmdMinus_Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_CmdMinus_Property));

        public static readonly AvaloniaProperty<string> Key_CmdBackspace_Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_CmdBackspace_Property));

        public static readonly AvaloniaProperty<string> Key_CmdPlusMinus_Property = AvaloniaProperty.Register<ClassChangeNumpad, string>(nameof(Key_CmdPlusMinus_Property));


        public static readonly AvaloniaProperty<SolidColorBrush> BackgroundFixKey_Property = AvaloniaProperty.Register<ClassChangeNumpad, SolidColorBrush>(nameof(BackgroundFixKey_Property));

        public static readonly AvaloniaProperty<SolidColorBrush> BackgroundKey_Property = AvaloniaProperty.Register<ClassChangeNumpad, SolidColorBrush>(nameof(BackgroundKey_Property));

        public static readonly AvaloniaProperty<bool> EpsilonKeyVisibility_Property = AvaloniaProperty.Register<ClassChangeNumpad, bool>(nameof(EpsilonKeyVisibility_Property));

        public static readonly AvaloniaProperty<bool> PlusKeyVisibility_Property = AvaloniaProperty.Register<ClassChangeNumpad, bool>(nameof(PlusKeyVisibility_Property));

        public static readonly AvaloniaProperty<bool> MinusKeyVisibility_Property = AvaloniaProperty.Register<ClassChangeNumpad, bool>(nameof(MinusKeyVisibility_Property));

        public static readonly AvaloniaProperty<int> BtnEnterRowSpan_Property = AvaloniaProperty.Register<ClassChangeNumpad, int>(nameof(BtnEnterRowSpan_Property));

        public static readonly AvaloniaProperty<int> BtnEnterRow_Property = AvaloniaProperty.Register<ClassChangeNumpad, int>(nameof(BtnEnterRow_Property));


        public ClassChangeNumpad()
        {
            Set_Numpad();
        }
        public string Key_Cmd1
        {
            get => (string)GetValue(Key_Cmd1Property);
            set => SetValue(Key_Cmd1Property, value);
        }
        /*
        public String Key_Cmd1
        {
            set { SetValue(Key_Cmd1_Property, value); }
            get { return (String)GetValue(Key_Cmd1_Property); }
        }
        */
        public string Key_Cmd2
        {
            get => (string)GetValue(Key_Cmd2Property);
            set => SetValue(Key_Cmd2Property, value);
        }

        public string Key_Cmd3
        {
            get => (string)GetValue(Key_Cmd3Property);
            set => SetValue(Key_Cmd3Property, value);
        }

        public string Key_Cmd4
        {
            get => (string)GetValue(Key_Cmd4Property);
            set => SetValue(Key_Cmd4Property, value);
        }

        public string Key_Cmd5
        {
            get => (string)GetValue(Key_Cmd5Property);
            set => SetValue(Key_Cmd5Property, value);
        }

        public string Key_Cmd6
        {
            get => (string)GetValue(Key_Cmd6Property);
            set => SetValue(Key_Cmd6Property, value);
        }

        public string Key_Cmd7
        {
            get => (string)GetValue(Key_Cmd7Property);
            set => SetValue(Key_Cmd7Property, value);
        }

        public string Key_Cmd8
        {
            get => (string)GetValue(Key_Cmd8Property);
            set => SetValue(Key_Cmd8Property, value);
        }

        public string Key_Cmd9
        {
            get => (string)GetValue(Key_Cmd9Property);
            set => SetValue(Key_Cmd9Property, value);
        }

        public string Key_Cmd0
        {
            get => (string)GetValue(Key_Cmd0Property);
            set => SetValue(Key_Cmd0Property, value);
        }

        public string Key_CmdBackspace
        {
            get => (string)GetValue(Key_CmdBackspace_Property);
            set => SetValue(Key_CmdBackspace_Property, value);
        }

        public string Key_CmdPlus
        {
            get => (string)GetValue(Key_CmdPlus_Property);
            set => SetValue(Key_CmdPlus_Property, value);
        }

        public string Key_CmdMinus
        {
            get => (string)GetValue(Key_CmdMinus_Property);
            set => SetValue(Key_CmdMinus_Property, value);
        }

        public string Key_CmdPlusMinus
        {
            get => (string)GetValue(Key_CmdPlusMinus_Property);
            set => SetValue(Key_CmdPlusMinus_Property, "±");
        }

        public string Key_CmdEpsilon
        {
            get => (string)GetValue(Key_CmdEpsilon_Property);
            set => SetValue(Key_CmdEpsilon_Property, "E");
        }

        public string Key_CmdDecimalSeparator
        {
            get => (string)GetValue(Key_CmdDecimalSeparator_Property);
            set => SetValue(Key_CmdDecimalSeparator_Property, value);
        }

        public SolidColorBrush BackgroundFixKey
        {
            get => (SolidColorBrush)GetValue(BackgroundFixKey_Property);
            set => SetValue(BackgroundFixKey_Property, value);
        }

        public SolidColorBrush BackgroundKey
        {
            get => (SolidColorBrush)GetValue(BackgroundKey_Property);
            set => SetValue(BackgroundKey_Property, value);
        }

        


        public bool EpsilonKeyVisibility
        {
            get => (bool)GetValue(EpsilonKeyVisibility_Property);
            set => SetValue(EpsilonKeyVisibility_Property, value); 
        }


        public bool PlusKeyVisibility
        {
            get => (bool)GetValue(PlusKeyVisibility_Property);
            set => SetValue(PlusKeyVisibility_Property, value); 
        }


        public bool MinusKeyVisibility
        {
            get => (bool)GetValue(MinusKeyVisibility_Property);
            set => SetValue(MinusKeyVisibility_Property, value); 
        }

        public int BtnEnterRowSpan
        {
            get => (int)GetValue(BtnEnterRowSpan_Property);
            set => SetValue(BtnEnterRowSpan_Property, value);
        }


        public int BtnEnterRow
        {
            get => (int)GetValue(BtnEnterRow_Property);
            set => SetValue(BtnEnterRow_Property, value);
        }


        private void Set_Numpad()
        {
            Key_Cmd1 = "1";
            Key_Cmd2 = "2";
            Key_Cmd3 = "3";
            Key_Cmd4 = "4";
            Key_Cmd5 = "5";
            Key_Cmd6 = "6";
            Key_Cmd7 = "7";
            Key_Cmd8 = "8";
            Key_Cmd9 = "9";
            Key_Cmd0 = "0";
            Key_CmdEpsilon = "E";
            Key_CmdBackspace = "Backspace";
            Key_CmdPlus = "+";
            Key_CmdMinus = "-";


            switch (NumpadTouchScreen.ContentTp)
            {
                case NumpadTouchScreen.ContentType.IPAddress:
                    Key_CmdDecimalSeparator = ".";
                    EpsilonKeyVisibility =
                    MinusKeyVisibility =
                    PlusKeyVisibility = false; 
                    BtnEnterRowSpan = 3;
                    BtnEnterRow = 2;
                    break;
                default:
                    Key_CmdDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    BtnEnterRowSpan = 2;
                    BtnEnterRow = 3;
                    break;

            }
        }
    }
}
