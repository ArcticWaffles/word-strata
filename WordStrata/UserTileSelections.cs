using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;

namespace WordStrata
{
    public class UserTileSelections : INotifyPropertyChanged
    {
        public UserTileSelections()
        {
            Selections.CollectionChanged += OnCollectionChanged;
        }
        
        //List of tiles the user has clicked, removed when they are unclicked.
        private ObservableCollection<TileViewModel> selections = new ObservableCollection<TileViewModel>();
        public ObservableCollection<TileViewModel> Selections
        {
            get
            {
                return (selections);
            }

            set
            {
                if (value != selections)
                {
                    selections = value;
                    OnPropertyChanged(null);
                }
            }
        }

        //The last tile in the list. Used for determining which tiles are clickable.
        public TileViewModel CurrentTile
        {
            get
            {
                if (Selections == null || Selections.Count == 0)
                {
                    return null;
                }
                else
                {
                    return Selections.Last();
                }
            }

        }

        //The word the user is building
        private string userWord;
        public string UserWord
        {
            get
            {
                userWord = "";
                foreach (var tile in Selections)
                {
                    userWord += tile.TheTile.Letter;
                }
                return userWord;
            }

            set
            {
                if (value != userWord)
                {
                    userWord = value;
                    OnPropertyChanged("UserWord");
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(null);
        }
    }

}
