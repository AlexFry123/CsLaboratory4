using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;

namespace CsLaboratory2
{
    internal class UserListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _users;
        private static CollectionView _sortFilterOptionsCollection;
        public Person SelectedUser { get; set; }
        public string Filter { get; set; }
        public string SelectedField { get; set; }

        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _filterCommand;
        private ICommand _sortCommand;

        public static CollectionView SortFilterOptions => _sortFilterOptionsCollection ??
                                                          (_sortFilterOptionsCollection =
                                                              new CollectionView(ComboBoxNames));

        public static readonly string[] ComboBoxNames =
            Array.ConvertAll(typeof(Person).GetProperties(), (property) => property.Name);

        internal UserListViewModel()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
            var file = new FileInfo(FileFolderHelper.StorageFilePath);
            if (!file.CreateFolderAndCheckFileExistance())
                CreateFirstUsers();
        }

        private void CreateFirstUsers()
        {
            for (int i = 0; i < 50; i++)
            {
                var users = _users.ToList();
                var tmpUser = new Person("FirstNAme" + i, "LastNAme" + i, "Email@ukma.edu.ua" + i, DateTime.Today);
                users.Add(tmpUser);
                StationManager.DataStorage.AddUser(tmpUser);
                Users = new ObservableCollection<Person>(users);
            }
        }

        public ObservableCollection<Person> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand =
                           new RelayCommand<object>(AddComm));
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand<object>(FilterByProperty));
            }
        }

        public ICommand SortCommand
        {
            get
            {
                return _sortCommand ?? (_sortCommand = new RelayCommand<object>(SortByChosenProp));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand =
                           new RelayCommand<object>(DeleteComm));
            }
        }

        private void AddComm(object obj)
        {
            NavigationManager.Navigate(new MainWindow());
        }

        private void DeleteComm(object obj)
        {
            StationManager.DataStorage.DeleteUser(SelectedUser);
            _users.Remove(SelectedUser);
        }

        private void FilterByProperty(object obj)
        {
            var tmpUsers = StationManager.DataStorage.UsersList;
            var resUsers = from p in tmpUsers
            where (p.GetType().GetProperty(SelectedField)?.GetValue(p, null)).ToString().ToLower().Contains(Filter)
            select p;
            if(resUsers != null)
            {
                Users = new ObservableCollection<Person>(resUsers);
            }
        }

        private void SortByChosenProp(object obj)
        {
            var tmpUsers = StationManager.DataStorage.UsersList;
            var res = from p in tmpUsers
                      orderby p.GetType().GetProperty(SelectedField)?.GetValue(p, null).ToString() ascending
                      select p;
            Users = new ObservableCollection<Person>(res);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
