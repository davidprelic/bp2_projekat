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

            //List<AutoKompanija> listaAutoKomp = context.AutoKompanijas.ToList();
            //foreach (var autoKomp in listaAutoKomp)
            //{
            //    ComboBoxData.Add(autoKomp.Naziv);
            //}

            //AddItemType = ComboBoxData[0];

            AddCommand = new MyICommand(OnAdd, CanAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            EditCommand = new MyICommand(OnEdit, CanEdit);
            KreditPlacanja = new ObservableCollection<Kredit>(new AutoKompanijaDbContext().Placanjes.ToList());
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

        #endregion

        #region AddFunctions

        #endregion

        #region DeleteFunctions

        #endregion






    }
}
