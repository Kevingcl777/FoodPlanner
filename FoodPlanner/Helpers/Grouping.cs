namespace Fooddaily.Portable.Helpers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Grouping<TKey, T> : ObservableCollection<T>
    {
        public TKey Key { get; private set; }

        public Grouping(TKey key, IEnumerable<T> items)
        {
            this.Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
