using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class AutomobilViewModel : BindableBase
    {
        #region Fields
        private int id;
        private string marka;
        private string model;
        private int cena;
        private DateTime datumNarucivanja;
        private ObservableCollection<Automobil> automobili = new ObservableCollection<Automobil>();
        private Automobil selectedAutomobil;
        private int currentIndex;
        private int addItemType;
        private int typeText;
        private int izmenaAddItemType;
        private int izmenaTypeText;
        private int kupacAddItemType;
        private int kupacTypeText;
        private int automobilAddItemType;
        private int automobilTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand NaruciCommand { get; set; }

        private int cenaText;
        private string markaText;
        private string modelText;
        private int izmenaCenaTekst;
        private string izmenaMarkaTekst;
        private string izmenaModelTekst;
        public List<int> ComboBoxData { get; set; }
        public List<int> ComboBoxKupci { get; set; } = new List<int>();
        public List<int> ComboBoxAutomobili { get; set; } = new List<int>();

        #endregion

        #region Constructor
        public AutomobilViewModel()
        {
            ComboBoxData = new List<int>();
            
            var context = new AutoKompanijaDbContext();

            List<AutoSalon> listaAutoSalona = context.AutoSalons.ToList();
            foreach (var autoSal in listaAutoSalona)
            {
                ComboBoxData.Add(autoSal.Id);
            }

            AddItemType = ComboBoxData[0];

            List<Kupac> listaKupaca = context.Kupacs.ToList();
            foreach (var kupac in listaKupaca)
            {
                ComboBoxKupci.Add(kupac.Id);
            }

            KupacAddItemType = ComboBoxKupci[0];

            List<Automobil> listaAutomobila = context.Automobils.ToList();
            foreach (var auto in listaAutomobila)
            {
                if(auto.DatumNarucivanja == null)
                {
                    ComboBoxAutomobili.Add(auto.Id);
                }
            }

            AutomobilAddItemType = ComboBoxAutomobili[0];

            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            NaruciCommand = new MyICommand(OnNaruci, CanNaruci);
            Automobili = new ObservableCollection<Automobil>(new AutoKompanijaDbContext().Automobils.ToList());
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
                        if(!Automobili[CurrentIndex].AutoSalonId.HasValue)
                        {
                            IzmenaTypeText = 0;
                            IzmenaAddItemType = 0;
                        }
                        else
                        {
                            IzmenaTypeText = Automobili[CurrentIndex].AutoSalonId.Value;
                            IzmenaAddItemType = Automobili[CurrentIndex].AutoSalonId.Value;
                        }

                        IzmenaCenaTekst = Automobili[CurrentIndex].Cena;
                        IzmenaMarkaTekst = Automobili[CurrentIndex].Marka;
                        IzmenaModelTekst = Automobili[CurrentIndex].Model;
                    }
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int AddItemType
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

        public int KupacAddItemType
        {
            get { return kupacAddItemType; }
            set
            {
                if (kupacAddItemType != value)
                {
                    kupacAddItemType = value;
                    OnPropertyChanged("KupacAddItemType");
                }
            }
        }

        public int AutomobilAddItemType
        {
            get { return automobilAddItemType; }
            set
            {
                if (automobilAddItemType != value)
                {
                    automobilAddItemType = value;
                    OnPropertyChanged("AutomobilAddItemType");
                }
            }
        }

        public int TypeText
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

        public int KupacTypeText
        {
            get { return kupacTypeText; }
            set
            {
                if (kupacTypeText != value)
                {
                    kupacTypeText = value;
                    OnPropertyChanged("KupacTypeText");
                    NaruciCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int AutomobilTypeText
        {
            get { return automobilTypeText; }
            set
            {
                if (automobilTypeText != value)
                {
                    automobilTypeText = value;
                    OnPropertyChanged("AutomobilTypeText");
                    NaruciCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int IzmenaAddItemType
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

        public int IzmenaTypeText
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

        public int IzmenaCenaTekst
        {
            get { return izmenaCenaTekst; }
            set
            {
                izmenaCenaTekst = value;
                OnPropertyChanged("IzmenaCenaTekst");
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public string MarkaText
        {
            get { return markaText; }
            set
            {
                markaText = value;
                OnPropertyChanged("MarkaText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public int CenaText
        {
            get { return cenaText; }
            set
            {
                cenaText = value;
                OnPropertyChanged("CenaText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public string IzmenaMarkaTekst
        {
            get { return izmenaMarkaTekst; }
            set
            {
                izmenaMarkaTekst = value;
                OnPropertyChanged("IzmenaMarkaTekst");
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public string ModelText
        {
            get { return modelText; }
            set
            {
                modelText = value;
                OnPropertyChanged("ModelText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public string IzmenaModelTekst
        {
            get { return izmenaModelTekst; }
            set
            {
                izmenaModelTekst = value;
                OnPropertyChanged("IzmenaModelTekst");
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public Automobil SelectedAutomobil
        {
            get { return selectedAutomobil; }
            set
            {
                if (selectedAutomobil != value)
                {
                    selectedAutomobil = value;
                    OnPropertyChanged("SelectedAutomobil");
                }
            }
        }

        public DateTime DatumNarucivanja
        {
            get { return datumNarucivanja; }
            set
            {
                if (datumNarucivanja != value)
                {
                    datumNarucivanja = value;
                    OnPropertyChanged("DatumNarucivanja");
                }
            }
        }

        public int Cena
        {
            get { return cena; }
            set
            {
                if (cena != value)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }

        public string Model
        {
            get { return model; }
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        public string Marka
        {
            get { return marka; }
            set
            {
                if (marka != value)
                {
                    marka = value;
                    OnPropertyChanged("Marka");
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

        public ObservableCollection<Automobil> Automobili
        {
            get { return automobili; }
            set
            {
                if (automobili != value)
                {
                    automobili = value;
                    OnPropertyChanged("Automobili");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = Automobili[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Automobils.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    result.AutoSalonId = IzmenaTypeText;
                    result.Cena = IzmenaCenaTekst;
                    result.Marka = IzmenaMarkaTekst;
                    result.Model = IzmenaModelTekst;
                    db.SaveChanges();
                }
            }

            Automobili.Clear();
            Automobili = new ObservableCollection<Automobil>(new AutoKompanijaDbContext().Automobils.ToList());
        }

        private bool CanEdit()
        {
            if (CurrentIndex >= 0 && IzmenaCenaTekst > 0 && IzmenaMarkaTekst != null && IzmenaModelTekst != null &&
                IzmenaMarkaTekst != "" && IzmenaModelTekst != "")
                return true;
            else
                return false;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            Automobil auto = new Automobil();
            if (TypeText != 0)
            {
                auto.AutoSalonId = TypeText;
                auto.Cena = CenaText;
                auto.Marka = MarkaText;
                auto.Model = ModelText;
            }
            else
            {
                auto.Cena = CenaText;
                auto.Marka = MarkaText;
                auto.Model = ModelText;
            }

            var context = new AutoKompanijaDbContext();

            context.Automobils.Add(auto);
            context.SaveChanges();

            Automobili.Clear();
            Automobili = new ObservableCollection<Automobil>(new AutoKompanijaDbContext().Automobils.ToList());
        }

        private bool CanAdd()
        {
            if (MarkaText != null && ModelText != null && MarkaText != "" && ModelText != "" &&
                CenaText >= 0)
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = Automobili[CurrentIndex].Id;

            Automobil auto = context.Automobils.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Automobils.Attach(auto);
            context.Automobils.Remove(auto);
            context.SaveChanges();

            Automobili.Clear();
            Automobili = new ObservableCollection<Automobil>(new AutoKompanijaDbContext().Automobils.ToList());
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion

        #region NaruciFunctions
        private void OnNaruci()
        {
            using (var db = new AutoKompanijaDbContext())
            {
                var automobilResult = db.Automobils.SingleOrDefault(a => a.Id == AutomobilTypeText);
                var kupacResult = db.Kupacs.SingleOrDefault(a => a.Id == KupacTypeText);
                if (automobilResult != null)
                {
                    automobilResult.DatumNarucivanja = DateTime.Now;
                    automobilResult.KupacId = kupacResult.Id;
                    db.SaveChanges();
                }
            }

            Automobili.Clear();
            Automobili = new ObservableCollection<Automobil>(new AutoKompanijaDbContext().Automobils.ToList());
        }

        private bool CanNaruci()
        {
            if (AutomobilTypeText != 0 && KupacTypeText != 0)
                return true;
            return false;
        }
        #endregion





    }
}
