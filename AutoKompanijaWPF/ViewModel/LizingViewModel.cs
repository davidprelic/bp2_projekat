using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class LizingViewModel : BindableBase
    {
        #region Fields
        private int id;
        private int rataLizingNaknade;
        private string vrstaLizinga;
        private ObservableCollection<Lizing> lizingPlacanja = new ObservableCollection<Lizing>();
        private Lizing selectedLizingPlacanje;
        private int currentIndex;
        private int addItemType;
        private int typeText;
        private int izmenaAddItemType;
        private int izmenaTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private int rataLizingNakText;
        private string vrstaLizingaText;
        private int izmenaRataLizingNakTekst;
        private string izmenaVrstaLizingaTekst;
        public List<int> ComboBoxData { get; set; }
        public List<int> ComboBoxData2 { get; set; }
        #endregion

        #region Constructor
        public LizingViewModel()
        {
            ComboBoxData = new List<int>();
            ComboBoxData2 = new List<int>();

            var context = new AutoKompanijaDbContext();

            List<Placanje> svaPlacanja = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            List<int> autoIdPlaceniLizingom = new List<int>();
            foreach (var p in svaPlacanja)
            {
                if ((p as Lizing) != null)
                {
                    autoIdPlaceniLizingom.Add(p.AutomobilId);
                }
            }

            List<Automobil> listaAuta = context.Automobils.ToList();
            foreach (var auto in listaAuta)
            {
                if (auto.DatumNarucivanja != null)
                {
                    if (!autoIdPlaceniLizingom.Contains(auto.Id))
                        ComboBoxData.Add(auto.Id);
                }
            }

            AddItemType = ComboBoxData[0];

            foreach (var item in ComboBoxData)
            {
                ComboBoxData2.Add(item);
            }

            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Lizing) != null)
                {
                    LizingPlacanja.Add(p as Lizing);
                }
            }
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
                        ComboBoxData2.Add(LizingPlacanja[CurrentIndex].AutomobilId);
                        IzmenaTypeText = LizingPlacanja[CurrentIndex].AutomobilId;
                        IzmenaAddItemType = LizingPlacanja[CurrentIndex].AutomobilId;
                        IzmenaRataLizingNakTekst = LizingPlacanja[CurrentIndex].RataLizingNaknade;
                        IzmenaVrstaLizingaTekst = LizingPlacanja[CurrentIndex].VrstaLizinga;
                    }
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public Lizing SelectedLizingPlacanje
        {
            get { return selectedLizingPlacanje; }
            set
            {
                if (selectedLizingPlacanje != value)
                {
                    selectedLizingPlacanje = value;
                    OnPropertyChanged("SelectedLizingPlacanje");
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

        public int RataLizingNakText
        {
            get { return rataLizingNakText; }
            set
            {
                if (rataLizingNakText != value)
                {
                    rataLizingNakText = value;
                    OnPropertyChanged("RataLizingNakText");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int IzmenaRataLizingNakTekst
        {
            get { return izmenaRataLizingNakTekst; }
            set
            {
                if (izmenaRataLizingNakTekst != value)
                {
                    izmenaRataLizingNakTekst = value;
                    OnPropertyChanged("IzmenaRataLizingNakTekst");
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string VrstaLizingaText
        {
            get { return vrstaLizingaText; }
            set
            {
                if (vrstaLizingaText != value)
                {
                    vrstaLizingaText = value;
                    OnPropertyChanged("VrstaLizingaText");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string IzmenaVrstaLizingaTekst
        {
            get { return izmenaVrstaLizingaTekst; }
            set
            {
                if (izmenaVrstaLizingaTekst != value)
                {
                    izmenaVrstaLizingaTekst = value;
                    OnPropertyChanged("IzmenaVrstaLizingaTekst");
                    EditCommand.RaiseCanExecuteChanged();
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

        public string VrstaLizinga
        {
            get { return vrstaLizinga; }
            set
            {
                if (vrstaLizinga != value)
                {
                    vrstaLizinga = value;
                    OnPropertyChanged("VrstaLizinga");
                }
            }
        }


        public int RataLizingNaknade
        {
            get { return rataLizingNaknade; }
            set
            {
                if (rataLizingNaknade != value)
                {
                    rataLizingNaknade = value;
                    OnPropertyChanged("RataLizingNaknade");
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

        public ObservableCollection<Lizing> LizingPlacanja
        {
            get { return lizingPlacanja; }
            set
            {
                if (lizingPlacanja != value)
                {
                    lizingPlacanja = value;
                    OnPropertyChanged("LizingPlacanja");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = LizingPlacanja[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Placanjes.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    Lizing k = (Lizing)result;
                    k.AutomobilId = IzmenaTypeText;
                    k.RataLizingNaknade = IzmenaRataLizingNakTekst;
                    k.VrstaLizinga = IzmenaVrstaLizingaTekst;
                    db.SaveChanges();
                }

                ComboBoxData.Clear();
                List<Placanje> svaPlacanja = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
                List<int> autoIdPlaceniLizingom = new List<int>();
                foreach (var p in svaPlacanja)
                {
                    if ((p as Lizing) != null)
                    {
                        autoIdPlaceniLizingom.Add(p.AutomobilId);
                    }
                }

                List<Automobil> listaAuta = db.Automobils.ToList();
                foreach (var auto in listaAuta)
                {
                    if (auto.DatumNarucivanja != null)
                    {
                        if (!autoIdPlaceniLizingom.Contains(auto.Id))
                            ComboBoxData.Add(auto.Id);
                    }
                }

                AddItemType = ComboBoxData[0];
            }

            LizingPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Lizing) != null)
                {
                    LizingPlacanja.Add(p as Lizing);
                }
            }
        }

        private bool CanEdit()
        {
            if (CurrentIndex >= 0 && IzmenaRataLizingNakTekst >= 0 && IzmenaVrstaLizingaTekst != null && IzmenaVrstaLizingaTekst != "")
                return true;
            else
                return false;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            Lizing k = new Lizing()
            {
                AutomobilId = TypeText,
                RataLizingNaknade = RataLizingNakText,
                VrstaLizinga = VrstaLizingaText,
                DatumPlacanja = DateTime.Now
            };

            var context = new AutoKompanijaDbContext();

            context.Placanjes.Add(k);
            context.SaveChanges();

            LizingPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Lizing) != null)
                {
                    LizingPlacanja.Add(p as Lizing);
                }
            }
        }

        private bool CanAdd()
        {
            if (RataLizingNakText > 0 && VrstaLizingaText != null && VrstaLizingaText != "")
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = LizingPlacanja[CurrentIndex].Id;

            Placanje ak = context.Placanjes.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Placanjes.Attach(ak);
            context.Placanjes.Remove(ak);
            context.SaveChanges();

            LizingPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Lizing) != null)
                {
                    LizingPlacanja.Add(p as Lizing);
                }
            }
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion






    }
}
