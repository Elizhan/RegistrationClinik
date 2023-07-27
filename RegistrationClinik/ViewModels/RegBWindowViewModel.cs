using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class RegBWindowViewModel : BaseViewModel
    {
        private BMainWindowViewModel model;
        public RegBWindowViewModel(BMainWindowViewModel _model)
        {
            model = _model;
            CreateCommand = new LambdaCommand(CreateCommandExcecute, CanCreateCommandExcecuted);
            if (model.IsChange)
            {
                ButtonName = "Изменить";
                Item = new DBTable
                {
                    Id = model.SelectedClient.Id,
                    Name = model.SelectedClient.Name,
                    Adres = model.SelectedClient.Adres,
                    Analiz = model.SelectedClient.Analiz,
                    Avans = model.SelectedClient.Avans,
                    Birday = model.SelectedClient.Birday,
                    IsShow = model.SelectedClient.IsShow,
                    LDoctor = model.SelectedClient.LDoctor,
                    Oplacheno = model.SelectedClient.Oplacheno,
                    Oplata = model.SelectedClient.Oplata,
                    Ostatok = model.SelectedClient.Ostatok,
                    RegistrationDate = model.SelectedClient.RegistrationDate
                };
            }
        }
        public RegBWindowViewModel()
        {

        }

        private DBTable item = new();
        public DBTable Item
        {
            get { return item; }
            set { Set(ref item, value); }
        }
        private string buttonName = "Сохранить";
        public string ButtonName
        {
            get { return buttonName; }
            set { buttonName = value; }
        }
        public ICommand CreateCommand { get; set; }
        private bool CanCreateCommandExcecuted(object arg)
        {
            return true;
        }
        private void CreateCommandExcecute(object obj)
        {
            if (Item is null) return;
            Item.IsShow = 0;
            using (ApplicationConnect db = new ApplicationConnect())
            {
                if (!model.IsChange)
                {
                    db.DBTables.Add(Item);
                }
                else
                {
                    var result = db.DBTables.FirstOrDefault(s => s.Id == Item.Id);
                    db.DBTables.Remove(result);
                    db.SaveChanges();
                    db.DBTables.Add(Item);
                }
                db.SaveChanges();
                model.GetAllDate();
            }
        }
    }
}
