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
using System.Windows.Shapes;

namespace WPFSQLite
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CategoryNameTextBox.Text))
            {
                // Добавление категории в БДшку
                Core.GetContext().Categories.Add(new Category { NameCategory = CategoryNameTextBox.Text });
                Core.GetContext().SaveChanges();
                this.DialogResult = true;
                this.Close();
                
            }
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
