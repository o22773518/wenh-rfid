using CG.Web.MegaApiClient;
using FirstFloor.ModernUI.Windows.Controls;
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

namespace MegaPlayer.Pages {
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class Login : Page {

        
        public Login() {
            InitializeComponent();
            
        }

        private void Button_Click(object sender ,RoutedEventArgs e) {
            BBCodeBlock bs = new BBCodeBlock();
            try {
                App.client.Login(Text_UserID.Text ,Text_Password.Password);
                bs.LinkNavigator.Navigate(new Uri("/Pages/Player.xaml" ,UriKind.Relative) ,this);
            } catch(Exception error) {
                ModernDialog.ShowMessage("登入錯誤!!\n請檢查您的帳號或密碼是否正確", FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
