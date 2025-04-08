using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WPFSQLite.Models;

namespace WPFSQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ListEntity> Users { get; set; } = new ObservableCollection<ListEntity>();

        private ListEntity _selectedItem;

        public ListEntity SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Update();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            double salary = Convert.ToDouble(SalaryTextBox.Text);
            double cashPerDay = salary / 30;
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Имя пользователя пустое!");
                return;
            }
      
            if (!double.TryParse(SalaryTextBox.Text, out salary))
            {
                MessageBox.Show("Зарплата неверная!");
                return;
            }
            


            User newUser = new User { FullName = fullName, Salary = salary};
            if (!Core.GetContext().Users.Any(u => u.FullName == newUser.FullName))
            {
                Core.GetContext().Users.Add(newUser);
                Core.GetContext().SaveChanges();
                Update();
            }
            else
            {
                MessageBox.Show("Такой пользователь уже существует!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ListEntity? selectedUser = UserListBox.SelectedItem as ListEntity;
            if (selectedUser == null)
            {
                MessageBox.Show("Вы не выбрали пользователя!");
                return;
            }

            Core.GetContext().Users.Remove(selectedUser.User);
            Core.GetContext().SaveChanges();
            Update();
        }

        private void Update()
        {   
            List<Category> categories = Core.GetContext().Categories.ToList();

            categories.Add(new Category { Id = -1, NameCategory = "+ Добавить категорию" });

            List<ListEntity> users = Core.GetContext().Users.Select(u => new ListEntity()
            {
                User = u,
                SelectedCategory = null,
                Categories = categories,

    }).ToList();

            Users.Clear();

            foreach (ListEntity user in users)
            {
                Users.Add(user);
            }

        }

        private void AddCashButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = UserListBox.SelectedItem as ListEntity;
            CategoryUser categoryUser = new CategoryUser();
            try
            {
                if (UserListBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя из списка");
                    return;
                }
                if (SelectedItem == null || SelectedItem.User == null)
                {
                    MessageBox.Show("Ошибка: не удалось получить данные пользователя");
                    return;
                }
                if (SelectedItem.Cash <= 0)
                {
                    MessageBox.Show("Сумма должна быть больше нуля");
                    return;
                }
                    categoryUser.User = SelectedItem.User;
                    categoryUser.Category = SelectedItem.SelectedCategory;
                    categoryUser.Cash = SelectedItem.Cash;
                    double cash = categoryUser.Cash;
                    categoryUser.User.Salary = categoryUser.User.Salary - cash;
                if (categoryUser.User.Salary >= 0)
                { 
                    Core.GetContext().CategoryUsers.Add(categoryUser);
                    Core.GetContext().SaveChanges();
                    Update();
                }
                else 
                {
                    MessageBox.Show("Недостаточно средств");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }


        private void CategotyComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox categoryComboBox = sender as ComboBox;
            if ((categoryComboBox.SelectedItem as Category).Id == -1)
            {
                DialogWindow dialog = new DialogWindow();
                dialog.Owner = this;
                if (dialog.ShowDialog() == true)
                {
                    MessageBox.Show("Добавлена новая категория!");
                    Update();
                }
            }
        }
    }
}