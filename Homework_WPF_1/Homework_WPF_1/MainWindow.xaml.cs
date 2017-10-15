using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Homework_WPF_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> users;
        private User CurrentUser;
        private bool nameIsFree;
        BinaryFormatter formater;
        public MainWindow()
        {
            InitializeComponent();
            users = new List<User>();
            CurrentUser = new User();
            formater = new BinaryFormatter();
            nameIsFree = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(userNameSaveTextBox.Text) &&
                userNameSaveTextBox.Text != "User Name" )
            {
                foreach(User user in users)
                {
                    if(user.Name == userNameSaveTextBox.Text)
                    {
                        nameIsFree = false;
                    }
                }
                if (nameIsFree)
                {
                    CurrentUser.Name = userNameSaveTextBox.Text;
                    CurrentUser.Red = (byte)redSlider.Value;
                    CurrentUser.Green = (byte)greenSlider.Value;
                    CurrentUser.Blue = (byte)blueSlider.Value;
                    users.Add(CurrentUser);
                                        
                    using (FileStream stream = new FileStream("users.bin", FileMode.OpenOrCreate))
                    {
                        formater.Serialize(stream, users);
                    }
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = new FileStream("users.bin", FileMode.OpenOrCreate))
            {
                users = (List<User>)formater.Deserialize(stream);
            }
            foreach (User user in users)
            {
                if (user.Name == userNameLoadTextBox.Text)
                {
                    redSlider.Value = user.Red;
                    greenSlider.Value = user.Green;
                    blueSlider.Value = user.Blue;
                }
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            main.Background = new SolidColorBrush(Color.FromArgb(255, (byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value));
        }
    }
}
