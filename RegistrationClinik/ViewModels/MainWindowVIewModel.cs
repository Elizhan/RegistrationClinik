using Microsoft.EntityFrameworkCore;
using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using RegistrationClinik.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class MainWindowVIewModel : BaseViewModel
    {
        public MainWindowVIewModel()
        {
            GetAllDate();
            ArchiveCommand = new LambdaCommand(ArchiveCommandExcecute, CanArchiveCommandExcecuted);
            EditCommand = new LambdaCommand(EditCommandExcecute, CanEditCommandExcecuted);
        }
        #region Props

        private ObservableCollection<DBTable> clientCollection;
        public ObservableCollection<DBTable> ClientCollection
        {
            get { return clientCollection; }
            set 
            { 
                Set(ref clientCollection, value); 
            }
        }

        private DBTable selectedClient = new();
        public DBTable SelectedClient
        {
            get { return selectedClient; }
            set
            {
                Set(ref selectedClient, value);
            }
        }

        public bool IsChange { get; set; } = false;

        #endregion
        public ICommand ArchiveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private void ArchiveCommandExcecute(object obj)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                db.DBArchives.Add(new DBArchive 
                {   
                    IsShow = 1,
                    Adres = SelectedClient.Adres,
                    Name = SelectedClient.Name,
                    Birday = SelectedClient.Birday,
                    Analiz = SelectedClient.Analiz,
                    LDoctor = SelectedClient.LDoctor,
                    Oplata = SelectedClient.Oplata,
                    RegistrationDate = SelectedClient.RegistrationDate,
                });
                db.Remove(SelectedClient);
                db.SaveChanges();
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

        private void EditCommandExcecute(object obj)
        {
            if((string)obj == "1")
                IsChange = false;
            else
                IsChange = true;
            new regClient(this).Show();
        }

        public void GetAllDate()
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                ClientCollection = new ObservableCollection<DBTable>(db.DBTables);
            }   
        }
    }
}
