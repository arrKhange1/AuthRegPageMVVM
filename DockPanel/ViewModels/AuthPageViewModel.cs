using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DockPanel.Commands;
using DockPanel.Models;
using System.Windows;
using DockPanel.Components;
using System.Data.Entity;

namespace DockPanel.ViewModels
{
    class AuthPageViewModel : ViewModelBase
    {
        ApplicationContext db;
        public AuthPageViewModel()
        {
            ShowLoginPopup = false;
            ShowPasswordPopup = false;
            ShowEmailPopup = false;
            LoginTextBorderColor = "Gray";
            PasswordTextBorderColor = "Gray";
            EmailTextBorderColor = "Gray";

            db = new ApplicationContext();
            db.Users.Load();
        }
        #region Свойства Логина
        private bool _showLoginPopup;
        private string _loginPopupText;
        private string _loginText;
        private string _loginTextBorderColor;

        public string LoginTextBorderColor
        {
            get { return _loginTextBorderColor; }
            set
            {
                _loginTextBorderColor = value;
                OnPropertyChanged();
            }
        }
        public string LoginText
        {
            get { return _loginText; }
            set
            {
                _loginText = value;
            }
        }


        public string LoginPopupText
        {
            get { return _loginPopupText; }
            set
            {
                _loginPopupText = value;
                OnPropertyChanged();
            }
        }
        public bool ShowLoginPopup
        {
            get { return _showLoginPopup; }
            set
            {
                _showLoginPopup = value;
                if (!_showLoginPopup) LoginTextBorderColor = "Gray";
                OnPropertyChanged();
            }
        }
        #endregion

        #region Свойства Пароля
        private bool _showPasswordPopup;
        private string _passwordPopupText;
        private string _passwordText;
        private string _passwordTextBorderColor;

        public string PasswordTextBorderColor
        {
            get { return _passwordTextBorderColor; }
            set
            {
                _passwordTextBorderColor = value;
                OnPropertyChanged();
            }
        }
        public string PasswordText
        {
            get { return _passwordText; }
            set
            {
                _passwordText = value;
            }
        }


        public string PasswordPopupText
        {
            get { return _passwordPopupText;  }
            set
            {
                _passwordPopupText = value;
                OnPropertyChanged();
            }
        }
        public bool ShowPasswordPopup
        {
            get { return _showPasswordPopup; }
            set
            {
                _showPasswordPopup = value;
                if (!_showPasswordPopup) PasswordTextBorderColor = "Gray";
                OnPropertyChanged();
            }
        }
        #endregion

        #region Свойства Email
        private bool _showEmailPopup;
        private string _emailPopupText;
        private string _emailText;
        private string _emailTextBorderColor;

        public string EmailTextBorderColor
        {
            get { return _emailTextBorderColor; }
            set
            {
                _emailTextBorderColor = value;
                OnPropertyChanged();
            }
        }
        public string EmailText
        {
            get { return _emailText; }
            set
            {
                _emailText = value;
            }
        }


        public string EmailPopupText
        {
            get { return _emailPopupText; }
            set
            {
                _emailPopupText = value;
                OnPropertyChanged();
            }
        }
        public bool ShowEmailPopup
        {
            get { return _showEmailPopup; }
            set
            {
                _showEmailPopup = value;
                if (!_showEmailPopup) EmailTextBorderColor = "Gray";
                OnPropertyChanged();
            }
        }
        #endregion



        private RelayCommand _authButton_Click;
        public RelayCommand AuthButton_Click
        {
            get
            {
                return _authButton_Click ??
                    (_authButton_Click = new RelayCommand(obj =>
                    {
                        bool valid = true;
                        if (db.Users.FirstOrDefault(user => user.login == LoginText) == null)
                        {
                            valid = false;
                            LoginTextBorderColor = "Red";
                            LoginPopupText = "Такой пользователь не зарегистрирован";
                            ShowLoginPopup = true;
                        }
                        if (db.Users.FirstOrDefault(user => user.email == EmailText) == null)
                        {
                            valid = false;
                            EmailTextBorderColor = "Red";
                            EmailPopupText = "Такой Email не зарегистрирован";
                            ShowEmailPopup = true;
                        }

                        if (valid)
                        {
                            bool equalPass = db.Users.FirstOrDefault(user => user.email == EmailText && user.login == LoginText).password == PasswordText;
                            if (equalPass)
                            {
                                LoginTextBorderColor = "Green";
                                PasswordTextBorderColor = "Green";
                                EmailTextBorderColor = "Green";
                            }
                            else
                            {
                                PasswordTextBorderColor = "Red";
                                PasswordPopupText = "Неверный пароль";
                                ShowPasswordPopup = true;
                            }
                        }


                    }));
            }
        }

    }
}
