using System.Windows;

namespace harunadev.OSCRelay
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            AboutText.Text = $"OSCRelay 1.0\nMade by harunadev\n\nOpenVR: https://github.com/ValveSoftware/openvr\nJSON: https://www.newtonsoft.com/json\nModernWpfUI: https://github.com/Kinnara/ModernWpf\nOSC: https://github.com/tilde-love/osc-core";
        }
    }
}
