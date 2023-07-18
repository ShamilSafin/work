using Avalonia;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Base;
using System.Threading.Tasks;

namespace EMC07.ControlsUI
{
    static class Utl
    {
        static string InfoProgName = "Тест";

        public async static void MessageEr(string s)
        {
            if (Application.Current.CheckAccess())
            {
                MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, ButtonEnum.Ok, Icon.Error);
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, ButtonEnum.Ok, Icon.Error);
                });
            }
        }
        
        public async static void MessageEr(string s, string longMsg)
        {
            if (Application.Current.CheckAccess())
            {
                MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, ButtonEnum.Ok, Icon.Error);
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    //MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, longMsg, ButtonEnum.OkCancel);

                    MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, ButtonEnum.OkCancel);
                });
            }
        }

        public static async Task<bool> YesNo(string s)
        {
           ButtonResult result = ButtonResult.No;

            if (Application.Current.CheckAccess())
            {
                IMsBox<ButtonResult> messageBox = MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, ButtonEnum.YesNo, Icon.Question);
                return await messageBox.ShowAsPopupAsync(null) == ButtonResult.Yes;
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    IMsBox<ButtonResult> messageBox = MessageBoxManager.GetMessageBoxStandard(InfoProgName, s, ButtonEnum.YesNo, Icon.Question);
                    return await messageBox.ShowAsPopupAsync(null) == ButtonResult.Yes;
                });   
            }

            return result == ButtonResult.Yes;
        }
    }
   
}