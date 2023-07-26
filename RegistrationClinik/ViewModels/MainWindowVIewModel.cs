using Microsoft.EntityFrameworkCore;
using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using RegistrationClinik.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class MainWindowVIewModel : BaseViewModel
    {
        public bool VisButton = true;
        public MainWindowVIewModel()
        {
            GetAllData();

            Edit = new LambdaCommand(EditMethod, CanEditMethodExecute);
            Archive = new LambdaCommand(ArchiveMethod, CanArchiveMethodExecute);
            Update = new LambdaCommand(UpdateM, CanArchiveMethodExecute);
        }


        #region Props

        public ObservableCollection<DBTable>? dBTables = new();
        public ObservableCollection<DBTable>? DBTables
        {
            get
            {
                return dBTables;
            }
            set
            {
                Set(ref dBTables, value);
            }
        }

        private DBTable item = new DBTable();
        public DBTable Item
        {
            get { return item; }
            set
            {
                if (value is null)
                    item = new DBTable();
                else
                    Set(ref item, value);
            }
        }
        public ICommand Archive { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Update { get; set; }

        private bool CanEditMethodExecute(object arg)
        {
            return true;
        }

        private void EditMethod(object obj)
        {
            new regClient(Item.Id).Show();
        }

        private bool CanArchiveMethodExecute(object arg)
        {
            return true;
        }

        private void ArchiveMethod(object obj)
        {
            using (ApplicationConnect db = new())
            {
                db.DBArchives.Add(new Models.DBArchive()
                {
                    Adres = Item.Adres,
                    Analiz = Item.Analiz,
                    Birday = Item.Birday,
                    LDoctor = Item.LDoctor,
                    Name = Item.Name,
                    Oplata = Item.Oplata,
                    RegistrationDate = Item.RegistrationDate,
                    IsShow = 1,
                });

                db.DBTables.Remove(db.DBTables.First(s => s.Id == Item.Id));
                db.SaveChanges();

                GetAllData();
            }
        }

        //private string? name = string.Empty;
        //public string? Name
        //{
        //    get { return name; }
        //    set
        //    {
        //        Set(ref name, value);
        //    }
        //}
        //private DateTime? birday;

        //public DateTime? Birday
        //{
        //    get { return birday; }
        //    set { Set(ref birday, value); }
        //}
        //private string? adres = "";

        //public string? Adres
        //{
        //    get { return adres; }
        //    set { Set(ref adres, value); }
        //}
        //private string? analiz = "";

        //public string? Analiz
        //{
        //    get { return analiz; }
        //    set { Set(ref analiz, value); }
        //}
        //private string? lDoctor = "";

        //public string? LDoctor
        //{
        //    get { return lDoctor; }
        //    set { Set(ref lDoctor, value); }
        //}
        //private DateTime? registrationDate = DateTime.Now;

        //public DateTime? RegistrationDate
        //{
        //    get { return registrationDate; }
        //    set { Set(ref registrationDate, value); }
        //}
        //private decimal? avans = 0;

        //public decimal? Avans
        //{
        //    get { return avans; }
        //    set { Set(ref avans, value); }
        //}
        //private decimal? ostatok = 0;

        //public decimal? Ostatok
        //{
        //    get { return ostatok; }
        //    set { Set(ref ostatok, value); }
        //}
        //private decimal? oplacheno = 0;
        //public decimal? Oplacheno
        //{
        //    get { return oplacheno; }
        //    set { Set(ref oplacheno, value); }
        //}
        //private decimal? oplata = 0;
        //public decimal? Oplata
        //{
        //    get { return oplata; }
        //    set { Set(ref oplata, value); }
        //}

        #endregion

        public void UpdateM(object o)
        {
            GetAllData();
        }
        public void GetAllData()
        {
            using ApplicationConnect db = new();
            var result = db.DBTables;
            foreach (DBTable table in result)
            {
                DBTables.Add(table);
            }
        }
    }
}
