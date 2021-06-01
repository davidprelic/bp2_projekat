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
        private int id;
        private int vrednost;
        private ObservableCollection<Kes> kesPlacanja = new ObservableCollection<Kes>();
        private Kes selectedKesPlacanje;
        private int currentIndex;

        public KesViewModel()
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

    }
}
