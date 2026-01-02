using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sampler.Codex.AudioUI.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy KnobControl.xaml
    /// </summary>
    public partial class KnobControl : UserControl
    {

            private bool _dragging;
            private Point _lastPos;

            public KnobControl() { InitializeComponent(); }


        #region DP: Value (0..1)

                    public double Value
                    {
                        get => (double)GetValue(ValueProperty);
                        set => SetValue(ValueProperty, value);
                    }

                    public static readonly DependencyProperty ValueProperty =
                        DependencyProperty.Register(
                            nameof(Value),
                            typeof(double),
                            typeof(KnobControl),
                            new PropertyMetadata(0.0)
                        );

        #endregion


        #region DP: Label

                public string Label
                {
                    get => (string)GetValue(LabelProperty);
                    set => SetValue(LabelProperty, value);
                }

                public static readonly DependencyProperty LabelProperty =
                    DependencyProperty.Register(
                        nameof(Label),
                        typeof(string),
                        typeof(KnobControl),
                        new PropertyMetadata(string.Empty)
                    );

        #endregion


        #region Mouse handling

                private void Root_MouseDown(object sender, MouseButtonEventArgs e)   {
                    _dragging = true;
                    _lastPos = e.GetPosition(this);
                    Mouse.Capture(this);
                }

                private void Root_MouseUp(object sender, MouseButtonEventArgs e)       {
                    _dragging = false;
                    Mouse.Capture(null);
                }

                private void Root_MouseMove(object sender, MouseEventArgs e)       {
                    if (!_dragging) return;

                    var pos = e.GetPosition(this);
                    double delta = (_lastPos.Y - pos.Y) * 0.005;
                    _lastPos = pos;

                    Value = Math.Clamp(Value + delta, 0.0, 1.0);
                }

        #endregion

            public string DisplayValue
            {
                get => (string)GetValue(DisplayValueProperty);
                set => SetValue(DisplayValueProperty, value);
            }

            public static readonly DependencyProperty DisplayValueProperty =
                DependencyProperty.Register(
                    nameof(DisplayValue),
                    typeof(string),
                    typeof(KnobControl),
                    new PropertyMetadata("")
                );

    }
}
