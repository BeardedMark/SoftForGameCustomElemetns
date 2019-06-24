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

namespace CustomFilter_1
{
    public partial class MainWindow : Window
    {
        bool MenuHide;
        public MainWindow()
        {
            InitializeComponent();
            // Считывание элементов меню и добавление им хендлера
            foreach(CustomControls.MenuElement c in StackPanelMenu.Children)
            {
                c.MouseDown += MenuMouseDown;
            }
        }

        //Активирование и деактивирование кнопок меню
        private void MenuMouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (CustomControls.MenuElement c in StackPanelMenu.Children)
            {
                c.Active.Visibility = Visibility.Hidden;
            }
            CustomControls.MenuElement element = (CustomControls.MenuElement)sender;
            element.Active.Visibility = Visibility.Visible;
        }

        //Сокрытие меню и раскрытие 
        private void MenuElement_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (MenuHide == true)
            {
                MenuGrid.Width = new GridLength(150);
                MenuHide = false;
            }
            else
            {
                MenuGrid.Width = new GridLength(30);
                MenuHide = true;
            }
        }

        // Сохранение фильтра
        private void Go(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
