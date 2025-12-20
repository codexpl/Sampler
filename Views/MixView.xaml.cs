using Sampler.ViewModels;

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

namespace Sampler.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MixView.xaml
    /// </summary>
    public partial class MixView : UserControl
    {
                private bool _dragging = false;
                private Point _lastPos;
                public double Value { get; set; } = 0.5; // 0..1
                public double KnobValue { get; set; } = 0.5; // 0..1





                public MixView()
                {
                    InitializeComponent();
                    //DataContext = new MixViewModel(App.ViewModel);
                }


private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
{
    _dragging = true;
    _lastPos = e.GetPosition(this);
    Mouse.Capture((UIElement)sender);
}

private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
{
    _dragging = false;
    Mouse.Capture(null);
}

private void Knob_MouseMove(object sender, MouseEventArgs e)
{
    if (!_dragging) return;

    var pos = e.GetPosition(this);
    double delta = (_lastPos.Y - pos.Y) * 0.005; // czułość
    _lastPos = pos;

    KnobValue = Math.Clamp(KnobValue + delta, 0, 1);

    // mapowanie 0..1 → -135..135 stopni
    double angle = -135 + KnobValue * 270;
    PointerRotate.Angle = angle;

    // aktualizacja tekstu
    ValueText.Text = KnobValue.ToString("0.00");
}




    }
}
