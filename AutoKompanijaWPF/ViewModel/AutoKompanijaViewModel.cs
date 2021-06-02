using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoKompanijaWPF.ViewModel
{
    public class AutoKompanijaViewModel : BindableBase
    {
        #region Fields
        private int id;
        private string naziv;
        private DateTime datumOsnivanja;
        private ObservableCollection<AutoKompanija> autoKompanije = new ObservableCollection<AutoKompanija>();
        private AutoKompanija selectedAutoKompanija;
        private int currentIndex;
        private int kompanijaAddItemType;
        private int kompanijaTypeText;
        private int sluzbenikAddItemType;
        private int sluzbenikTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand ZaposliCommand { get; set; }

        private string nazivText;
        private string izmenaNazivTekst;
        private DateTime izmenaDatumOsnivanja;
        public List<int> ComboBoxKompanije { get; set; } = new List<int>();
        public List<int> ComboBoxSluzbenici { get; set; } = new List<int>();
        #endregion

        #region Constructor
        public AutoKompanijaViewModel()
        {
            var context = new AutoKompanijaDbContext();

            List<AutoKompanija> listaKompanija = context.AutoKompanijas.ToList();
            foreach (var komp in listaKompanija)
            {
                ComboBoxKompanije.Add(komp.Id);
            }

            KompanijaAddItemType = ComboBoxKompanije[0];

            List<Sluzbenik> listaSluzbenika = context.Sluzbeniks.ToList();

            foreach (var sluzb in listaSluzbenika)
            {
                ComboBoxSluzbenici.Add(sluzb.Id);
            }

            SluzbenikAddItemType = ComboBoxSluzbenici[0];

            DatumOsnivanja = DateTime.Now;
            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            ZaposliCommand = new MyICommand(OnZaposli, CanZaposli);

            AutoKompanije = new ObservableCollection<AutoKompanija>(new AutoKompanijaDbContext().AutoKompanijas.ToList());
        }
        #endregion

        #region Props
        public DateTime IzmenaDatumOsnivanja
        {
            get { return izmenaDatumOsnivanja; }
            set
            {
                if (izmenaDatumOsnivanja != value)
                {
                    izmenaDatumOsnivanja = value;
                    OnPropertyChanged("IzmenaDatumOsnivanja");
                }
            }
        }

        public int KompanijaAddItemType
        {
            get { return kompanijaAddItemType; }
            set
            {
                if (kompanijaAddItemType != value)
                {
                    kompanijaAddItemType = value;
                    OnPropertyChanged("KompanijaAddItemType");
                }
            }
        }

        public int SluzbenikAddItemType
        {
            get { return sluzbenikAddItemType; }
            set
            {
                if (sluzbenikAddItemType != value)
                {
                    sluzbenikAddItemType = value;
                    OnPropertyChanged("SluzbenikAddItemType");
                }
            }
        }

        public int KompanijaTypeText
        {
            get { return kompanijaTypeText; }
            set
            {
                if (kompanijaTypeText != value)
                {
                    kompanijaTypeText = value;
                    OnPropertyChanged("KompanijaTypeText");
                    ZaposliCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int SluzbenikTypeText
        {
            get { return sluzbenikTypeText; }
            set
            {
                if (sluzbenikTypeText != value)
                {
                    sluzbenikTypeText = value;
                    OnPropertyChanged("SluzbenikTypeText");
                    ZaposliCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string IzmenaNazivTekst
        {
            get { return izmenaNazivTekst; }
            set
            {
                izmenaNazivTekst = value;
                OnPropertyChanged("IzmenaNazivTekst");
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public string NazivText
        {
            get { return nazivText; }
            set
            {
                nazivText = value;
                OnPropertyChanged("NazivText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

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
                        IzmenaNazivTekst = AutoKompanije[CurrentIndex].Naziv;
                        IzmenaDatumOsnivanja = AutoKompanije[CurrentIndex].DatumOsnivanja;
                    }
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public AutoKompanija SelectedAutoKompanija
        {
            get { return selectedAutoKompanija; }
            set
            {
                if (selectedAutoKompanija != value)
                {
                    selectedAutoKompanija = value;
                    OnPropertyChanged("SelectedAutoKompanija");
                }
            }
        }

        public DateTime DatumOsnivanja
        {
            get { return datumOsnivanja; }
            set
            {
                if (datumOsnivanja != value)
                {
                    datumOsnivanja = value;
                    OnPropertyChanged("DatumOsnivanja");
                }
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
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

        public ObservableCollection<AutoKompanija> AutoKompanije
        {
            get { return autoKompanije; }
            set
            {
                if (autoKompanije != value)
                {
                    autoKompanije = value;
                    OnPropertyChanged("AutoKompanije");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = AutoKompanije[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.AutoKompanijas.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    result.Naziv = IzmenaNazivTekst;
                    result.DatumOsnivanja = IzmenaDatumOsnivanja;
                    db.SaveChanges();
                }
            }

            AutoKompanije.Clear();
            AutoKompanije = new ObservableCollection<AutoKompanija>(new AutoKompanijaDbContext().AutoKompanijas.ToList());
        }

        private bool CanEdit()
        {
            if (IzmenaNazivTekst != "" && CurrentIndex >= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            AutoKompanija ak = new AutoKompanija()
            {
                Naziv = NazivText,
                DatumOsnivanja = DatumOsnivanja
            };

            var context = new AutoKompanijaDbContext();

            context.AutoKompanijas.Add(ak);
            context.SaveChanges();

            AutoKompanije.Clear();
            AutoKompanije = new ObservableCollection<AutoKompanija>(new AutoKompanijaDbContext().AutoKompanijas.ToList());

        }

        private bool CanAdd()
        {
            if (NazivText != null && DatumOsnivanja != null && NazivText != "")
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = AutoKompanije[CurrentIndex].Id;

            AutoKompanija ak = context.AutoKompanijas.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.AutoKompanijas.Attach(ak);
            context.AutoKompanijas.Remove(ak);
            context.SaveChanges();

            AutoKompanije.Clear();
            AutoKompanije = new ObservableCollection<AutoKompanija>(new AutoKompanijaDbContext().AutoKompanijas.ToList());
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion

        #region ZaposliFunctions
        private void OnZaposli()
        {
            using (var db = new AutoKompanijaDbContext())
            {
                var sluzbenikResult = db.Sluzbeniks.SingleOrDefault(a => a.Id == SluzbenikTypeText);
                var kompanijaResult = db.AutoKompanijas.SingleOrDefault(a => a.Id == KompanijaTypeText);
                if (sluzbenikResult != null && kompanijaResult != null)
                {
                    Zaposljava zapos = new Zaposljava()
                    {
                        AutoKompanijaId = kompanijaResult.Id,
                        SluzbenikId = sluzbenikResult.Id
                    };

                    db.Zaposljavas.Add(zapos);
                    db.SaveChanges();
                }
            }
        }

        private bool CanZaposli()
        {
            using (var db = new AutoKompanijaDbContext())
            {
                List<Zaposljava> zaposleni = db.Zaposljavas.ToList();

                foreach (var zap in zaposleni)
                {
                    if (KompanijaTypeText == zap.AutoKompanijaId && SluzbenikTypeText == zap.SluzbenikId)
                    {
                        return false;
                    }
                }
                return true;
                
            }
        }
        #endregion

    }

}
