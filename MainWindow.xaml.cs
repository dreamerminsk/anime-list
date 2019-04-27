using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AnimeList
{

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<AnimeInfo> items = new ObservableCollection<AnimeInfo>();

        public MainWindow()
        {
            InitializeComponent();
            topicGrid.ItemsSource = items;
            Load();
        }

        private async void Load()
        {
            var page = await GetPage(@"http://nnmclub.to/forum/portal.php?c=1");
            var refs = page.DocumentNode.SelectNodes("//table[contains(@class, \"pline\")]//a[contains(concat(\" \", normalize-space(@class), \" \"), \" pgenmed \")]");
            foreach (var rf in refs)
            {
                items.Add(new AnimeInfo { Topic = rf.InnerText });
            }
        }

        private Task<HtmlDocument> GetPage(string uri)
        {
            var task = Task<HtmlDocument>.Factory.StartNew(() => {
                HtmlWeb web = new HtmlWeb();
                web.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                return web.Load(uri);
            });
            return task;
        }
    }
}
