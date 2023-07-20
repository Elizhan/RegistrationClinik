using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace RegistrationClinik.ViewModels
{
    public class BMainWindowViewModel : BaseViewModel
    {
        public static bool VisButtonB = true;

        public BMainWindowViewModel()
        {
            CreateB = new LambdaCommand(CreateMethodB, CanCloseApplicationExecate);
            GetAllDataB();
        }

        #region Props

        private string? name = "";
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
        }

        #endregion

        public ICommand CreateB { get; set; }
        public void CreateMethodB(object o)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                db.DBTablesB.Add(new Models.DBTableB()
                {
                    Adres = adres,
                    Analiz = analiz,
                    Avans = avans,
                    Birday = birday,
                    LDoctor = lDoctor,
                    Name = name,
                    Oplacheno = oplacheno,
                    Oplata = oplata,
                    Ostatok = ostatok,
                    RegistrationDate = registrationDate
                });
                db.SaveChanges();
                GetAllDataB();
                MessageBox.Show("Данные успешно сохранены");
                Adres = "";
                Analiz = "";
                Avans = 0;
                Birday = null;
                LDoctor = "";
                Name = "";
                Oplacheno = 0;
                Oplata = 0;
                Ostatok = 0;
                RegistrationDate = DateTime.Now;
            }
        }
        public bool CanCloseApplicationExecate(object o)
        {
            return true;
        }

        public List<DBTableB> _dBTablesB;
        public List<DBTableB> dBTablesB
        {
            get { return _dBTablesB; }
            set { Set(ref _dBTablesB, value); }
        }

        public void GetAllDataB()
        {
            using (ApplicationConnect db = new ApplicationConnect())
                dBTablesB = new List<DBTableB>(db.DBTablesB);
        }
    }
}
