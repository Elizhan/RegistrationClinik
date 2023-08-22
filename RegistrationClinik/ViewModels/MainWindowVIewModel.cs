using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using RegistrationClinik.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class MainWindowVIewModel : BaseViewModel
    {
        public MainWindowVIewModel()
        {
            GetAllDate();
            ArchiveCommand = new LambdaCommand(ArchiveCommandExcecute, CanArchiveCommandExcecuted);
            ShowArchiveWindowCommand = new LambdaCommand(ShowArchiveWindowCommandExcecute, CanShowArchiveWindowCommandExcecuted);
        }
        #region Props

        private ObservableCollection<ShowTableModel> clientCollection;
        public ObservableCollection<ShowTableModel> ClientCollection
        {
            get { return clientCollection; }
            set
            {
                Set(ref clientCollection, value);
            }
        }

        private ShowTableModel selectedClient = new();
        public ShowTableModel SelectedClient
        {
            get { return selectedClient; }
            set
            {
                Set(ref selectedClient, value);
            }
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                Set(ref searchText, value);
                if (string.IsNullOrEmpty(value))
                    GetAllDate();
                else
                    SearchByText(value);
            }
        }

        public bool IsChange { get; set; } = false;

        #endregion
        public ICommand ArchiveCommand { get; set; }
        public ICommand ShowArchiveWindowCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private void ArchiveCommandExcecute(object obj)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                var result = db.DBTables.FirstOrDefault(s => s.Id == selectedClient.Id);
                if (result != null)
                {
                    result.IsShow = 0;
                }
                db.SaveChanges();
                MessageBox.Show("Успешно!");
                GetAllDate();
            }
        }
        private bool CanArchiveCommandExcecuted(object arg)
        {
            return true;
        }
        private bool CanUpdateCommandExcecuted(object arg)
        {
            return true;
        }

        private bool CanEditCommandExcecuted(object arg)
        {
            return true;
        }
        private bool CanShowArchiveWindowCommandExcecuted(object arg)
        {
            return true;
        }

        private void ShowArchiveWindowCommandExcecute(object obj)
        {
            new Archive().Show();
        }
        public void GetAllDate()
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                var result = db.DBTables.Where(s => s.IsShow == 1).ToList();
                ClientCollection = new ObservableCollection<ShowTableModel>();
                for (int i = 0; i < result.Count; i++)
                    ClientCollection.Add(new ShowTableModel
                    {
                        Number = i + 1,
                        Name = result[i].Name,
                        Adres = result[i].Adres,
                        Analiz = result[i].Analiz,
                        Birday = result[i].Birday,
                        Id = result[i].Id,
                        LDoctor = result[i].LDoctor,
                        Oplacheno = result[i].Oplacheno,
                        Oplata = result[i].Oplata,
                        RegistrationDate = result[i].RegistrationDate,
                    });
            }
        }

        private void SearchByText(string value)
        {
            if (ClientCollection is null && ClientCollection == new ObservableCollection<ShowTableModel>())
                return;
            GetAllDate();
            ClientCollection = new ObservableCollection<ShowTableModel>(ClientCollection.Where(s => s.Name.Contains(value)));
        }
    }
}
