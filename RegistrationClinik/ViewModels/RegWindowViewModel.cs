using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class RegWindowViewModel : BaseViewModel
    {
        private MainWindowVIewModel model;
        public RegWindowViewModel(MainWindowVIewModel _model)
        {
            model = _model;
            CreateCommand = new LambdaCommand(CreateCommandExcecute, CanCreateCommandExcecuted);
            if (model.IsChange)
                Item = model.SelectedClient;
        }
        public RegWindowViewModel()
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
            ButtonName = model.IsChange ? "Изменить" : "Сохранить";
            return true;
        }
        private void CreateCommandExcecute(object obj)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                if (model.IsChange)
                {
                    db.DBTables.Add(Item);
                }
                else 
                {
                    var result = db.DBTables.FirstOrDefault(s => s.Id == Item.Id);
                    result = Item;
                }
                db.SaveChanges();
                model.GetAllDate();
            }
        }
    }
}
