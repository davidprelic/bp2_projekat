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
        private int id;
        private int rataLizingNaknade;
        private string vrstaLizinga;
        private ObservableCollection<Lizing> lizingPlacanja = new ObservableCollection<Lizing>();
        private Lizing selectedLizingPlacanje;
        private int currentIndex;

        public LizingViewModel()
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
    }
}
