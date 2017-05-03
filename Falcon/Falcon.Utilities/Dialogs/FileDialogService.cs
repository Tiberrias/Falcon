using Falcon.Utilities.Dialogs.Interfaces;
using Microsoft.Win32;

namespace Falcon.Utilities.Dialogs
{
    public class FileDialogService : IFileDialogService
    {
        public string GetFileNameDialog()
        {
            var dialog = new OpenFileDialog();
            
            var dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                return dialog.FileName;
            }
            return null;
        }
    }
}