using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace RegistrationClinik.ViewModels
{
    public class MainWindowVIewModel : BaseViewModel
    {
        public static bool VisButton = true;

        public MainWindowVIewModel()
        {
            Create = new LambdaCommand(CreateMethod, CanCloseApplicationExecate);
            GetAllData();
        }

        #region Props

        private DBTable? item = new DBTable();

        public DBTable? Item
        {
            get { return item; }
            set { Set(ref item, value); }
        }

        /*private string? name = "";

        public string? Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }
        private DateTime? birday;

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
        public ICommand CreateB { get; set; }

        public List<DBTable> _dBTables;
        public List<DBTable> dBTables
        {
            get { return _dBTables; }
            set { Set(ref _dBTables, value); }
        }

        public void CreateMethod(object o)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                if (!VisButton)
                {
                    var r = db.DBTables.FirstOrDefault(s=>s.Id == Item.Id);
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


                    var r1 = db.DBTablesB.FirstOrDefault(s => s.Id == Item.Id);
                    r1.Adres = Item.Adres;
                    r1.Analiz = Item.Analiz;
                    r1.Avans = Item.Avans;
                    r1.Birday = Item.Birday;
                    r1.LDoctor = Item.LDoctor;
                    r1.Name = Item.Name;
                    r1.Oplacheno = Item.Oplacheno;
                    r1.Oplata = Item.Oplata;
                    r1.Ostatok = Item.Ostatok;
                    r1.RegistrationDate = Item.RegistrationDate;
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
                        RegistrationDate = Item.RegistrationDate
                    });
                    db.DBTablesB.Add(new Models.DBTableB()
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
                        RegistrationDate = Item.RegistrationDate
                    });

                }
                db.SaveChanges();
                GetAllData();
                MessageBox.Show("Данные успешно сохранены");
                Item = new DBTable();
            }
        }

        public bool CanCloseApplicationExecate(object o)
        {
            return true;
        }

        public void GetAllData()
        {
            using (ApplicationConnect db = new ApplicationConnect())
                dBTables = new List<DBTable>(db.DBTables);
        }
    }
}
