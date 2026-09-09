namespace Fooddaily.Portable.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Fooddaily.Portable.Data;

    public class BaseViewModel : INotifyPropertyChanged
    {
        private string title = string.Empty;
        public const string TitlePropertyName = Data.Constants.TITLE;

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        private string subtitle = string.Empty;
        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public const string SubtitlePropertyName = Data.Constants.SUB;
        public string Subtitle
        {
            get => this.subtitle;
            set => this.SetProperty(ref this.subtitle, value);
        }

        private string icon;
        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public const string IconPropertyName = Data.Constants.ICON;
        public string Icon
        {
            get => this.icon;
            set => this.SetProperty(ref this.icon, value);
        }

        private bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                if (this.SetProperty(ref this.isBusy, value))
                    this.IsNotBusy = !this.isBusy;
            }
        }

        private bool isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get => this.isNotBusy;
            private set => this.SetProperty(ref this.isNotBusy, value);
        }

        private bool canLoadMore = true;
        /// <summary>
        /// Gets or sets if we can load more.
        /// </summary>
        public const string CanLoadMorePropertyName = Data.Constants.LOAD;
        public bool CanLoadMore
        {
            get => this.canLoadMore;
            set => this.SetProperty(ref this.canLoadMore, value);
        }

        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = string.Empty,
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            onChanged?.Invoke();

            this.OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged([CallerMemberName] string propertyName = string.Empty)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

