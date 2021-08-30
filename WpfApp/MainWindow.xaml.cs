using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;

            var person = new Person() { FirstName = "Monika" };
            People = new ObservableCollection<Person>
            {
                person,
                new Person() { FirstName = "Ewa", LastName = "Ewowska", Gender = Gender.Female, BirthDate = new System.DateTime(1989, 12, 9) },
                new Person() { FirstName = "Damian", LastName = "Damianowski", },
                new Person() { BirthDate = new System.DateTime(1990, 1, 21) }
            };

            InitializeComponent();

        }

        //public Array GenderSource { get; } = Enum.GetValues(typeof(Gender));
        public ObservableCollection<Person> People { get; set; }
        public Person Person { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Person == null)
                return;

            People.Remove(Person);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(People)));
        }
    }
}
