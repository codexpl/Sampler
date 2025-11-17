using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Models {
        public class AudioSample : INotifyPropertyChanged
        {
            public int Index { get; set; }

            private int _left;
            public int Left
            {
                get => _left;
                set
                {
                    if (_left != value)
                    {
                        _left = value;
                        OnPropertyChanged(nameof(Left));
                    }
                }
            }

            private int _right;
            public int Right
            {
                get => _right;
                set
                {
                    if (_right != value)
                    {
                        _right = value;
                        OnPropertyChanged(nameof(Right));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
}
