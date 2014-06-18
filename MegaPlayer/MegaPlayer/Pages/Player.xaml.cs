using AxWMPLib;
using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MegaPlayer.Pages {
    /// <summary>
    /// Player.xaml 的互動邏輯
    /// </summary>
    public partial class Player : Page {
        WindowsFormsHost FormHost;
        AxWindowsMediaPlayer MainPlayer;

        public Player() {
            InitializeComponent();

            //建立MediaPlayer播放器
            MainPlayer = new AxWindowsMediaPlayer();
            FormHost = new WindowsFormsHost();
            PlayGrid.Children.Add(FormHost);
            FormHost.Child = MainPlayer;
            FormHost.Margin = new Thickness(0);//Fill Player Grid
            MainPlayer.BeginInit();
            MainPlayer.EndInit();

            MediaList.SelectionChanged += MediaList_SelectionChanged;//新增一個事件:針對項目選擇改變時

            //取得所有節點並且篩選出是檔案的節點(去除資料夾節點)
            var k = App.client.GetNodes();
            foreach (var temp in k){
                if (temp.Type != CG.Web.MegaApiClient.NodeType.File) continue;
                MediaList.Items.Add(temp);
            }
        }


        int count = 0;
        void MediaList_SelectionChanged(object sender, SelectionChangedEventArgs e){
            ThreadStart k = new ThreadStart(() =>{
                Node temp = null;
                this.Dispatcher.Invoke(() =>{
                    temp = (Node)MediaList.SelectedItem;
                    PlayButton.IsEnabled = false;
                });
                count++;
                File.Delete("temp" + count + ".mp4");

                time = 10;
                Wait(() =>{
                    this.Dispatcher.Invoke(() => { 
                        
                        PlayButton.Content = "播放 - " + time;
                        if (time <= 0) PlayButton.Content = "播放";
                    });
                }, 1000,10);
                App.client.DownloadFile(temp, "temp" + count + ".mp4");
            });
            new Thread(k).Start();
            

           
        }

        int time;
        public void Wait(Action Callback, int f,int count)
        {
            ThreadStart k = new ThreadStart(()=>{
                for (int i = 0; i < count; i++)
                {
                    time--;
                    Thread.Sleep(f);
                    Callback();
                }
                this.Dispatcher.Invoke(() =>
                {
                    PlayButton.IsEnabled = true;
                });
            });
            new Thread(k).Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e){
            MainPlayer.URL = "temp" + count + ".mp4";
        }
    }
}
