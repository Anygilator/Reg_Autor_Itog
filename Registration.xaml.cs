﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace Reg_Autor
{
    /// <summary>
    /// Логика взаимодействия для autores.xaml
    /// </summary>
    public partial class autores : Window
    {
        public autores()
        {
            InitializeComponent();
        }

        private void registratione_Click(object sender, RoutedEventArgs e)
        {
            var pochta = Pochtat.Text;
            var login = Logint.Text;
            var pass = Parolt.Text;
            var pass2 = Parol2t.Text;
            var context = new AppDbContext();

            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(pochta, expression))
            {
                if (Regex.Replace(pochta, expression, string.Empty).Length == 0)
                {
                    if(pass.Length > 7)
                    {
                        bool symbolCheck = pass.Any(p => !char.IsLetterOrDigit(p));
                        if (symbolCheck == true) 
                        {
                            if (pass == pass2)
                            {
                                var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
                                if (user_exists is not null)
                                {
                                    MessageBox.Show("Такой пользователь уже существует");
                                }
                                else
                                {
                                    var user = new User { pochta = pochta, Login = login, Password = pass };
                                    context.Users.Add(user);
                                    context.SaveChanges();
                                    MessageBox.Show("Вы зарегестрировались");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают");
                            }
                        }
                        else
                        {
                            MessageBox.Show("В парле должен быть хоть один спецсимвол");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль дожне состоять из 8 и более символов");
                    }
                }
            }
            else
            {
                MessageBox.Show("Укажите почту правильно");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }
    }
}
