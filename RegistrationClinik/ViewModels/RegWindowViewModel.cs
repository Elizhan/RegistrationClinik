using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RegistrationClinik.ViewModels
{
    public class RegWindowViewModel : BaseViewModel
    {
        public RegWindowViewModel(int id = 0)
        {
            Create = new LambdaCommand(CreateMethod, CanCloseApplicationExecate);

            if (id != 0) 
            {
                using ApplicationConnect db = new();
                Item = db.DBTables.FirstOrDefault(s=>s.Id == id);
            }
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

        private DBTable item = new();
        public DBTable Item
        {
            get { return item; }
            set
            {
                Set(ref item, value);
            }
        }
        public ICommand Create { get; set; }

        public void CreateMethod(object o)
        {
            using (ApplicationConnect db = new())
            {
                if (StaticFields.IsChange)
                {
                    var r = db.DBTables.FirstOrDefault(s => s.Id == Item.Id);
                    db.DBTables.Remove(r);
                    db.DBTables.Add(Item);
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
                MessageBox.Show("Данные успешно сохранены");
            }
        }

        public bool CanCloseApplicationExecate(object o)
        {
            ButtonName = StaticFields.IsChange ? "Изменить" : "Сохранить";
            return true;
        }
    }
}
