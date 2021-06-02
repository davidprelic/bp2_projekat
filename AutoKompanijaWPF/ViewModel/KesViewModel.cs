using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class KesViewModel : BindableBase
    {
        #region Fields
        private int id;
        private int vrednost;
        private ObservableCollection<Kes> kesPlacanja = new ObservableCollection<Kes>();
        private Kes selectedKesPlacanje;
        private int currentIndex;
        private int addItemType;
        private int typeText;
        private int izmenaAddItemType;
        private int izmenaTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private int vrednostText;
        private int izmenaVrednostTekst;
        public List<int> ComboBoxData { get; set; }
        public List<int> ComboBoxData2 { get; set; }

        #endregion

        #region Constructor
        public KesViewModel()
        {
            ComboBoxData = new List<int>();
            ComboBoxData2 = new List<int>();

            var context = new AutoKompanijaDbContext();

            List<Placanje> svaPlacanja = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            List<int> autoIdPlaceniKesom = new List<int>();
            foreach (var p in svaPlacanja)
            {
                if ((p as Kes) != null)
                {
                    autoIdPlaceniKesom.Add(p.AutomobilId);
                }
            }

            List<Automobil> listaAuta = context.Automobils.ToList();
            foreach (var auto in listaAuta)
            {
                if (auto.DatumNarucivanja != null)
                {
                    if (!autoIdPlaceniKesom.Contains(auto.Id))
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
                if ((p as Kes) != null)
                {
                    KesPlacanja.Add(p as Kes);
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
                        ComboBoxData2.Add(KesPlacanja[CurrentIndex].AutomobilId);
                        IzmenaTypeText = KesPlacanja[CurrentIndex].AutomobilId;
                        IzmenaAddItemType = KesPlacanja[CurrentIndex].AutomobilId;
                        IzmenaVrednostTekst = KesPlacanja[CurrentIndex].Vrednost;
                    }
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int VrednostText
        {
            get { return vrednostText; }
            set
            {
                if (vrednostText != value)
                {
                    vrednostText = value;
                    OnPropertyChanged("VrednostText");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int IzmenaVrednostTekst
        {
            get { return izmenaVrednostTekst; }
            set
            {
                if (izmenaVrednostTekst != value)
                {
                    izmenaVrednostTekst = value;
                    OnPropertyChanged("IzmenaVrednostTekst");
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

        public Kes SelectedKesPlacanje
        {
            get { return selectedKesPlacanje; }
            set
            {
                if (selectedKesPlacanje != value)
                {
                    selectedKesPlacanje = value;
                    OnPropertyChanged("SelectedKesPlacanje");
                }
            }
        }

        public int Vrednost
        {
            get { return vrednost; }
            set
            {
                if (vrednost != value)
                {
                    vrednost = value;
                    OnPropertyChanged("Vrednost");
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

        public ObservableCollection<Kes> KesPlacanja
        {
            get { return kesPlacanja; }
            set
            {
                if (kesPlacanja != value)
                {
                    kesPlacanja = value;
                    OnPropertyChanged("KesPlacanja");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = KesPlacanja[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Placanjes.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    Kes k = (Kes)result;
                    k.AutomobilId = IzmenaTypeText;
                    k.Vrednost = IzmenaVrednostTekst;
                    db.SaveChanges();
                }

                ComboBoxData.Clear();
                List<Placanje> svaPlacanja = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
                List<int> autoIdPlaceniKesom = new List<int>();
                foreach (var p in svaPlacanja)
                {
                    if ((p as Kes) != null)
                    {
                        autoIdPlaceniKesom.Add(p.AutomobilId);
                    }
                }

                List<Automobil> listaAuta = db.Automobils.ToList();
                foreach (var auto in listaAuta)
                {
                    if (auto.DatumNarucivanja != null)
                    {
                        if (!autoIdPlaceniKesom.Contains(auto.Id))
                            ComboBoxData.Add(auto.Id);
                    }
                }

                AddItemType = ComboBoxData[0];

            }

            KesPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Kes) != null)
                {
                    KesPlacanja.Add(p as Kes);
                }
            }
        }

        private bool CanEdit()
        {
            if (CurrentIndex >= 0 && IzmenaVrednostTekst > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            Kes k = new Kes()
            {
                AutomobilId = TypeText,
                Vrednost = VrednostText,
                DatumPlacanja = DateTime.Now
            };

            var context = new AutoKompanijaDbContext();

            context.Placanjes.Add(k);
            context.SaveChanges();

            KesPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Kes) != null)
                {
                    KesPlacanja.Add(p as Kes);
                }
            }
        }

        private bool CanAdd()
        {
            if (VrednostText > 0)
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = KesPlacanja[CurrentIndex].Id;

            Placanje ak = context.Placanjes.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Placanjes.Attach(ak);
            context.Placanjes.Remove(ak);
            context.SaveChanges();

            KesPlacanja.Clear();
            List<Placanje> plc = new List<Placanje>(new AutoKompanijaDbContext().Placanjes.ToList());
            foreach (var p in plc)
            {
                if ((p as Kes) != null)
                {
                    KesPlacanja.Add(p as Kes);
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
