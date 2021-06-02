using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class SluzbenikViewModel : BindableBase
    {
        #region Fields
        private int id;
        private string ime;
        private string prezime;
        private DateTime datumZaposlenja;
        private ObservableCollection<Sluzbenik> sluzbenici = new ObservableCollection<Sluzbenik>();
        private Sluzbenik selectedSluzbenik;
        private int currentIndex;
        private int kupacAddItemType;
        private int kupacTypeText;
        private int sluzbenikAddItemType;
        private int sluzbenikTypeText;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }
        public MyICommand PregovorCommand { get; set; }


        private string imeText;
        private string prezimeText;
        private string izmenaImeTekst;
        private string izmenaPrezimeTekst;
        private DateTime izmenaDatumZaposlenja;
        public List<int> ComboBoxKupci { get; set; } = new List<int>();
        public List<int> ComboBoxSluzbenici { get; set; } = new List<int>();
        #endregion

        #region Constructor
        public SluzbenikViewModel()
        {
            var context = new AutoKompanijaDbContext();

            List<Kupac> listaKupaca = context.Kupacs.ToList();
            foreach (var kup in listaKupaca)
            {
                ComboBoxKupci.Add(kup.Id);
            }

            KupacAddItemType = ComboBoxKupci[0];

            List<Sluzbenik> listaSluzbenika = context.Sluzbeniks.ToList();

            foreach (var sluzb in listaSluzbenika)
            {
                ComboBoxSluzbenici.Add(sluzb.Id);
            }

            SluzbenikAddItemType = ComboBoxSluzbenici[0];

            DatumZaposlenja = DateTime.Now;
            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            PregovorCommand = new MyICommand(OnPregovor, CanPregovor);
            Sluzbenici = new ObservableCollection<Sluzbenik>(new AutoKompanijaDbContext().Sluzbeniks.ToList());
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
                        IzmenaImeTekst = Sluzbenici[CurrentIndex].Ime;
                        IzmenaPrezimeTekst = Sluzbenici[CurrentIndex].Prezime;
                        IzmenaDatumZaposlenja = Sluzbenici[CurrentIndex].DatumZaposlenja;
                    }
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string IzmenaImeTekst
        {
            get { return izmenaImeTekst; }
            set
            {
                izmenaImeTekst = value;
                OnPropertyChanged("IzmenaImeTekst");
                EditCommand.RaiseCanExecuteChanged();
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

        public int KupacTypeText
        {
            get { return kupacTypeText; }
            set
            {
                if (kupacTypeText != value)
                {
                    kupacTypeText = value;
                    OnPropertyChanged("KupacTypeText");
                    PregovorCommand.RaiseCanExecuteChanged();
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
                    PregovorCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string ImeText
        {
            get { return imeText; }
            set
            {
                imeText = value;
                OnPropertyChanged("ImeText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public string IzmenaPrezimeTekst
        {
            get { return izmenaPrezimeTekst; }
            set
            {
                izmenaPrezimeTekst = value;
                OnPropertyChanged("IzmenaPrezimeTekst");
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public string PrezimeText
        {
            get { return prezimeText; }
            set
            {
                prezimeText = value;
                OnPropertyChanged("PrezimeText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime IzmenaDatumZaposlenja
        {
            get { return izmenaDatumZaposlenja; }
            set
            {
                if (izmenaDatumZaposlenja != value)
                {
                    izmenaDatumZaposlenja = value;
                    OnPropertyChanged("IzmenaDatumZaposlenja");
                }
            }
        }

        public Sluzbenik SelectedSluzbenik
        {
            get { return selectedSluzbenik; }
            set
            {
                if (selectedSluzbenik != value)
                {
                    selectedSluzbenik = value;
                    OnPropertyChanged("SelectedSluzbenik");
                }
            }
        }

        public DateTime DatumZaposlenja
        {
            get { return datumZaposlenja; }
            set
            {
                if (datumZaposlenja != value)
                {
                    datumZaposlenja = value;
                    OnPropertyChanged("DatumZaposlenja");
                }
            }
        }

        public string Prezime
        {
            get { return prezime; }
            set
            {
                if (prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Ime
        {
            get { return ime; }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
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

        public ObservableCollection<Sluzbenik> Sluzbenici
        {
            get { return sluzbenici; }
            set
            {
                if (sluzbenici != value)
                {
                    sluzbenici = value;
                    OnPropertyChanged("Sluzbenici");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = Sluzbenici[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Sluzbeniks.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    result.Ime = IzmenaImeTekst;
                    result.Prezime = IzmenaPrezimeTekst;
                    result.DatumZaposlenja = IzmenaDatumZaposlenja;
                    db.SaveChanges();
                }
            }

            Sluzbenici.Clear();
            Sluzbenici = new ObservableCollection<Sluzbenik>(new AutoKompanijaDbContext().Sluzbeniks.ToList());
        }

        private bool CanEdit()
        {
            if (CurrentIndex >= 0 && IzmenaImeTekst != null && IzmenaPrezimeTekst != null && IzmenaImeTekst != "" && IzmenaPrezimeTekst != "" &&
                IzmenaDatumZaposlenja != null)
                return true;
            else
                return false;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            Sluzbenik sluz = new Sluzbenik()
            {
                Ime = ImeText,
                Prezime = PrezimeText,
                DatumZaposlenja = DatumZaposlenja
            };

            var context = new AutoKompanijaDbContext();

            context.Sluzbeniks.Add(sluz);
            context.SaveChanges();

            Sluzbenici.Clear();
            Sluzbenici = new ObservableCollection<Sluzbenik>(new AutoKompanijaDbContext().Sluzbeniks.ToList());

        }

        private bool CanAdd()
        {
            if (ImeText != null && PrezimeText != null && ImeText != "" && PrezimeText != "" &&
                DatumZaposlenja != null)
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = Sluzbenici[CurrentIndex].Id;

            Sluzbenik sluz = context.Sluzbeniks.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Sluzbeniks.Attach(sluz);
            context.Sluzbeniks.Remove(sluz);
            context.SaveChanges();

            Sluzbenici.Clear();
            Sluzbenici = new ObservableCollection<Sluzbenik>(new AutoKompanijaDbContext().Sluzbeniks.ToList());
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion

        #region PregovorFunctions
        private void OnPregovor()
        {
            using (var db = new AutoKompanijaDbContext())
            {
                var sluzbenikResult = db.Sluzbeniks.SingleOrDefault(a => a.Id == SluzbenikTypeText);
                var kupacResult = db.Kupacs.SingleOrDefault(a => a.Id == KupacTypeText);
                if (sluzbenikResult != null && kupacResult != null)
                {
                    Pregovara prego = new Pregovara()
                    {
                        KupacId = kupacResult.Id,
                        SluzbenikId = sluzbenikResult.Id
                    };

                    db.Pregovaras.Add(prego);
                    db.SaveChanges();
                }
            }
        }

        private bool CanPregovor()
        {
            using (var db = new AutoKompanijaDbContext())
            {
                List<Pregovara> progovori = db.Pregovaras.ToList();

                foreach (var preg in progovori)
                {
                    if (KupacTypeText == preg.KupacId && SluzbenikTypeText == preg.SluzbenikId)
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
