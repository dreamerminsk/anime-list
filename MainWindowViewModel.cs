using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AnimeList
{

    public class MainWindowViewModel : INotifyPropertyChanged
    {

        

        public event PropertyChangedEventHandler PropertyChanged;

        private int currentPage = 0;

        private ObservableCollection<AnimeInfo> items = new ObservableCollection<AnimeInfo>();

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public ObservableCollection<AnimeInfo> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public MainWindowViewModel()
        {
            Load();
        }

        private RelayCommand prevCommand;
        public RelayCommand PrevCommand
        {
            get
            {
                return prevCommand ??
                  (prevCommand = new RelayCommand(obj =>
                  {
                      if (CurrentPage > 0)
                      {
                          CurrentPage -= 1;
                          Load();
                      }
                  }));
            }
        }

        private RelayCommand nextCommand;
        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand ??
                  (nextCommand = new RelayCommand(obj =>
                  {
                      CurrentPage += 1;
                      Load();
                  }));
            }
        }

        private async void Load()
        {
            var html = await GetPage(@"http://nnmclub.to/forum/portal.php?c=1&start=" + 20 * currentPage);
            var refs = html.DocumentNode.SelectNodes("//table[contains(@class, \"pline\")]//td[contains(@class, \"pcatHead\")]//a[contains(concat(\" \", normalize-space(@class), \" \"), \" pgenmed \")]");
            items.Clear();
            foreach (var rf in refs)
            {
                if (!rf.InnerText.Equals("Популярные раздачи"))
                {
                    items.Add(new AnimeInfo { Topic = rf.InnerText });
                }
            }
            return;
        }

        private Task<HtmlDocument> GetPage(string uri)
        {
            var task = Task<HtmlDocument>.Factory.StartNew(() =>
            {
                HtmlWeb web = new HtmlWeb();
                web.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                return web.Load(uri);
            });
            return task;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
