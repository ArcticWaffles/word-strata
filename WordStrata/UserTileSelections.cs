using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;
using Core;
using Solve;

namespace WordStrata
{
    /// <summary>
    /// Keeps track of tiles the player is currently using
    /// </summary>
    public class UserTileSelections : INotifyPropertyChanged
    {
        public UserTileSelections()
        {
            Selections.CollectionChanged += OnCollectionChanged;
        }
        
        /// <summary> List of tiles currently selected. </summary>
        private ObservableCollection<Tile> selections = new ObservableCollection<Tile>();
        public ObservableCollection<Tile> Selections
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
                    // Changes to this property affect multiple other properties in the class
                    OnPropertyChanged(null);
                }
            }
        }

        /// <summary> Last tile in the <see cref="Selections"/> list. Used for determining which tiles are clickable. </summary>
        public Tile CurrentTile
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

        /// <summary> Word the user is building. </summary>
        private string userWord;
        public string UserWord
        {
            get
            {
                return Solver.GetLetters(Selections.ToList());
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

        public bool EnableSubmit
        {
            get
            {
                return Selections.Count() > 0;
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
