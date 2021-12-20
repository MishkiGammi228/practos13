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
using Libmas;
using Microsoft.Win32;
using System.IO;

namespace Lab_13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int[,] _matrix;
        int _column;
        int _row;

        private void Rez_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(Column.Text, out int column) && Int32.TryParse(Row.Text, out int row))
            {
                if (row != 1)
                {
                    _matrix = new int[row, column];
                    ArrayOperation.FillArrayRandom(_matrix, 10);
                    dataGrid.ItemsSource = VisualArray.ToDataTable(_matrix).DefaultView;
                  
                    int countUniq = 0;
                    int countUniqColumns = 0;
                    for (int i = 0; i < _matrix.GetLength(1); i++)
                    {
                        countUniq = 0;
                        for (int j = 0; j < _matrix.GetLength(0) - 1; j++)
                        {
                            for (int k = 0; k < _matrix.GetLength(0); k++)
                            {
                                if (_matrix[j, i] != _matrix[k, i])
                                {
                                    countUniq++;
                                }
                            }
                        }
                        if (countUniq / (_matrix.GetLength(0) - 1) == _matrix.GetLength(0) - 1)
                        {
                            countUniqColumns++;
                        }                  
                    }                  
                     Otv.Text = $"{countUniqColumns}";
                }
            }
            else MessageBox.Show("Введены неправильные значения в поля или поля незаполнены");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_matrix == null)
            {
                MessageBox.Show("Таблица пуста");
                return;
            }
            SaveFileDialog save = new SaveFileDialog();
            StreamWriter outFileRez = new StreamWriter("rez.txt");
            StreamWriter outFileColumn = new StreamWriter("column.txt");
            StreamWriter outFileRow = new StreamWriter("row.txt");
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                ArrayOperation.SaveArray(_matrix, save.FileName);
                outFileRez.Write(Otv.Text);
                outFileRow.Write(Row.Text);
                outFileColumn.Write(Column.Text);
            }
            outFileRez.Close();
            outFileColumn.Close();
            outFileRow.Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            StreamReader inFileRez = new StreamReader("rez.txt");
            StreamReader inFileColumn = new StreamReader("column.txt");
            StreamReader inFileRow = new StreamReader("row.txt");
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                if (open.FileName != string.Empty)
                {
                    ArrayOperation.OpenArray(out _matrix, open.FileName);
                    Otv.Text = Convert.ToString(inFileRez.ReadLine());
                    Column.Text = Convert.ToString(inFileColumn.ReadLine());
                    Row.Text = Convert.ToString(inFileRow.ReadLine());
                    dataGrid.ItemsSource = VisualArray.ToDataTable(_matrix).DefaultView;
                }
            }
            inFileRez.Close();
            inFileColumn.Close();
            inFileRow.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("4. Дана целочисленная матрица размера M * N." +
                " Найти количество ее столбцов, все элементы которых различны.");
        }

        private void Row_TextChanged(object sender, TextChangedEventArgs e)
        {
            SizeRow.Text = $"Строка {Row.Text}";
        }

        private void Column_TextChanged(object sender, TextChangedEventArgs e)
        {
            SizeColumn.Text = $"Столбец {Column.Text}";
        }

        private void FocusArray(object sender, MouseEventArgs e)
        {
            _column = dataGrid.CurrentColumn.DisplayIndex;
            _row = dataGrid.Items.IndexOf(dataGrid.CurrentItem);
            Position.Text = $"[{_row + 1};{_column + 1}]";
        }

        private void MatrixClear_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
        }

        private void OtvClear_Click(object sender, RoutedEventArgs e)
        {
            Otv.Clear();
        }

        private void DataClear_Click(object sender, RoutedEventArgs e)
        {
            Row.Clear();
            Column.Clear();
        }
    }
}
