﻿using Android.Graphics;

namespace Subsonic.Client.Android
{
    public class AndroidNowPlayingItem : NowPlayingItem 
    {
        private Bitmap _image;

        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
    }
}

