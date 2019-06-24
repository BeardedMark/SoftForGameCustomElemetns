using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class CheckCustomBox : UserControl
    {
        string[] text = File.ReadAllLines("Options.txt", Encoding.UTF8); // Хранит весь фаил сохранения
        public List<string> code = new List<string>(); // Список загруженых параметров для данного блока из сохранений
        public bool check = false;

        public CheckCustomBox()
        {
            InitializeComponent();
        }

        #region Изменение внешнего виде елемента через геттер и сеттер
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); } // Загружает (возвращает) значение
            set { SetValue(ButtonTextProperty, value); SearchText(); } // Сохраняет значение
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register( // регистрирует новый элемент для изменеия
            "Text", // Ввести имя изменяемого элемента
            typeof(string), // Тип в котором он хранится
            typeof(CheckCustomBox), // Среда в которой он присутствует
            new PropertyMetadata("Text", new PropertyChangedCallback(ButtonTextChanged))); // Дефолтное значение и изменение значения

        private static void ButtonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var twoButton = d as CheckCustomBox;
            twoButton.ButtonTextChanged();
        }

        private void ButtonTextChanged()
        {
            Text.Text = ButtonText;
        }
        #endregion

        // Изменение чекбокса (включено или выключено)
        private void ClickTrigger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(CheckMark.Visibility == Visibility.Visible)
            {
                CheckMark.Visibility = Visibility.Hidden;
                check = false;
            }
            else
            {
                CheckMark.Visibility = Visibility.Visible;
                check = true;
            }
        }

        // Открытие нового окна настройки цвета
        private void ColorTrigger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Customization window = new Customization(Text.Text, ColorBorder.Background, ColorFill.Background, ColorText.Background, this, code);

            window.Show();
        }

        // Функция поиска строк, расшаривание цветов и запись в тултип кода
        void SearchText()
        {
            
            for (int i = 0; i < text.Length; i++) // Запускает цикл поиска
            {
                if (text[i].Contains(Text.Text)) // Если находит строчку с названием такое же как у чекбокса
                {
                    for (int j = i+1; j < text.Length; j++) // запускает цикл считывание всех дальнейших строк
                    {
                        if (text[j].Length == 0) // если строка пустая то остановка цикла
                        {
                            break;
                        }
                        code.Add(text[j]); // Запись в лист строки
                    }
                }
            }

            // Цикл поиска цветов, их расшареность и запись в переменную
            foreach( string str in code)
            {
                if (str.Contains("SetBorderColor"))
                {
                    String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Разбиваем строку на элементы массива
                    Color c = new Color(); // объявляем переменную с цветом
                    c.A = byte.Parse(line[4]); // добавляем каналь альфа
                    c.R = byte.Parse(line[1]); // добавляем канал ред
                    c.G = byte.Parse(line[2]); // добавляем канал грин
                    c.B = byte.Parse(line[3]); // добавляем канал блю

                    ColorBorder.Background = new SolidColorBrush(c); // передаем в переменную цвет и меняем цвет
                }
                if (str.Contains("SetBackgroundColor"))
                {
                    String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Color c = new Color();
                    c.A = byte.Parse(line[4]);
                    c.R = byte.Parse(line[1]);
                    c.G = byte.Parse(line[2]);
                    c.B = byte.Parse(line[3]);

                    ColorFill.Background = new SolidColorBrush(c);
                }
                if (str.Contains("SetTextColor"))
                {
                    String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Color c = new Color();
                    c.A = byte.Parse(line[4]);
                    c.R = byte.Parse(line[1]);
                    c.G = byte.Parse(line[2]);
                    c.B = byte.Parse(line[3]);

                    ColorText.Background = new SolidColorBrush(c);
                }
            }

            // Цикл записи кода блока в тултип
            foreach (string s in code)
            {
                ColorTrigger.ToolTip += s + "\n";
            }
        }
    }
}
