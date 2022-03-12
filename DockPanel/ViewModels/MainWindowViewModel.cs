using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DockPanel.Models;
using DockPanel.Commands;

namespace DockPanel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            CurrentPage = mainWindowModel.FirstPage;
            CurrentMode = "Регистрация";
            RegButtonOpacity = 0.05;
            AuthButtonOpacity = 1;
        }


        private MainWindowModel mainWindowModel = new MainWindowModel();
        private Page _currentPage;
        private string _currentMode;
        private double _regButtonOpacity;
        private double _authButtonOpacity;

        private RelayCommand _firstPage_Click;
        public RelayCommand FirstPage_Click
        {
            get
            {
                return _firstPage_Click ??
                    (_firstPage_Click = new RelayCommand(obj =>
                    {
                        CurrentPage = mainWindowModel.FirstPage;
                        CurrentMode = "Регистрация";
                        RegButtonOpacity = 0.05;
                        AuthButtonOpacity = 1;
                    }));
            }
        }

        private RelayCommand _secondPage_Click;
        public RelayCommand SecondPage_Click
        {
            get
            {
                return _secondPage_Click ??
                    (_secondPage_Click = new RelayCommand(obj =>
                    {
                        CurrentPage = mainWindowModel.SecondPage;
                        CurrentMode = "Авторизация";
                        AuthButtonOpacity = 0.05;
                        RegButtonOpacity = 1;
                    }));
            }
        }

        
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (Equals(_currentPage, value)) return;
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public string CurrentMode
        {
            get { return _currentMode; }

            set
            {
                if (Equals(_currentMode, value)) return;
                _currentMode = value;
                OnPropertyChanged();
            }
        }

        public double RegButtonOpacity
        {
            get { return _regButtonOpacity; }
            set
            {
                if (Equals(_regButtonOpacity, value)) return;
                _regButtonOpacity = value;
                OnPropertyChanged();

            }
        }

        public double AuthButtonOpacity
        {
            get { return _authButtonOpacity; }
            set
            {
                if (Equals(_authButtonOpacity, value)) return;
                _authButtonOpacity = value;
                OnPropertyChanged();

            }
        }

        








    }
}

