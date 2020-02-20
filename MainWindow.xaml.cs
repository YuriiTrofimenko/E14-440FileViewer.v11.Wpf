using E14_440FileViewer.NET.dao.implements.NewDataFileDAO_Parts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using gen = org.tyaa.e14_440fileviewernet.model.generics;
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
using System.Windows.Media.Animation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using E14_440FileViewerWpf.utils;
using E14_440FileViewer.NET.utils;

namespace E14_440FileViewerWpf
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double xmin = 10;
        //private double xmax;
        //private double ymin = 10;
        //private double ymax;
        private double margin = 10;
        private double step = 0.2;
        //TODO - List
        //private double zoom = 1;
        private Dictionary<int, double> zoomList =
            new Dictionary<int, double>();
        private string currentDataFilePath = "";
        private string currentDataFileDir = "";

        private bool? settingsPanelVisibility = false;

        private Brush[] brushes =
            { Brushes.Red, Brushes.Green, Brushes.Blue,  Brushes.Orange,
                Brushes.Magenta, Brushes.LightGreen, Brushes.LightBlue,  Brushes.Yellow
            };
        public MainWindow()
        {
            InitializeComponent();

            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;

            diagramCanvas.Width = this.Width;
            diagramCanvas.Height = this.Height;

            drawMeasures();

            double xmax = diagramCanvas.Width - margin;
            double ymax = diagramCanvas.Height - margin;
            
            /*Random rand = new Random();
            for (int data_set = 0; data_set < 3; data_set++)
            {
                int last_y = rand.Next((int)ymin, (int)ymax);

                PointCollection points = new PointCollection();
                for (double x = xmin; x <= xmax; x += step)
                {
                    last_y = rand.Next(last_y - (int)margin, last_y + (int)margin);
                    if (last_y < ymin) last_y = (int)ymin;
                    if (last_y > ymax) last_y = (int)ymax;
                    points.Add(new Point(x, last_y));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                diagramCanvas.Children.Add(polyline);
            }*/
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Data files (*.dat)|*.dat|Settings files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                string fullDataFilePath = openFileDialog.FileName;

                if (fullDataFilePath.Substring(fullDataFilePath.Count() - 3) == "xml")
                {
                    //1. Прочесть из файла xml из элемента dataFileName имя файла
                    //и передать в функцию рисования графиков

                    //2. Прочесть из файла xml из элемента channelsSettings
                    //номера и масштабы каналов, сохранить их в словарь масштабов
                }
                else
                {
                    string datFilePathWitoutExt = fullDataFilePath.Remove(fullDataFilePath.Count() - 4);

                    currentDataFilePath = datFilePathWitoutExt.Split('\\').Last();
                    currentDataFileDir = System.IO.Path.GetDirectoryName(fullDataFilePath);
                    drawGraph(datFilePathWitoutExt);
                }
            }
        }
        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Settings files (*.xml)|*.xml";
            dialog.AddExtension = true;
            dialog.DefaultExt = "xml";
            string fileName = currentDataFilePath;
            dialog.InitialDirectory = currentDataFileDir;
            if (dialog.ShowDialog() == true)
            {
                ExportXML exportXML = new ExportXML();
                //3. Записать в файл xml в элемент channelsSettings
                //номера и масштабы каналов из словаря масштабов
                exportXML.saveChannelsSettings(dialog.FileName, currentDataFilePath);
            }
        }

        private void drawGraph(string _filePath) {

            zoomList.Clear();
            settingsStackPanel.Children.Clear();
            ParamsReader paramsReader = new ParamsReader();
            List<gen.Channel<double>> channelsArrayList =
                paramsReader.getParams(_filePath + ".prm");
            Console.WriteLine(paramsReader.Count + " " + paramsReader.Frequency + "\n");
            foreach (gen.Channel<double> channel in channelsArrayList)
            {
                //Console.WriteLine(channel.number + " " + channel.amp);
                zoomList.Add(channel.number, 1);

                //?
                /*StackPanel panel =
                    (StackPanel)Cloner.DeepClone(this.Resources["zoomStackPanel"]);*/
                //Console.WriteLine(panel.Children[0].GetHashCode());

                TextBlock textBlock = new TextBlock();
                textBlock.Margin = new Thickness(10);
                textBlock.FontSize = 24;
                textBlock.Text = "#" + channel.number;

                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(10);
                textBox.FontSize = 24;
                textBox.Text = zoomList[channel.number].ToString();
                textBox.Tag = channel.number;
                textBox.KeyDown += (sourceRef, eventArgs) => {
                    if (eventArgs.Key == Key.Return)
                    {
                        //Console.WriteLine(eventArgs.Key);
                        //Console.WriteLine((int)textBox.Tag);
                        //Console.WriteLine(textBox.Text);
                        try
                        {
                            zoomList[(int)textBox.Tag] = Double.Parse(textBox.Text);
                            drawSerieses(ref channelsArrayList);
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Wrong format (expected double)");
                        }
                        
                    }
                };

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Children.Add(textBlock);
                panel.Children.Add(textBox);

                //((TextBlock)panel.Children[0]).Text = "#" + channel.number;
                settingsStackPanel.Children.Add(panel);
            }

            DataReader dataReader = new DataReader();
            dataReader.getData(_filePath + ".dat", ref channelsArrayList);
            /*foreach (var channel in channelsArrayList)
            {
                foreach (double dataItem in channel.getDataArray())C:\Users\student\source\repos\E14-440FileViewer.NET.v11\E14-440FileViewer.NET\dao\
                {
                   Console.WriteLine(dataItem);
                }
            }*/
            drawSerieses(ref channelsArrayList);
        }

        private void drawSerieses(ref List<gen.Channel<double>> channelsArrayList) {

            diagramCanvas.Children.Clear();
            int seriesNumber = 0;
            foreach (var channel in channelsArrayList)
            {
                PointCollection points = new PointCollection();
                double x = xmin;
                foreach (double dataItem in channel.getDataArray())
                {
                    /*if (x > 0 && x < 50)
                    {
                        Console.WriteLine(dataItem);
                    }*/

                    points.Add(new Point(x, (dataItem * zoomList[channel.number] * 150) + (diagramCanvas.Height / 2)));
                    x += step;
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[seriesNumber];
                polyline.Points = points;

                diagramCanvas.Children.Add(polyline);
                seriesNumber++;
            }
        }

        private void drawMeasures() {
            
            for (int i = 0; i < diagramCanvas.Width / 50; i++)
            {
                Line line = new Line()
                {
                    X1 = 0,
                    X2 = 0,
                    Y1 = 0,
                    Y2 = -65
                };

                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 3;

                Canvas.SetTop(line, diagramCanvas.Height);
                Canvas.SetLeft(line, 50 * i);

                diagramCanvas.Children.Add(line);
            }

            for (int i = 0; i < diagramCanvas.Height / 50; i++)
            {
                Line line = new Line()
                {
                    X1 = 0,
                    X2 = 5,
                    Y1 = 0,
                    Y2 = 0
                };

                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 3;

                Canvas.SetTop(line, 50 * i);
                Canvas.SetLeft(line, 0);

                diagramCanvas.Children.Add(line);
            }
        }

        private void ShowSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (settingsPanelVisibility != null)
            {
                if (settingsPanelVisibility == false)
                {
                    //https://stackoverflow.com/questions/1220717/wpf-button-content-with-not-accepting
                    DoubleAnimation animation =
                        new DoubleAnimation(0d, 200d, TimeSpan.FromSeconds(1));
                    animation.Completed +=
                        (senderRef, eventArgs) => {
                            showSettingsButton.Content = ">";
                            settingsPanelVisibility = true;
                            //Console.WriteLine("EndAnimation");
                        };
                    //Console.WriteLine("BeginAnimation");
                    settingsPanelVisibility = null;
                    settingsStackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
                    //Console.WriteLine("EndAnimation");
                }
                else
                {
                    DoubleAnimation animation =
                        new DoubleAnimation(200d, 0d, TimeSpan.FromSeconds(1));
                    animation.Completed +=
                        (senderRef, eventArgs) => {
                            showSettingsButton.Content = "<";
                            settingsPanelVisibility = false;
                        };
                    settingsPanelVisibility = null;
                    settingsStackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
                }
            }
            
        }
    }
}
