using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class PlacanjeViewModel : BindableBase
    {
        #region Fields
        private int id;
        private DateTime datumPlacanja;
        private ObservableCollection<Placanje> placanja = new ObservableCollection<Placanje>();
        private Placanje selectedPlacanje;
        private int currentIndex;
        private int addItemType;
        private int typeText;
        private int izmenaAddItemType;
        private int izmenaTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private DateTime izmenaDatumPlacanja;
        public List<int> ComboBoxData { get; set; }


        #endregion

        #region Constructor
        public PlacanjeViewModel()
        {
            ComboBoxData = new List<int>();

            var context = new AutoKompanijaDbContext();

            List<Automobil> listaAuta = context.Automobils.ToList();
            foreach (var auto in listaAuta)
            {
                ComboBoxData.Add(auto.Id);
            }

            AddItemType = ComboBoxData[0];

            DatumPlacanja = DateTime.Now;
            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            Placanja = new ObservableCollection<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
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
                        IzmenaTypeText = Placanja[CurrentIndex].AutomobilId;
                        IzmenaAddItemType = Placanja[CurrentIndex].AutomobilId;
                        IzmenaDatumPlacanja = Placanja[CurrentIndex].DatumPlacanja;
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

        public DateTime IzmenaDatumPlacanja
        {
            get { return izmenaDatumPlacanja; }
            set
            {
                if (izmenaDatumPlacanja != value)
                {
                    izmenaDatumPlacanja = value;
                    OnPropertyChanged("IzmenaDatumPlacanja");
                }
            }
        }

        public Placanje SelectedPlacanje
        {
            get { return selectedPlacanje; }
            set
            {
                if (selectedPlacanje != value)
                {
                    selectedPlacanje = value;
                    OnPropertyChanged("SelectedPlacanje");
                }
            }
        }

        public DateTime DatumPlacanja
        {
            get { return datumPlacanja; }
            set
            {
                if (datumPlacanja != value)
                {
                    datumPlacanja = value;
                    OnPropertyChanged("DatumPlacanja");
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

        public ObservableCollection<Placanje> Placanja
        {
            get { return placanja; }
            set
            {
                if (placanja != value)
                {
                    placanja = value;
                    OnPropertyChanged("Placanja");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = Placanja[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Placanjes.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    result.AutomobilId = IzmenaTypeText;
                    result.DatumPlacanja = IzmenaDatumPlacanja;
                    db.SaveChanges();
                }
            }

            Placanja.Clear();
            Placanja = new ObservableCollection<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
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
                KamatnaStopa = 6,
                PeriodOtplate = 12,
                DatumPlacanja = DateTime.Now
            };
            //Placanje ak = new Placanje()
            //{
            //    AutomobilId = TypeText,
            //    DatumPlacanja = DatumPlacanja
            //};

            var context = new AutoKompanijaDbContext();

            context.Placanjes.Add(k);
            context.SaveChanges();

            Placanja.Clear();
            Placanja = new ObservableCollection<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());

        }

        private bool CanAdd()
        {
            if (DatumPlacanja != null)
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = Placanja[CurrentIndex].Id;

            Placanje ak = context.Placanjes.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Placanjes.Attach(ak);
            context.Placanjes.Remove(ak);
            context.SaveChanges();

            Placanja.Clear();
            Placanja = new ObservableCollection<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion







    }
}
