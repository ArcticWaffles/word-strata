using Core;
using Solve;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WordStrata
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        public MainWindowViewModel(GameModel gameModel)
        {
            this.gameModel = gameModel;
            ThePath.CollectionChanged += OnCollectionChanged;
        }

        private GameModel gameModel;

        public Board GameBoard
        {
            get { return gameModel.GameBoard; }
        }

        public int Rows
        {
            get { return gameModel.GameBoard.Rows; }
        }

        public int Columns
        {
            get { return gameModel.GameBoard.Columns; }
        }

        public int LevelNumber
        {
            get { return gameModel.LevelNumber; }
        }

        /// <summary> Used for determining a word's validity. </summary>
        public HashSet<String> Dictionary
        {
            get { return gameModel.Dictionary; }
        }

        /// <summary> Word the user is building. </summary>
        public string UserWord
        {
            get
            {
                return Solver.GetLetters(ThePath.ToList());
            }
        }

        /// <summary> Current path of clicked tiles. </summary>
        private ObservableCollection<Tile> thePath = new TilePath();
        public ObservableCollection<Tile> ThePath
        {
            get { return thePath; }
            set
            {
                if (value == thePath) return;
                thePath = value;
                OnPropertyChanged("ThePath");
            }
        }

        /// <summary> Visually traces the current TilePath. </summary>
        public Snake CurrentSnake
        {
            get
            {
                return new Snake(ThePath, GameBoard);
            }
        }

        /// <summary> Is true only if a valid word is selected. </summary>
        public bool EnableSubmit
        {
            get
            {
                return Dictionary.Contains(UserWord, StringComparer.OrdinalIgnoreCase);
            }
        }

        /// <summary> Clears ThePath, thereby clearing the textblock and the gameboard. </summary>
        public void ClearWord()
        {
            ThePath.Clear();
        }

        /// <summary> Turns used tiles to holes and clears word. </summary>
        public void FinishTurn()
        {
            GameBoard.ConvertTilesToHoles(ThePath.ToList());
            ClearWord();
        }

        /// <summary> Determines if any valid words remain on the board. </summary>
        public bool WordsRemain()
        {
            return Solver.AnyWordExistsOnBoard(Dictionary, GameBoard);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ThePath");
            OnPropertyChanged("UserWord");
            OnPropertyChanged("CurrentSnake");
            OnPropertyChanged("EnableSubmit");
        }
    }
}

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one