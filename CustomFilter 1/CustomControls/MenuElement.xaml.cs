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

namespace CustomFilter_1.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для MenuElement.xaml
    /// </summary>
    public partial class MenuElement : UserControl
    {
        public MenuElement()
        {
            InitializeComponent();
        }

        #region Text
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); } // Загружает (возвращает) значение
            set { SetValue(ButtonTextProperty, value); } // Сохраняет значение
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register( // регистрирует новый элемент для изменеия
            "Text", // Ввести имя изменяемого элемента
            typeof(string), // Тип в котором он хранится
            typeof(MenuElement), // Среда в которой он присутствует
            new PropertyMetadata("Text", new PropertyChangedCallback(ButtonTextChanged))); // Дефолтное значение и изменение значения

        private static void ButtonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var twoButton = d as MenuElement;
            twoButton.ButtonTextChanged();
        }

        private void ButtonTextChanged()
        {
            Text.Text = ButtonText;
        }
        #endregion

        #region Ico
        public ImageSource ButtonIco
        {
            get { return (ImageSource)GetValue(ButtonIcoProperty); } // Загружает (возвращает) значение
            set { SetValue(ButtonIcoProperty, value); } // Сохраняет значение
        }

        public static readonly DependencyProperty ButtonIcoProperty = DependencyProperty.Register( // регистрирует новый элемент для изменеия
            "ButtonIco", // Ввести имя изменяемого элемента
            typeof(ImageSource), // Тип в котором он хранится
            typeof(MenuElement), // Среда в которой он присутствует
            new PropertyMetadata(null, new PropertyChangedCallback(ButtonIcoChanged))); // Дефолтное значение и изменение значения

        private static void ButtonIcoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var twoButton = d as MenuElement;
            twoButton.ButtonIcoChanged();
        }

        private void ButtonIcoChanged()
        {
            Source.Source = ButtonIco;
        }

        #endregion

        private void Trigger_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker.Visibility = Visibility.Visible;
        }

        private void Trigger_MouseLeave(object sender, MouseEventArgs e)
        {
            Marker.Visibility = Visibility.Hidden;
        }
    }
}
