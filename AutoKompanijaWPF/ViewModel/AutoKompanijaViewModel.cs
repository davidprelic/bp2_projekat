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

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand EditCommand { get; set; }

        private string nazivText;
        private string izmenaNazivTekst;
        private DateTime izmenaDatumOsnivanja;
        #endregion

        #region Constructor
        public AutoKompanijaViewModel()
        {
            DatumOsnivanja = DateTime.Now;
            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
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

        public string IzmenaNazivTekst
        {
            get { return izmenaNazivTekst; }
            set
            {
                izmenaNazivTekst = value;
                OnPropertyChanged("IzmenaNazivTekst");
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
            return CurrentIndex >= 0;
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
            if (NazivText != null && DatumOsnivanja != null)
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
    }

}
