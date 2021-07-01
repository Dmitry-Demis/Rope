using System.ComponentModel;
using System.Windows;

namespace Rope.Model
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : INotifyPropertyChanged
                                           where TView : IDialog;
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : INotifyPropertyChanged;
        MessageBoxResult ShowMessageBoxDialog(string caption, string text);
        void ShowMessageBoxDialog(string caption);
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog(string filter);  // открытие файла
        bool SaveFileDialog(string filter);  // сохранение файла
    }
}
