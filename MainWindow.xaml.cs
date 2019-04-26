using HtmlAgilityPack;
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

namespace AnimeList
{

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<AnimeInfo> items = new List<AnimeInfo>();

        public MainWindow()
        {
            InitializeComponent();
            lvItems.ItemsSource = items;
            Load();
        }

        private async void Load()
        {
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            var page = web.Load(@"http://nnmclub.to/forum/portal.php?c=1");
            var refs = page.DocumentNode.SelectNodes("//table[contains(@class, \"pline\")]//a[contains(concat(\" \", normalize-space(@class), \" \"), \" pgenmed \")]");
            foreach (var rf in refs)
            {
                items.Add(new AnimeInfo { Title = rf.InnerText });
            }
        }
    }
}
