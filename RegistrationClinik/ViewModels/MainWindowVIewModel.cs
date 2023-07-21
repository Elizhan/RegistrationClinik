using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class MainWindowVIewModel : BaseViewModel
    {
        public static bool VisButton = true;

        public MainWindowVIewModel()
        {
            Create = new LambdaCommand(CreateMethod, CanCloseApplicationExecate);
            Archive = new LambdaCommand(ArchiveMethod, CanArchiveMethodExecute);
            GetAllData();
        }


        #region Props

        public ObservableCollection<DBTable>? dBTables = new ObservableCollection<DBTable>();
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

        private DBTable? item = new DBTable();
        public DBTable? Item
        {
            get { return item; }
            set { Set(ref item, value); }
        }

        private string? buttonName = "Сохранить";
        public string? ButtonName
        {
            get { return buttonName; }
            set
            {
                Set(ref buttonName, value);
            }
        }
        /*private DateTime? birday;

        public DateTime? Birday
        {
            get { return birday; }
            set { Set(ref birday, value); }
        }
        private string? adres = "";

        public string? Adres
        {
            get { return adres; }
            set { Set(ref adres, value); }
        }
        private string? analiz = "";

        public string? Analiz
        {
            get { return analiz; }
            set { Set(ref analiz, value); }
        }
        private string? lDoctor = "";

        public string? LDoctor
        {
            get { return lDoctor; }
            set { Set(ref lDoctor, value); }
        }
        private DateTime? registrationDate = DateTime.Now;

        public DateTime? RegistrationDate
        {
            get { return registrationDate; }
            set { Set(ref registrationDate, value); }
        }
        private decimal? avans = 0;

        public decimal? Avans
        {
            get { return avans; }
            set { Set(ref avans, value); }
        }
        private decimal? ostatok = 0;

        public decimal? Ostatok
        {
            get { return ostatok; }
            set { Set(ref ostatok, value); }
        }
        private decimal? oplacheno = 0;

        public decimal? Oplacheno
        {
            get { return oplacheno; }
            set { Set(ref oplacheno, value); }
        }
        private decimal? oplata = 0;

        public decimal? Oplata
        {
            get { return oplata; }
            set { Set(ref oplata, value); }
        }*/

        #endregion

        public ICommand Create { get; set; }
        public ICommand Archive { get; set; }


        public void CreateMethod(object o)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                if (!VisButton)
                {
                    var r = db.DBTables.FirstOrDefault(s => s.Id == Item.Id);
                    r.Adres = Item.Adres;
                    r.Analiz = Item.Analiz;
                    r.Avans = Item.Avans;
                    r.Birday = Item.Birday;
                    r.LDoctor = Item.LDoctor;
                    r.Name = Item.Name;
                    r.Oplacheno = Item.Oplacheno;
                    r.Oplata = Item.Oplata;
                    r.Ostatok = Item.Ostatok;
                    r.RegistrationDate = Item.RegistrationDate;
                }
                else
                {
                    db.DBTables.Add(new Models.DBTable()
                    {
                        Adres = Item.Adres,
                        Analiz = Item.Analiz,
                        Avans = Item.Avans,
                        Birday = Item.Birday,
                        LDoctor = Item.LDoctor,
                        Name = Item.Name,
                        Oplacheno = Item.Oplacheno,
                        Oplata = Item.Oplata,
                        Ostatok = Item.Ostatok,
                        RegistrationDate = Item.RegistrationDate,
                        IsShow = 1,
                    });

                }
                db.SaveChanges();
                GetAllData();
                MessageBox.Show("Данные успешно сохранены");
            }
        }

        public bool CanCloseApplicationExecate(object o)
        {
            return true;
        }
        private bool CanArchiveMethodExecute(object arg)
        {
            ButtonName = VisButton ? "Изменить" : "Сохранить";
            return VisButton;
        }

        private void ArchiveMethod(object obj)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {

                db.DBTables.Remove(db.DBTables.First(s => s.Id == Item.Id));

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
                db.SaveChanges();
                Item = new DBTable();
                DBTables = new ObservableCollection<DBTable>();
                GetAllData();
            }
        }
        public void GetAllData()
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                DBTables = new ObservableCollection<DBTable>(db.DBTables);
            }
        }
    }
}
