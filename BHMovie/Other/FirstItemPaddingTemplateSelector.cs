using System;
using BHMovie.Model;

namespace BHMovie.Other
{
    public class FirstItemPaddingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstItemTemplate { get; set; }
        public DataTemplate DefaultItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var collectionView = (CollectionView)container;
            var isFirstItem = ((ObservableCollection<Movie>)collectionView.ItemsSource)?.IndexOf(item as Movie) == 0;

            return isFirstItem ? FirstItemTemplate : DefaultItemTemplate;
        }
    }
}

