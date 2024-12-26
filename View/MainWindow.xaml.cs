using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, int> groups;
        MainViewModel dc;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SubscribeToCollectionChanged()
        {
            dc = (MainViewModel)DataContext;
            dc.Students.CollectionChanged += PropertiesChanged;
            this.SizeChanged += PropertiesChanged;
        }

        private void PropertiesChanged(object sender, SizeChangedEventArgs e)
        {
            var dict = GetSpecialitiesDict();
            DrawHistogram(dict);
        }

        private void PropertiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var dict = GetSpecialitiesDict();
            DrawHistogram(dict);
        }

        public Dictionary<string, int> GetSpecialitiesDict()
        {
            ObservableCollection<Student> students = dc.Students;

            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<Student> list = students.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Student student = list[i];
                if (student.Speciality != null)
                {
                    if (dict.ContainsKey(student.Speciality))
                    {
                        dict[student.Speciality]++;
                    }
                    else
                    {
                        dict.Add(student.Speciality, 1);
                    }
                }
            }

            return dict;
        }

        private void DrawHistogram(Dictionary<string, int> data)
        {
            HistogramCanvas.Children.Clear(); // Очищаем канвас перед новым рисованием

            if (data == null || data.Count == 0)
                return;

            double canvasWidth = HistogramCanvas.ActualWidth;
            double canvasHeight = HistogramCanvas.ActualHeight;

            double maxCount = data.Values.Max();

            double barWidth = canvasWidth / data.Count;
            int index = 0;

            foreach (KeyValuePair<string, int> entry in data)
            {
                double normalizedHeight = entry.Value / maxCount * (canvasHeight - 40); // Увеличиваем отступ от верхнего края

                // Создаём прямоугольник для столбца с градиентом и закруглёнными углами
                Rectangle rect = new Rectangle
                {
                    Width = barWidth - 10, // Оставим немного пространства между столбцами
                    Height = normalizedHeight,
                    Fill = new LinearGradientBrush(Colors.LightBlue, Colors.Blue, 90),
                    Stroke = Brushes.Black,
                    RadiusX = 5,
                    RadiusY = 5
                };

                // Устанавливаем положение столбца
                Canvas.SetLeft(rect, index * barWidth);
                Canvas.SetBottom(rect, 20); // Добавляем место для подписи

                HistogramCanvas.Children.Add(rect);

                // Добавляем подпись под столбцом
                TextBlock label = new TextBlock
                {
                    Text = entry.Key,
                    Width = barWidth - 10,
                    TextAlignment = TextAlignment.Center
                };

                Canvas.SetLeft(label, index * barWidth);
                Canvas.SetTop(label, canvasHeight - 20);

                HistogramCanvas.Children.Add(label);

                // Добавляем процентное значение над столбцом
                double percentage = (entry.Value / maxCount) * 100;
                TextBlock percentageLabel = new TextBlock
                {
                    Text = $"{percentage:F1}%",
                    Width = barWidth - 10,
                    TextAlignment = TextAlignment.Center
                };

                Canvas.SetLeft(percentageLabel, index * barWidth);
                Canvas.SetBottom(percentageLabel, normalizedHeight + 25); // Отступ для размещения над столбцом

                HistogramCanvas.Children.Add(percentageLabel);

                index++;
            }
        }


    }

    public class Group
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
