using CsLaboratory2.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CsLaboratory2
{
    internal class PersonViewModel : BaseViewModel, ILoaderOwner
    {
        private Person _user;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        private ICommand _proceedCommand;

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand =
                           new RelayCommand<object>(ConfirmPersonInplementation, CanSignInExecute));
            }
        }

        private async void ConfirmPersonInplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            bool dateIsValid = true;
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                try
                {
                    _user = new Person(Name, Surname, Email, BirthDate);
                }
                catch (BirthDateInDistantPastException pastEx)
                {
                    MessageBox.Show(pastEx.Message);
                    dateIsValid = false;
                }
                catch (BirthDateInFutureException futureEx)
                {
                    MessageBox.Show(futureEx.Message);
                    dateIsValid = false;
                }
                catch(WrongEmailException emailEx)
                {
                    MessageBox.Show(emailEx.Message);
                    dateIsValid = false;
                }
            });
            if (dateIsValid)
            {
                StationManager.DataStorage.AddUser(_user);
                NavigationManager.Navigate(new UsersView());
            }
            LoaderManager.Instance.HideLoader();
        }

        private bool CanSignInExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) && !string.IsNullOrWhiteSpace(_email) && !string.IsNullOrWhiteSpace(_birthDate.ToShortDateString());
        }

        internal PersonViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }
    }
}
