using Microsoft.Win32;
using Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

        private IService<Person> _service = new Services.Database.Service<Person>();
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

            do
            {
                var window = new PersonWindow(person);
                var result = window.ShowDialog();
                if (result != true)
                    return;
                try
                {
                    person.Id = _service.Create(person);
                    People.Add(person);
                }
                catch
                {
                    //ignore
                }
            } while (person.Id == 0);

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;

            var dialog = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Json|*.json|ALL|*.*"
            };

            if (dialog.ShowDialog() != true)
                return;


           
            
            var content = JsonConvert.SerializeObject(SelectedPerson, _jsonSettings);
            
            File.WriteAllText(dialog.FileName, content);
            //using (var fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
            //using (var streamWriter = new StreamWriter(fileStream))
            //{
            //    //streamWriter.AutoFlush = true;
            //    streamWriter.Write(content);
            //    streamWriter.Flush();
            //}

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Json|*.json|ALL|*.*"
            };
            if (dialog.ShowDialog() != true)
                return;

            var content = File.ReadAllText(dialog.FileName);

            var person = JsonConvert.DeserializeObject<Person>(content);

            person.Id = _service.Create(person);
            People.Add(person);
        }

        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            DateFormatString = "yyyy dd MMM",
        };
    }
}
