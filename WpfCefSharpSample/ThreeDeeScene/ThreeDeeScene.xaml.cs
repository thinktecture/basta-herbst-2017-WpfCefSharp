using System.Windows;
using System.Windows.Controls;

namespace WpfCefSharpSample.ThreeDeeScene
{
    /// <summary>
    /// Interaction logic for ThreeDeeScene.xaml
    /// </summary>
    public partial class ThreeDeeScene : UserControl
    {
        public ThreeDeeScene()
        {
            InitializeComponent();
        }
        private void ThreeDeeScene_OnLoaded(object sender, RoutedEventArgs e)
        {
            Trackball trackball = new Trackball() { EventSource = this };
            PictureCube3D.Camera.Transform = trackball.Transform;
        }
    }
}
