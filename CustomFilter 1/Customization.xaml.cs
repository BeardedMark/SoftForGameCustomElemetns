using CustomFilter_1.CustomControls;
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
using System.Windows.Shapes;

namespace CustomFilter_1
{
    public partial class Customization : Window
    {
        string SetTextColor; // Цвет текста
        string SetBackgroundColor; // Цвет фона
        string SetBorderColor; // Цвет рамки
        string SetFontSize; // Размер текста
        string MinimapIcon; // Иконка на карте
        string PlayEffect; // эфект луча
        string CustomAlertSound; // Иконка на карте

        CheckCustomBox cb; // Хранит чекбокс который вызвал это окно

        DirectoryInfo dir = new DirectoryInfo("C:\\Users\\user\\Documents\\My Games\\Path of Exile");
        List<FileInfo> files = new List<FileInfo>();

        List<string> codelist = new List<string>(); // Список строк готового блока
        List<string> colorslist = new List<string>() { "White", "Brown", "Green", "Red", "Blue", "Yellow" }; // Список цветов
        List<string> formlist = new List<string>() { "Circle", "Diamond", "Square", "Hexagon", "Star", "Triangle" }; // Список форм иконки
        List<string> soundformats = new List<string>() // Список форматов для звуков
        {
            "UNKNOWN", "AIFF", "ASF", "DLS", "FLAC", "FSB", "IT", "MIDI", "MOD", "MPEG",
            "OGGVORBIS", "PLAYLIST", "RAW", "S3M", "USER", "WAV", "XM", "XMA", "AUDIOQUEUE", "AT9",
            "VORBIS", "MEDIA_FOUNDATION", "MEDIACODEC", "FADPCM", "MAX",
        };

        // Инициализация
        public Customization(string viewtext, Brush border, Brush fill, Brush text, CheckCustomBox cbc, List<string> code)
        {
            InitializeComponent();

            rayfalse.IsChecked = true;
            vfalse.IsChecked = true;
            sfalse.IsChecked = true;

            cb = cbc;
            item.Text = viewtext;



            List<FoldersAndFiles> folders = new List<FoldersAndFiles>();
            foreach (DirectoryInfo item in new DirectoryInfo("C:\\Users\\user\\Documents\\My Games\\Path of Exile").GetDirectories())
            {
                folders.Add(new FoldersAndFiles(item.Name));
            }


            foreach (FoldersAndFiles s in folders)
            {
                folder.Items.Add(s.namefolder);
            }



            // Загрузка папок и файлов в комбобоксы с проверкой на содержимое
            //foreach (DirectoryInfo item in dir.GetDirectories())
            //{
            //    DirectoryInfo dirfiles = new DirectoryInfo(dir + "\\" + item);

            //    foreach(string s in soundformats)
            //    {
            //        //files = dirfiles.GetFiles("*." + s);
            //        files.AddRange(dirfiles.GetFiles("*." + s));

            //    }
            //    if (files.Count != 0)
            //    {
            //        folder.Items.Add(item.Name);
            //    }
            //    files.Clear();
            //}


            // Выбор цветов из чекбокса в комбобоксы
            PickerForeground.SelectedColor = ((SolidColorBrush)text).Color;
            PickerBackground.SelectedColor = ((SolidColorBrush)fill).Color;
            PickerBorder.SelectedColor = ((SolidColorBrush)border).Color;

            // Цикл добавления всех цветов в комбобокс иконки и луча
            foreach (string c in colorslist)
            {
                icocolor.Items.Add(c);
                raycolor.Items.Add(c);
            }
            // Цикл добавления всех форм в комбобокс иконки
            foreach (string f in formlist)
            {
                icoform.Items.Add(f);
            }
            // Загрузка иконки, размера, луча
            foreach (string str in code)
            {
                if (str.Contains("SetFontSize"))
                {
                    String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    sizeslider.Value = Convert.ToDouble(line[1]);
                }
                else if(str.Contains("MinimapIcon"))
                {
                    String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    vtrue.IsChecked = true;

                    for(int i = 0; i < formlist.Count; i++)
                    {
                        if (formlist[i].Contains(line[3]))
                        {
                            icoform.SelectedIndex = i;
                        }
                    }

                    for (int i = 0; i < colorslist.Count; i++)
                    {
                        if (colorslist[i].Contains(line[2]))
                        {
                            icocolor.SelectedIndex = i;
                        }
                    }

                    icosize.SelectedIndex = Convert.ToInt32(line[1]);
                    editico();
                }
                else if (str.Contains("PlayEffect"))
                {
                    String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    raytrue.IsChecked = true;

                    for (int i = 0; i < colorslist.Count; i++)
                    {
                        if (colorslist[i].Contains(line[1]))
                        {
                            raycolor.SelectedIndex = i;
                        }
                    }

                    if (line.Contains("Temporary"))
                    {
                        temporary.IsChecked = true;
                    }
                    editray();
                }
                else if (str.Contains("CustomAlertSound"))
                {
                    //String[] line = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //strue.IsChecked = true;

                    //int index = -1;
                    //foreach (string s in folder.Items)
                    //{
                    //    index++;
                    //    if (line[1].Contains(s))
                    //    {
                    //        folder.SelectedIndex = index;
                    //        index = -1;
                    //        DirectoryInfo dirfiles = new DirectoryInfo(dir + "\\" + folder.SelectedItem.ToString());
                    //        foreach (string s2 in soundformats)
                    //        {
                    //            //files = dirfiles.GetFiles("*." + s);
                    //            files.AddRange(dirfiles.GetFiles("*." + s));
                    //        }
                    //        foreach (FileInfo f in files)
                    //        {
                    //            index++;
                    //            if (line[1].Contains(f.ToString()))
                    //            {
                    //                file.SelectedIndex = index;
                    //            }
                    //        }
                    //    }
                    //}

                }
                else
                {
                    folder.SelectedIndex = 0;
                    file.SelectedIndex = 0;
                    raycolor.SelectedIndex = 0;
                    icoform.SelectedIndex = 0;
                    icocolor.SelectedIndex = 0;
                    icosize.SelectedIndex = 0;
                }
            }
            // Добавление хендлеров на каждое изменение
            icoform.SelectionChanged += new SelectionChangedEventHandler(acceptico);
            icocolor.SelectionChanged += new SelectionChangedEventHandler(acceptico);
            icosize.SelectionChanged += new SelectionChangedEventHandler(acceptico);
            raycolor.SelectionChanged += new SelectionChangedEventHandler(acceptray);
        }

        // Конвертация цвета текста
        private void PickerForeground_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            item.Foreground = new SolidColorBrush((Color)PickerForeground.SelectedColor);

            int a = PickerForeground.SelectedColor.Value.A;
            int r = PickerForeground.SelectedColor.Value.R;
            int g = PickerForeground.SelectedColor.Value.G;
            int b = PickerForeground.SelectedColor.Value.B;

            SetTextColor = "SetTextColor " + r.ToString() + " " + g.ToString() + " " + b.ToString() + " " + a.ToString();
        }
        // Конвертация цвета фона
        private void PickerBackground_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            item.Background = new SolidColorBrush((Color)PickerBackground.SelectedColor);

            int a = PickerBackground.SelectedColor.Value.A;
            int r = PickerBackground.SelectedColor.Value.R;
            int g = PickerBackground.SelectedColor.Value.G;
            int b = PickerBackground.SelectedColor.Value.B;

            SetBackgroundColor = "SetBackgroundColor " + r.ToString() + " " + g.ToString() + " " + b.ToString() + " " + a.ToString();
        }
        // Конвертация цвета рамки
        private void PickerBorder_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            item.BorderBrush = new SolidColorBrush((Color)PickerBorder.SelectedColor);

            int a = PickerBorder.SelectedColor.Value.A;
            int r = PickerBorder.SelectedColor.Value.R;
            int g = PickerBorder.SelectedColor.Value.G;
            int b = PickerBorder.SelectedColor.Value.B;

            SetBorderColor = "SetBorderColor " + r.ToString() + " " + g.ToString() + " " + b.ToString() + " " + a.ToString();
        }

        // Изменение размеров
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //size.Content = "Size: " + sizeslider.Value.ToString();
            item.FontSize = 9 + (sizeslider.Value - 11) * (15.0 / 34.0);
            resize.Width = 90 + (sizeslider.Value - 11) * (110.0 / 34.0);
            resize.Height = 18 + (sizeslider.Value - 11) * (22.0 / 34.0);
            SetFontSize = "SetFontSize " + sizeslider.Value;
        }
        // Выключение иконки в превью и скрытие ее параметров
        private void Vtrue_Checked(object sender, RoutedEventArgs e)
        {
            blokico.Visibility = Visibility.Hidden;
            ico.Visibility = Visibility.Visible;
        }
        // Включение иконки в превью и открытие ее параметров
        private void Vfalse_Checked(object sender, RoutedEventArgs e)
        {
            blokico.Visibility = Visibility.Visible;
            ico.Visibility = Visibility.Hidden;
        }
        // Выключение эффекта в превью и скрытие его параметров
        private void Raytrue_Checked(object sender, RoutedEventArgs e)
        {
            blokray.Visibility = Visibility.Hidden;
            raynone.Visibility = Visibility.Hidden;
        }
        // Включение эффекта в превью и открытие его параметров
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            blokray.Visibility = Visibility.Visible;
            raynone.Visibility = Visibility.Visible;
        }

        // Изменение иконки при выборе
        void editico()
        {
            ico.Source = new BitmapImage(new Uri("pack://application:,,,/Images/IcoMap/" + formlist[icoform.SelectedIndex] + "/" + colorslist[icocolor.SelectedIndex] + ".png"));

            if (icosize.SelectedIndex == 0)
            {
                ico.Width = 15;
                ico.Height = 15;
            }
            if (icosize.SelectedIndex == 1)
            {
                ico.Width = 12;
                ico.Height = 12;
            }
            if (icosize.SelectedIndex == 2)
            {
                ico.Width = 9;
                ico.Height = 9;
            }
        }
        // Изменение луча при выборе
        void editray()
        {
            rayimage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Rays/" + colorslist[raycolor.SelectedIndex] + ".jpg"));
        }

        // Хендлеры для изменения иконки
        private void acceptico(object sender, SelectionChangedEventArgs e)
        {
            editico();
        }
        // Хендлеры для изменения луча
        private void acceptray(object sender, SelectionChangedEventArgs e)
        {
            editray();
        }



        // Нажатие на ОК и сохранение всех строк
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            codelist.Add("DisableDropSound"); // Выключение дефолтного звука
            codelist.Add(SetFontSize); // Запись размера текста
            codelist.Add(SetBorderColor); // запись цвета рамки
            codelist.Add(SetBackgroundColor); // Запись цвета фона
            codelist.Add(SetTextColor); // Запись цвета текста

            // Запись иконки если выбрана
            if (vtrue.IsChecked == true)
            {
                MinimapIcon = "MinimapIcon " + icosize.SelectedIndex.ToString() + " " + icocolor.SelectedItem + " " + icoform.SelectedItem;
                codelist.Add(MinimapIcon);
            }

            // Запись луча если выбран
            if (raytrue.IsChecked == true)
            {
                PlayEffect = "PlayEffect " + raycolor.SelectedItem;
                if (temporary.IsChecked == true)
                {
                    PlayEffect += " Temporary";
                }
                codelist.Add(PlayEffect);
            }

            if (strue.IsChecked == true)
            {
                CustomAlertSound = "CustomAlertSound " + folder.SelectedItem.ToString() + "//" + file.SelectedItem.ToString() + " 100";
                codelist.Add(CustomAlertSound);
            }

            cb.ColorBorder.Background = new SolidColorBrush((Color)PickerBorder.SelectedColor); // Передаем в превью цвет рамки
            cb.ColorFill.Background = new SolidColorBrush((Color)PickerBackground.SelectedColor); // Передаем в превью цвет фона
            cb.ColorText.Background = new SolidColorBrush((Color)PickerForeground.SelectedColor); // Передаем в превью цвет текста
            cb.code.Clear(); // Очищаем старый блок кода фильтра
            cb.ColorTrigger.ToolTip = ""; // Очищаем тултип чекбокса

            // Цикл записи нового кода в блок чекбокса и в тултип
            foreach (string s in codelist)
            {
                cb.code.Add(s);
                cb.ColorTrigger.ToolTip += s + "\n";
            }

            Close();

        }
        
        // загрузка вложеных файлов в комбобокс исходя из выбранной папки
        private void folder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectoryInfo dirfiles = new DirectoryInfo(dir + "\\" + folder.SelectedItem.ToString());
            foreach (string s in soundformats)
            {
                //files = dirfiles.GetFiles("*." + s);
                files.AddRange(dirfiles.GetFiles("*." + s));
            }
            file.Items.Clear();

            foreach (FileInfo fi in files)
            {
                file.Items.Add(fi);
            }
            files.Clear();
            file.SelectedIndex = 0;
        }

        private void sfalse_Checked(object sender, RoutedEventArgs e)
        {
            bloksound.Visibility = Visibility.Visible;
        }

        private void strue_Checked(object sender, RoutedEventArgs e)
        {
            bloksound.Visibility = Visibility.Hidden;
        }
    }
}



class FoldersAndFiles : List
{
    public string namefolder;
    public List<string> files = new List<string>();
    public FoldersAndFiles(string name)
    {
        namefolder = name;
    }
}