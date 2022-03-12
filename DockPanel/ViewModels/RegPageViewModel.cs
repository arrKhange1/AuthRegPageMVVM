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
    class RegPageViewModel : ViewModelBase
    {
        ApplicationContext db;
        private IEnumerable<User> _users;
        
        public IEnumerable<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
            }
        }
        public RegPageViewModel()
        {
            LoginTextBorderColor = "Gray";
            PasswordTextBorderColor = "Gray";
            ConfirmPasswordTextBorderColor = "Gray";
            IsConfirmPasswordEnabled = false;
            EmailTextBorderColor = "Gray";
            ShowLoginPopup = false;

            db = new ApplicationContext();
            db.Users.Load();
        }

        private RegPageModel regPageModel = new RegPageModel();

        private string _loginText;
        private string _loginTextBorderColor;
        private bool _showLoginPopup;
        private string _loginPopupText;
        private string _loginPopupTextColor;

        private string _passwordText;
        private string _passwordTextBorderColor;
        private bool _showPasswordPopup;
        private string _passwordPopupText;
        private string _passwordPopupTextColor;

        private string _confirmPasswordText;
        private string _confirmPasswordTextBorderColor;
        private bool _isConfirmPasswordEnabled;
        private bool _showConfirmPasswordPopup;
        private string _confirmPasswordPopupText;
        private string _confirmPasswordPopupTextColor;

        private string _emailText;
        private string _emailTextBorderColor;
        private bool _showEmailPopup;
        private string _emailPopupText;
        private string _emailPopupTextColor;

        private string _regStatusText;

        
        

        public string RegStatusText
        {
            get { return _regStatusText; }
            set
            {
                _regStatusText = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _regButton_Click;
        public RelayCommand RegButton_Click 
        {
            get
            {
                return _regButton_Click ??
                    (_regButton_Click = new RelayCommand(obj =>
                    {
                        RegStatusText = regPageModel.GetRegStatus(ref _loginTextBorderColor, ref _passwordTextBorderColor, ref _confirmPasswordTextBorderColor, ref _emailTextBorderColor);
                        
                        LoginTextBorderColor = _loginTextBorderColor;
                        PasswordTextBorderColor = _passwordTextBorderColor;
                        ConfirmPasswordTextBorderColor = _confirmPasswordTextBorderColor;
                        EmailTextBorderColor = _emailTextBorderColor;

                         
                        if (RegStatusText == "Успешная регистрация!" && db.Users.FirstOrDefault(user => user.login == _loginText) == null && db.Users.FirstOrDefault(user => user.email == _emailText) == null)
                        {

                            User user = new User(LoginText, PasswordText, EmailText);
                            db.Users.Add(user);
                            db.SaveChanges();
                            
                        }
                        else if (RegStatusText == "Успешная регистрация!")
                        {
                            bool existLogin = db.Users.FirstOrDefault(user => user.login == _loginText) != null;
                            bool existEmail = db.Users.FirstOrDefault(user => user.email == _emailText) != null;
                            RegStatusText = "Регистрация не удалась!";
                            if (existLogin)
                            {
                                LoginTextBorderColor = "Red";
                                LoginPopupText = "Данный логин уже занят";
                                LoginPopupTextColor = "Red";
                                ShowLoginPopup = true;
                            }

                            if (existEmail)
                            {
                                EmailTextBorderColor = "Red";
                                EmailPopupText = "Данный Email уже занят";
                                EmailPopupTextColor = "Red";
                                ShowEmailPopup = true;
                            }
                        }

                    }));
            }
        }

        #region Свойства Email
        public string EmailText
        {
            get { return _emailText; }
            set
            {
                _emailText = value;

                if (_emailText.Contains("a") && db.Users.FirstOrDefault(user => user.email == _emailText) == null)
                {
                    EmailTextBorderColor = "Green";
                    EmailPopupText = "Данный Email доступен";
                    EmailPopupTextColor = "Green";
                    ShowEmailPopup = true;
                }
                else if (_emailText.Contains("a"))
                {
                    EmailTextBorderColor = "Red";
                    EmailPopupText = "Такой Email уже занят";
                    EmailPopupTextColor = "Red";
                    ShowEmailPopup = true;
                }
                else
                {
                    EmailTextBorderColor = "Red";
                    EmailPopupText = "Email не соответствует формату";
                    EmailPopupTextColor = "Red";
                    ShowEmailPopup = true;
                }

            }
        }

        public string EmailTextBorderColor
        {
            get { return _emailTextBorderColor; }
            set
            {
                _emailTextBorderColor = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public string EmailPopupTextColor
        {
            get { return _emailPopupTextColor; }
            set
            {
                _emailPopupTextColor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Свойства ConfirmPassword
        public bool IsConfirmPasswordEnabled
        {
            get { return _isConfirmPasswordEnabled; }
            set
            {
                _isConfirmPasswordEnabled = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordText
        {
            get { return _confirmPasswordText; }
            set
            {
                _confirmPasswordText = value;

                if (_confirmPasswordText != _passwordText)
                {
                    ConfirmPasswordTextBorderColor = "Red";
                    ConfirmPasswordPopupText = "Пароли не совпадают";
                    ConfirmPasswordPopupTextColor = "Red";
                    ShowConfirmPasswordPopup = true;
                }
                else
                {
                    ConfirmPasswordTextBorderColor = "Green";
                    ConfirmPasswordPopupText = "Пароли совпадают";
                    ConfirmPasswordPopupTextColor = "Green";
                    PasswordPopupText = ConfirmPasswordPopupText;
                    PasswordPopupTextColor = ConfirmPasswordPopupTextColor;
                    ShowConfirmPasswordPopup = true;
                    ShowPasswordPopup = ShowConfirmPasswordPopup;
                }

                OnPropertyChanged();



            }
        }

        public string ConfirmPasswordTextBorderColor
        {
            get { return _confirmPasswordTextBorderColor; }
            set
            {
                _confirmPasswordTextBorderColor = value;
                OnPropertyChanged();
            }

        }

        public string ConfirmPasswordPopupText
        {
            get { return _confirmPasswordPopupText; }
            set
            {
                _confirmPasswordPopupText = value;
                OnPropertyChanged();
            }
        }
        public bool ShowConfirmPasswordPopup
        {
            get { return _showConfirmPasswordPopup; }
            set
            {
                _showConfirmPasswordPopup = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordPopupTextColor
        {
            get { return _confirmPasswordPopupTextColor; }
            set
            {
                _confirmPasswordPopupTextColor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Свойства Password
        public string PasswordText
        {
            get { return _passwordText; }
            set
            {
                _passwordText = value;

                if (_passwordText != "" && _passwordText.Length >= 8)
                {
                    PasswordTextBorderColor = "Green";
                    IsConfirmPasswordEnabled = true;
                    PasswordPopupText = "Формат пароля правильный";
                    PasswordPopupTextColor = "Green";
                    ShowPasswordPopup = true;
                }
                else
                {
                    PasswordTextBorderColor = "Red";
                    IsConfirmPasswordEnabled = false;
                    PasswordPopupText = "Формат пароля неправильный";
                    PasswordPopupTextColor = "Red";
                    ShowPasswordPopup = true;
                }

                if (_passwordText != _confirmPasswordText)
                {
                    ConfirmPasswordTextBorderColor = "Red";
                    ConfirmPasswordPopupText = "Пароли не совпадают";
                    ConfirmPasswordPopupTextColor = "Red";
                    ShowConfirmPasswordPopup = true;
                }
                else if (_passwordTextBorderColor == "Green")
                {
                    ConfirmPasswordTextBorderColor = "Green";
                    ConfirmPasswordPopupText = "Пароли совпадают";
                    ConfirmPasswordPopupTextColor = "Green";
                    PasswordPopupText = ConfirmPasswordPopupText;
                    PasswordPopupTextColor = ConfirmPasswordPopupTextColor;
                    ShowConfirmPasswordPopup = true;
                    ShowPasswordPopup = ShowConfirmPasswordPopup;
                }

            }
        }
        public string PasswordTextBorderColor
        {
            get { return _passwordTextBorderColor; }
            set
            {
                _passwordTextBorderColor = value;
                OnPropertyChanged();
            }

        }

        public string PasswordPopupText
        {
            get { return _passwordPopupText; }
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
                OnPropertyChanged();
            }
        }

        public string PasswordPopupTextColor
        {
            get { return _passwordPopupTextColor; }
            set
            {
                _passwordPopupTextColor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Свойства Login
        public string LoginText
        {
            get { return _loginText; }
            set
            {
                _loginText = value;

                if (_loginText.Length >= 5 && db.Users.FirstOrDefault(user => user.login == _loginText) == null)
                {
                    
                    LoginTextBorderColor = "Green";
                    LoginPopupText = "Данный логин не занят";
                    LoginPopupTextColor = "Green";
                    ShowLoginPopup = true;
                }
                else if (_loginText.Length >= 5)
                {
                    LoginTextBorderColor = "Red";
                    LoginPopupText = "Данный логин уже занят";
                    LoginPopupTextColor = "Red";
                    ShowLoginPopup = true;
                }
                else
                {
                    LoginTextBorderColor = "Red";
                    LoginPopupText = "Неверный формат логина";
                    LoginPopupTextColor = "Red";
                    ShowLoginPopup = true;
                }



            }
        }
        public string LoginTextBorderColor
        {
            get { return _loginTextBorderColor; }
            set
            {
                _loginTextBorderColor = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public string LoginPopupTextColor
        {
            get { return _loginPopupTextColor; }
            set
            {
                _loginPopupTextColor = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }
}
