using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CsharpCalculator.Views
{
    public partial class LicenseKeyDialog : Window
    {
        public LicenseKeyDialog()
        {
            InitializeComponent();
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