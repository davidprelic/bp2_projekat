using DB_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class KupacViewModel : BindableBase
    {
        #region Fields
        private int id;
        private string ime;
        private string prezime;
        private string omiljeniAutomobil;
        private ObservableCollection<Kupac> kupci = new ObservableCollection<Kupac>();
        private Kupac selectedKupac;
        private int currentIndex;

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private string imeText;
        private string prezimeText;
        private string omiljeniAutoText;
        private string izmenaImeTekst;
        private string izmenaPrezimeTekst;
        private string izmenaOmiljeniAutoTekst;
        #endregion

        #region Constructor
        public KupacViewModel()
        {
            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            Kupci = new ObservableCollection<Kupac>(new AutoKompanijaDbContext().Kupacs.ToList());
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
                        IzmenaImeTekst = Kupci[CurrentIndex].Ime;
                        IzmenaPrezimeTekst = Kupci[CurrentIndex].Prezime;
                        IzmenaOmiljeniAutoTekst = Kupci[CurrentIndex].OmiljeniAutomobil;
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

        public string IzmenaOmiljeniAutoTekst
        {
            get { return izmenaOmiljeniAutoTekst; }
            set
            {
                izmenaOmiljeniAutoTekst = value;
                OnPropertyChanged("IzmenaOmiljeniAutoTekst");
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public string OmiljeniAutoText
        {
            get { return omiljeniAutoText; }
            set
            {
                omiljeniAutoText = value;
                OnPropertyChanged("OmiljeniAutoText");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public Kupac SelectedKupac
        {
            get { return selectedKupac; }
            set
            {
                if (selectedKupac != value)
                {
                    selectedKupac = value;
                    OnPropertyChanged("SelectedKupac");
                }
            }
        }

        public string OmiljeniAutomobil
        {
            get { return omiljeniAutomobil; }
            set
            {
                if (omiljeniAutomobil != value)
                {
                    omiljeniAutomobil = value;
                    OnPropertyChanged("OmiljeniAutomobil");
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

        public ObservableCollection<Kupac> Kupci
        {
            get { return kupci; }
            set
            {
                if (kupci != value)
                {
                    kupci = value;
                    OnPropertyChanged("Kupci");
                }
            }
        }
        #endregion

        #region EditFunctions
        private void OnEdit()
        {
            int selectedIndex = Kupci[CurrentIndex].Id;

            using (var db = new AutoKompanijaDbContext())
            {
                var result = db.Kupacs.SingleOrDefault(a => a.Id == selectedIndex);
                if (result != null)
                {
                    result.Ime = IzmenaImeTekst;
                    result.Prezime = IzmenaPrezimeTekst;
                    result.OmiljeniAutomobil = IzmenaOmiljeniAutoTekst;
                    db.SaveChanges();
                }
            }

            Kupci.Clear();
            Kupci = new ObservableCollection<Kupac>(new AutoKompanijaDbContext().Kupacs.ToList());
        }

        private bool CanEdit()
        {
            if (CurrentIndex >= 0 && IzmenaImeTekst != null && IzmenaPrezimeTekst != null && IzmenaOmiljeniAutoTekst != null &&
                IzmenaImeTekst != "" && IzmenaPrezimeTekst != "" && IzmenaOmiljeniAutoTekst != "")
                return true;
            else
                return false;
        }
        #endregion

        #region AddFunctions
        private void OnAdd()
        {
            Kupac kup = new Kupac()
            {
                Ime = ImeText,
                Prezime = PrezimeText,
                OmiljeniAutomobil = OmiljeniAutoText
            };

            var context = new AutoKompanijaDbContext();

            context.Kupacs.Add(kup);
            context.SaveChanges();

            Kupci.Clear();
            Kupci = new ObservableCollection<Kupac>(new AutoKompanijaDbContext().Kupacs.ToList());

        }

        private bool CanAdd()
        {
            if (ImeText != null && PrezimeText != null && OmiljeniAutoText != null &&
                ImeText != "" && PrezimeText != "" && OmiljeniAutoText != "")
                return true;
            return false;
        }
        #endregion

        #region DeleteFunctions
        private void OnDelete()
        {
            var context = new AutoKompanijaDbContext();

            int selectedIndex = Kupci[CurrentIndex].Id;

            Kupac kup = context.Kupacs.Where(x => x.Id == selectedIndex).FirstOrDefault();

            context.Kupacs.Attach(kup);
            context.Kupacs.Remove(kup);
            context.SaveChanges();

            Kupci.Clear();
            Kupci = new ObservableCollection<Kupac>(new AutoKompanijaDbContext().Kupacs.ToList());
        }

        private bool CanDelete()
        {
            return CurrentIndex >= 0;
        }
        #endregion

    }
}
