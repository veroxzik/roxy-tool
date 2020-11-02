using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace roxy_tool.Views
{
    public class LicenseWindow : Window
    {
        public LicenseWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
