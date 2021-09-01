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
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

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

        private IAsyncService<Person> _service = new Services.WebApi.Service<Person>("http://localhost:52369", "csharp/People");
        public ObservableCollection<Person> People { get; set; }
        public Person SelectedPerson { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;

            if(await _service.DeleteAsync(SelectedPerson.Id))
                People.Remove(SelectedPerson);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(People)));
        }

        private async void Refresh_Click(object sender = null, RoutedEventArgs e = null)
        {
            var threaId = Thread.CurrentThread.ManagedThreadId;

            //var person = new Person() { FirstName = "Monika" };
            //People = new ObservableCollection<Person>
            //{
            //    person,
            //    new Person() { FirstName = "Ewa", LastName = "Ewowska", Gender = Gender.Female, BirthDate = new System.DateTime(1989, 12, 9) },
            //    new Person() { FirstName = "Damian", LastName = "Damianowski", },
            //    new Person() { BirthDate = new System.DateTime(1990, 1, 21) }
            //};
            People = new ObservableCollection<Person>(await _service.ReadAsync());

            threaId = Thread.CurrentThread.ManagedThreadId;
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

            _service.UpdateAsync(SelectedPerson.Id, window.Person);
            Refresh_Click();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
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
                    person.Id = await _service.CreateAsync(person);
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
                Filter = "Json|*.json|XML|*.xml"
            };

            if (dialog.ShowDialog() != true)
                return;

            var content = JsonConvert.SerializeObject(SelectedPerson, _jsonSettings);

            if (dialog.FileName.EndsWith("xml"))
            {
                var xDoc = JsonConvert.DeserializeXNode(content, nameof(Person));
                xDoc.Save(dialog.FileName);
            }
            else if (dialog.FileName.EndsWith("json"))
            {
                File.WriteAllText(dialog.FileName, content);
                //using (var fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                //using (var streamWriter = new StreamWriter(fileStream))
                //{
                //    //streamWriter.AutoFlush = true;
                //    streamWriter.Write(content);
                //    streamWriter.Flush();
                //}
            }
        }

        private void ExportToXML(SaveFileDialog dialog)
        {
            /*XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                            xmlWriterSettings.Indent = true;

                            using (XmlWriter writer = XmlWriter.Create(dialog.FileName, xmlWriterSettings))
                            {
                                writer.WriteStartElement(nameof(Person));

                                //writer.WriteElementString(nameof(Person.FirstName), SelectedPerson.FirstName);
                                //writer.WriteElementString(nameof(Person.LastName), SelectedPerson.LastName);
                                SelectedPerson.GetType()
                                    .GetProperties()
                                    .Where(x => x.CanRead)
                                    .ToList()
                                    .ForEach(x => writer.WriteElementString(x.Name, x.GetValue(SelectedPerson)?.ToString()));


                                writer.WriteEndElement();
                            }*/

            XDocument doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                var serializer = new DataContractSerializer(SelectedPerson.GetType());
                serializer.WriteObject(writer, SelectedPerson);
            }
            doc.Save(dialog.FileName);

            //var name = doc.Root.Elements().SingleOrDefault(x => x.Name.LocalName == "FirstName")?.Value;
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Json|*.json|XML|*.xml"
            };
            if (dialog.ShowDialog() != true)
                return;

            var content = File.ReadAllText(dialog.FileName);
            if (dialog.FileName.EndsWith("xml"))
            {
                XDocument xmlDocument = XDocument.Parse(content);
                content = JsonConvert.SerializeXNode(xmlDocument, Newtonsoft.Json.Formatting.None, true);
                //BAD CODE
                content = content.Split('}')[1] + "}";
            }
         
            var person = JsonConvert.DeserializeObject<Person>(content);

                person.Id = await _service.CreateAsync(person);
                People.Add(person);
        }

        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings()
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            DateFormatString = "yyyy dd MMM",
        };
    }
}
