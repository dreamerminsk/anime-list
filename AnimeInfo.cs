﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList
{
    public class AnimeInfo : INotifyPropertyChanged
    {

        public AnimeInfo()
        {
        }

        public AnimeInfo(int id, string engTitle, string rusTitle, int year, int episodes, string topic)
        {
            ID = id;
            EngTitle = engTitle;
            RusTitle = rusTitle;
            Year = year;
            Episodes = episodes;
            Topic = topic;
        }

        public int ID
        {
            get { return mId; }
            set
            {
                mId = value;
                OnPropertyChanged("ID");
            }
        }

        public string EngTitle
        {
            get { return mEngTitle; }
            set
            {
                mEngTitle = value;
                OnPropertyChanged("EngTitle");
            }
        }

        public string RusTitle
        {
            get { return mRusTitle; }
            set
            {
                mRusTitle = value;
                OnPropertyChanged("RusTitle");
            }
        }

        public int Year
        {
            get { return mYear; }
            set
            {
                mYear = value;
                OnPropertyChanged("Year");
            }
        }

        public int Episodes
        {
            get { return mEpisodes; }
            set
            {
                mEpisodes = value;
                OnPropertyChanged("Episodes");
            }
        }

        public string Topic
        {
            get { return mTopic; }
            set
            {
                mTopic = value;
                OnPropertyChanged("Topic");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private int mId;
        private string mEngTitle;
        private string mRusTitle;
        private int mYear;
        private int mEpisodes;
        private string mTopic;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
