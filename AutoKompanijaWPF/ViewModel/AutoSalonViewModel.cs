using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class AutoSalonViewModel : BindableBase
    {
        #region Fields
        private int id;
        private int brojRaspolozivihAutomobila;
        private string grad;
        private string ulica;
        private ObservableCollection<AutoSalon> autoSaloni = new ObservableCollection<AutoSalon>();
        private AutoSalon selectedAutoSalon;
        private int currentIndex;
        private string addItemType;
        private string typeText;
        private string izmenaAddItemType;
        private string izmenaTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private int brojRaspAutoText;
        private string gradText;
        private string ulicaText;
        private int izmenaBrojRaspAutoTekst;
        private string izmenaGradTekst;
        private string izmenaUlicaTekst;
        public List<string> ComboBoxData { get; set; }

        #endregion

        #region Constructor
        public AutoSalonViewModel()
        {
            ComboBoxData = new List<string>();

            var context = new AutoKompanijaDbContext();

            List<AutoKompanija> listaAutoKomp = context.AutoKompanijas.ToList();
            foreach (var autoKomp in listaAutoKomp)
            {
                ComboBoxData.Add(autoKomp.Naziv);
            }

            AddItemType = ComboBoxData[0];

            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            AutoSaloni = new ObservableCollection<AutoSalon>(new AutoKompanijaDbContext().AutoSalons.ToList());
        }
        #endregion

        #region Props
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    OnPropertyChanged("CurrentIndex");
                    if (CurrentIndex >= 0)
                    {
                        int akId = AutoSaloni[CurrentIndex].AutoKompanijaId;
                        string nazivAutoKomp = "";

                        using (var db = new AutoKompanijaDbContext())
                        {
                            var result = db.AutoKompanijas.SingleOrDefault(a => a.Id == akId);
                            if (result != null)
                            {
                                nazivAutoKomp = result.Naziv;
                            }
                        }

                        IzmenaTypeText = nazivAutoKomp;
                        IzmenaAddItemType = nazivAutoKomp;
                        IzmenaBrojRaspAutoTekst = AutoSaloni[CurrentIndex].BrojRaspolozivihAutomobila;
                        IzmenaGradTekst = AutoSaloni[CurrentIndex].Grad;
                        IzmenaUlicaTekst = AutoSaloni[CurrentIndex].Ulica;
                    }
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string AddItemType
        {
            get { return addItemType; }
            set
            {
                if (addItemType != value)
                {
                    addItemType = value;
                    OnPropertyChanged("AddItemType");
                }
            }
        }

        public string TypeText
        {
            get { return typeText; }
            set
            {
                if (typeText != value)
                {
                    typeText = value;
                    OnPropertyChanged("TypeText");
                }
            }
        }

        public string IzmenaAddItemType
        {
            get { return izmenaAddItemType; }
            set
            {
                if (izmenaAddItemType != value)
                {
                    izmenaAddItemType = value;
                    OnPropertyChanged("IzmenaAddItemType");
                }
            }
        }

        public string IzmenaTypeText
        {
            get { return izmenaTypeText; }
            set
            {
                if (izmenaTypeText != value)
                {
                    izmenaTypeText = value;
                    OnPropertyChanged("IzmenaTypeText");
                }
            }
        }

        public int IzmenaBrojRaspAutoTekst
        {
            get { return izmenaBrojRaspAutoTekst; }
            set
            {
                izmenaBrojRaspAutoTekst = value;
                OnPropertyChanged("IzmenaBrojRaspAutoTekst");
            }
        }

        public string GradText
        {
            get { return gradText; }
            set
            {
                gradText = value;
                OnPropertyChanged("GradText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public int BrojRaspAutoText
        {
            get { return brojRaspAutoText; }
            set
            {
                brojRaspAutoText = value;
                OnPropertyChanged("BrojRaspAutoText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public string IzmenaGradTekst
        {
            get { return izmenaGradTekst; }
            set
            {
                izmenaGradTekst = value;
                OnPropertyChanged("IzmenaGradTekst");
            }
        }

        public string UlicaText
        {
            get { return ulicaText; }
            set
            {
                ulicaText = value;
                OnPropertyChanged("UlicaText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public string IzmenaUlicaTekst
        {
            get { return izmenaUlicaTekst; }
            set
            {
                izmenaUlicaTekst = value;
                OnPropertyChanged("IzmenaUlicaTekst");
            }
        }

        public AutoSalon SelectedAutoSalon
        {
            get { return selectedAutoSalon; }
            set
            {
                if (selectedAutoSalon != value)
                {
                    selectedAutoSalon = value;
                    OnPropertyChanged("SelectedAutoSalon");
                }
            }
        }

        public string Grad
        {
            get { return grad; }
            set
            {
                if (grad != value)
                {
                    grad = value;
                    OnPropertyChanged("Grad");
                }
            }
        }

        public string Ulica
        {
            get { return ulica; }
            set
            {
                if (ulica != value)
                {
                    ulica = value;
                    OnPropertyChanged("Ulica");
                }
            }
        }

        public int BrojRaspolozivihAutomobila
        {
            get { return brojRaspolozivihAutomobila; }
            set
            {
                if (brojRaspolozivihAutomobila != value)
                {
                    brojRaspolozivihAutomobila = value;
                    OnPropertyChanged("BrojRaspolozivihAutomobila");
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public ObservableCollection<AutoSalon> AutoSaloni
        {
            get { return autoSaloni; }
            set
            {
                if (autoSaloni != value)
                {
                    autoSaloni = value;
                    OnPropertyChanged("AutoSaloni");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = AutoSaloni[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.AutoSalons.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    var ak = db.AutoKompanijas.SingleOrDefault(a => a.Naziv == IzmenaTypeText);
                    result.AutoKompanijaId = ak.Id;
                    result.BrojRaspolozivihAutomobila = IzmenaBrojRaspAutoTekst;
                    result.Grad = IzmenaGradTekst;
                    result.Ulica = IzmenaUlicaTekst;
                    db.SaveChanges();
                }
            }

            AutoSaloni.Clear();
            AutoSaloni = new ObservableCollection<AutoSalon>(new AutoKompanijaDbContext().AutoSalons.ToList());
        }

        private bool CanEdit()
        {
            return CurrentIndex >= 0;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            AutoKompanija ak;
            using (var db = new AutoKompanijaDbContext())
            {
                ak = db.AutoKompanijas.SingleOrDefault(a => a.Naziv == TypeText);
            };

            AutoSalon autoSal = new AutoSalon()
            {
                AutoKompanijaId = ak.Id,
                BrojRaspolozivihAutomobila = brojRaspAutoText,
                Grad = GradText,
                Ulica = UlicaText
            };

            var context = new AutoKompanijaDbContext();

            context.AutoSalons.Add(autoSal);
            context.SaveChanges();

            AutoSaloni.Clear();
            AutoSaloni = new ObservableCollection<AutoSalon>(new AutoKompanijaDbContext().AutoSalons.ToList());

        }

        private bool CanAdd()
        {
            if (GradText != null && UlicaText != null)
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = AutoSaloni[CurrentIndex].Id;

            AutoSalon autoSal = context.AutoSalons.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.AutoSalons.Attach(autoSal);
            context.AutoSalons.Remove(autoSal);
            context.SaveChanges();

            AutoSaloni.Clear();
            AutoSaloni = new ObservableCollection<AutoSalon>(new AutoKompanijaDbContext().AutoSalons.ToList());
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion
    }
}
