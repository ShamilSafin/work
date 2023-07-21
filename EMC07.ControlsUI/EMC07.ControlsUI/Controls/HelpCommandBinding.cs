using ReactiveUI;
using System.Reactive;
using EMC07.ControlsUI;
using Avalonia.Interactivity;
using System.Threading.Tasks;

namespace EMC07.ControlsUI.CommandBinding
{
    public class HelpCommandBinding : ReactiveObject
    {
        #region реализация комманд ReactiveCommand
        public ReactiveCommand<object, Task<Unit>> Cmd1 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd2 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd3 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd4 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd5 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd6 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd7 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd8 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd9 { get; }
        public ReactiveCommand<object, Task<Unit>> Cmd0 { get; }
        public ReactiveCommand<object, Task<Unit>> CmdDecimalSeparator { get; }
        public ReactiveCommand<object, Task<Unit>> CmdEpsilon { get; }
        public ReactiveCommand<object, Task<Unit>> CmdPlus { get; }
        public ReactiveCommand<object, Task<Unit>> CmdMinus { get; }

        public ReactiveCommand<object, Task<Unit>> CmdBackspace { get; }

        public ReactiveCommand<object, Task<Unit>> CmdCaretLeft { get; }

        public ReactiveCommand<object, Task<Unit>> CmdCaretRight { get; }

        public ReactiveCommand<object, Task<Unit>> CmdEnter { get; }

        public ReactiveCommand<object, Task<Unit>> CmdClose { get; }
        #endregion

        public HelpCommandBinding()
        {
            Cmd1 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });

            Cmd2 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd3 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd4 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd5 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd6 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd7 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd8 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd9 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            Cmd0 = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdDecimalSeparator = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdEpsilon = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdPlus = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdMinus = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdBackspace = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdCaretLeft = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdCaretRight = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdEnter = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.RunCommand(sender);
                return Unit.Default;
            });
            CmdClose = ReactiveCommand.Create<object, Task<Unit>>(async (sender) =>
            {
                await NumpadTouchScreen.CloseCommand(sender);
                return Unit.Default;
            });
        }
    }
}
