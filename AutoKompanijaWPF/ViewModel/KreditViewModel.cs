using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class KreditViewModel : BindableBase
    {
        #region Fields
        private int id;
        private int kamatnaStopa;
        private int periodOtplate;
        private ObservableCollection<Kredit> kreditPlacanja = new ObservableCollection<Kredit>();
        private Kredit selectedKreditPlacanje;
        private int currentIndex;
        private int addItemType;
        private int typeText;
        private int izmenaAddItemType;
        private int izmenaTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private int kamatnaStopaText;
        private int periodOtplateText;
        private int izmenaKamatnaStopaTekst;
        private int izmenaPeriodOtplateTekst;
        public List<int> ComboBoxData { get; set; }
        #endregion

        #region Constructor
        public KreditViewModel()
        {
            ComboBoxData = new List<int>();

            var context = new AutoKompanijaDbContext();

            List<Automobil> listaAuta = context.Automobils.ToList();
            foreach (var auto in listaAuta)
            {
                ComboBoxData.Add(auto.Id);
            }

            AddItemType = ComboBoxData[0];

            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if((p as Kredit) != null)
                {
                    KreditPlacanja.Add(p as Kredit);
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
                        IzmenaTypeText = KreditPlacanja[CurrentIndex].AutomobilId;
                        IzmenaAddItemType = KreditPlacanja[CurrentIndex].AutomobilId;
                        IzmenaKamatnaStopaTekst = KreditPlacanja[CurrentIndex].KamatnaStopa;
                        IzmenaPeriodOtplateTekst = KreditPlacanja[CurrentIndex].PeriodOtplate;
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

        public int KamatnaStopaText
        {
            get { return kamatnaStopaText; }
            set
            {
                if (kamatnaStopaText != value)
                {
                    kamatnaStopaText = value;
                    OnPropertyChanged("KamatnaStopaText");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int IzmenaKamatnaStopaTekst
        {
            get { return izmenaKamatnaStopaTekst; }
            set
            {
                if (izmenaKamatnaStopaTekst != value)
                {
                    izmenaKamatnaStopaTekst = value;
                    OnPropertyChanged("IzmenaKamatnaStopaTekst");
                }
            }
        }

        public int PeriodOtplateText
        {
            get { return periodOtplateText; }
            set
            {
                if (periodOtplateText != value)
                {
                    periodOtplateText = value;
                    OnPropertyChanged("PeriodOtplateText");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int IzmenaPeriodOtplateTekst
        {
            get { return izmenaPeriodOtplateTekst; }
            set
            {
                if (izmenaPeriodOtplateTekst != value)
                {
                    izmenaPeriodOtplateTekst = value;
                    OnPropertyChanged("IzmenaPeriodOtplateTekst");
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

        public Kredit SelectedKreditPlacanje
        {
            get { return selectedKreditPlacanje; }
            set
            {
                if (selectedKreditPlacanje != value)
                {
                    selectedKreditPlacanje = value;
                    OnPropertyChanged("SelectedKreditPlacanje");
                }
            }
        }

        public int PeriodOtplate
        {
            get { return periodOtplate; }
            set
            {
                if (periodOtplate != value)
                {
                    periodOtplate = value;
                    OnPropertyChanged("PeriodOtplate");
                }
            }
        }


        public int KamatnaStopa
        {
            get { return kamatnaStopa; }
            set
            {
                if (kamatnaStopa != value)
                {
                    kamatnaStopa = value;
                    OnPropertyChanged("KamatnaStopa");
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

        public ObservableCollection<Kredit> KreditPlacanja
        {
            get { return kreditPlacanja; }
            set
            {
                if (kreditPlacanja != value)
                {
                    kreditPlacanja = value;
                    OnPropertyChanged("KreditPlacanja");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = KreditPlacanja[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Placanjes.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    Kredit k = (Kredit)result;
                    k.AutomobilId = IzmenaTypeText;
                    k.KamatnaStopa = IzmenaKamatnaStopaTekst;
                    k.PeriodOtplate = IzmenaPeriodOtplateTekst;
                    db.SaveChanges();
                }
            }

            KreditPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Kredit) != null)
                {
                    KreditPlacanja.Add(p as Kredit);
                }
            }
        }

        private bool CanEdit()
        {
            return CurrentIndex >= 0;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            Kredit k = new Kredit()
            {
                AutomobilId = TypeText,
                KamatnaStopa = KamatnaStopaText,
                PeriodOtplate = PeriodOtplateText,
                DatumPlacanja = DateTime.Now
            };
            
            var context = new AutoKompanijaDbContext();

            context.Placanjes.Add(k);
            context.SaveChanges();

            KreditPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Kredit) != null)
                {
                    KreditPlacanja.Add(p as Kredit);
                }
            }
        }

        private bool CanAdd()
        {
            if (KamatnaStopaText > 0 && PeriodOtplateText > 0)
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = KreditPlacanja[CurrentIndex].Id;

            Placanje ak = context.Placanjes.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Placanjes.Attach(ak);
            context.Placanjes.Remove(ak);
            context.SaveChanges();

            KreditPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Kredit) != null)
                {
                    KreditPlacanja.Add(p as Kredit);
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
