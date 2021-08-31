using Models;
using Services.InMemory;
using Services.Interfaces;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private IService<Person> _service = new Services.InMemory.Service<Person>();
        public ObservableCollection<Person> People { get; set; }
        public Person SelectedPerson { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;

            if(_service.Delete(SelectedPerson.Id))
                People.Remove(SelectedPerson);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(People)));
        }

        private void Refresh_Click(object sender = null, RoutedEventArgs e = null)
        {
            //var person = new Person() { FirstName = "Monika" };
            //People = new ObservableCollection<Person>
            //{
            //    person,
            //    new Person() { FirstName = "Ewa", LastName = "Ewowska", Gender = Gender.Female, BirthDate = new System.DateTime(1989, 12, 9) },
            //    new Person() { FirstName = "Damian", LastName = "Damianowski", },
            //    new Person() { BirthDate = new System.DateTime(1990, 1, 21) }
            //};
            People = new ObservableCollection<Person>(_service.Read());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(People)));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;

            var window = new PersonWindow((Person)SelectedPerson.Clone());
            var result = window.ShowDialog();
            if (result != true)
                return;

            _service.Update(SelectedPerson.Id, window.Person);
            Refresh_Click();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (People == null)
                return;

            var person = new Person();

            var window = new PersonWindow(person);
            var result = window.ShowDialog();
            if (result != true)
                return;

            person.Id = _service.Create(person);
            People.Add(person);
        }
    }
}
