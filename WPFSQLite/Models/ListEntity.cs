using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFSQLite.Models
{
    public class ListEntity : INotifyPropertyChanged
    {
		private User _user;

		public User User
		{
			get { return _user; }
			set { _user = value; OnPropertyChanged(nameof(User)); }
		}

		private double _cash = 0;

		public double Cash
		{
			get { return _cash; }
			set { _cash = value; OnPropertyChanged(nameof(Cash)); }
		}


		private Category? _selectedCategory;

		public Category? SelectedCategory
        {
			get { return _selectedCategory; }
			set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
		}


		private List<Category> _categories;

        public List<Category> Categories
		{
			get { return _categories; }
			set { _categories = value; OnPropertyChanged(nameof(Categories));  }
		}

        public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
			}
		}
    }
}
