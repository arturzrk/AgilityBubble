using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AgilityBubble.Logic;
using AgilityBubble.UI.Annotations;

namespace AgilityBubble.UI
{
    public class MultiDictionaryViewModel : INotifyPropertyChanged
    {
        private readonly MultiDictionary _multiDictionary;
        private int _codeChangedClickCount;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Code => _multiDictionary.Code;
        public Command ChangeCodeCommand { get; private set; }

        public MultiDictionaryViewModel()
        {
            _multiDictionary = new MultiDictionary("Sample", "Sample multi dictionary", true);
            ChangeCodeCommand = new Command(() => ChangeCode());
        }

        private void ChangeCode()
        {
            _multiDictionary.ChangeCodeTo($"Code changed Clicked count = {++_codeChangedClickCount}");
            OnPropertyChanged(nameof(Code));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
