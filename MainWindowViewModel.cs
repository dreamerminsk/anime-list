﻿using HtmlAgilityPack;
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
        private ObservableCollection<AnimeInfo> items = new ObservableCollection<AnimeInfo>();

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

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      MessageBox.Show("The Window is Fooing...");
                  }));
            }
        }

        void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The Window is Fooing...");
        }

        private async void Load()
        {
            var page = await GetPage(@"http://nnmclub.to/forum/portal.php?c=1");
            var refs = page.DocumentNode.SelectNodes("//table[contains(@class, \"pline\")]//a[contains(concat(\" \", normalize-space(@class), \" \"), \" pgenmed \")]");
            foreach (var rf in refs)
            {
                items.Add(new AnimeInfo { Topic = rf.InnerText });
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
