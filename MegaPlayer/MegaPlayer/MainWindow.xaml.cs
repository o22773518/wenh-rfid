using CG.Web.MegaApiClient;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MegaPlayer {
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : ModernWindow {
        
        public MainWindow() {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;//永遠最大化
            this.StateChanged += MainWindow_StateChanged;
            
        }

        public void MainWindow_StateChanged(object sender ,EventArgs e) {
            this.WindowState = System.Windows.WindowState.Maximized;//永遠最大化
        }

        public void Nav(string Target) {
            ContentSource = new Uri(Target);
        }
    }
}
