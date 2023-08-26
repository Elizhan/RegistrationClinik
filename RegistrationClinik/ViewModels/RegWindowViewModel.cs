using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class RegWindowViewModel : BaseViewModel
    {
        private readonly BMainWindowViewModel model;
        private readonly bool isBlack = true;
        public RegWindowViewModel(BMainWindowViewModel _model, bool _isBlack = true)
        {
            this.isBlack = _isBlack;
            CreateCommand = new LambdaCommand(CreateCommandExcecute, CanCreateCommandExcecuted);
            if (_model is not null)
            {
                model = _model;

                if (model.IsChange)
                {
                    ButtonName = "Изменить";
                    Item = new DBTable
                    {
                        Id = model.SelectedClient.Id,
                        Name = model.SelectedClient.Name,
                        Adres = model.SelectedClient.Adres,
                        Analiz = model.SelectedClient.Analiz,
                        KajBro = model.SelectedClient.KajBro,
                        Birday = model.SelectedClient.Birday,
                        IsShow = model.SelectedClient.IsShow,
                        LDoctor = model.SelectedClient.LDoctor,
                        Ostatok = model.SelectedClient.Ostatok,
                        Oplata = model.SelectedClient.Oplata,
                        PalataNumber = model.SelectedClient.PalataNumber,
                        Bonus = model.SelectedClient.Bonus,
                        Comments = model.SelectedClient.Comments,
                        TelNumber = model.SelectedClient.TelNumber,
                        RegistrationDate = model.SelectedClient.RegistrationDate
                    };
                }
            }
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
            return true;
        }
        private void CreateCommandExcecute(object obj)
        {
            if (Item is null) return;
            using (ApplicationConnect db = new ApplicationConnect())
            {
                if (isBlack)
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
                else
                {
                    Item.IsShow = 1;
                    db.DBTables.Add(Item);
                    db.SaveChanges();
                }
                MessageBox.Show("Успешено сохранено!");
            }
        }
    }
}
