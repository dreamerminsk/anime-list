using HtmlAgilityPack;
using LiteDB;
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

    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            using (var db = new LiteDatabase(@"anime-list.litedb"))
            {
                var col = db.GetCollection<AnimeInfo>("anime");
                col.EnsureIndex(x => x.Topic);
            }
        }

        private void TopicGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            AnimeInfo anime = (AnimeInfo)e.Row.Item;
            Console.WriteLine(anime.Episodes);
            Console.WriteLine(e.Column.Header);
            TextBox text = (TextBox)e.EditingElement;
            Console.WriteLine(text.Text);
            if (String.Equals(e.Column.Header, "EngTitle"))
            {
                anime.EngTitle = text.Text;
            }
            if (String.Equals(e.Column.Header, "RusTitle"))
            {
                anime.RusTitle = text.Text;
            }
            if (String.Equals(e.Column.Header, "Year"))
            {
                anime.Year = int.Parse(text.Text);
            }
            if (String.Equals(e.Column.Header, "Episodes"))
            {
                anime.Episodes = int.Parse(text.Text);
            }
            if (String.Equals(e.Column.Header, "Topic"))
            {
                return;
            }
            if (String.Equals(e.Column.Header, "ID"))
            {
                return;
            }
            using (var db = new LiteDatabase(@"C:\Users\User\YandexDisk\anime-list.litedb"))
            {
                var col = db.GetCollection<AnimeInfo>("anime");
                col.Update(anime);
            }
        }
    }
}