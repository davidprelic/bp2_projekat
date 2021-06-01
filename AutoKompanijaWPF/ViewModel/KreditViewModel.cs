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
        private int id;
        private int kamatnaStopa;
        private int periodOtplate;
        private ObservableCollection<Kredit> kreditPlacanja = new ObservableCollection<Kredit>();
        private Kredit selectedKreditPlacanje;
        private int currentIndex;

        public KreditViewModel()
        {

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
    }
}
