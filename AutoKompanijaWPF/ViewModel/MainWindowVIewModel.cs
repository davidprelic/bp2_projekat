using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoKompanijaWPF.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private AutoKompanijaViewModel autoKompanijaViewModel = new AutoKompanijaViewModel();
        private AutomobilViewModel automobilViewModel = new AutomobilViewModel();
        private AutoSalonViewModel autoSalonViewModel = new AutoSalonViewModel();
        private KesViewModel kesViewModel = new KesViewModel();
        private KreditViewModel kreditVIewModel = new KreditViewModel();
        private KupacViewModel kupacViewModel = new KupacViewModel();
        private LizingViewModel lizingViewModel = new LizingViewModel();
        private PlacanjeViewModel placanjeViewModel = new PlacanjeViewModel();
        private SluzbenikViewModel sluzbenikViewModel = new SluzbenikViewModel();

        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = autoKompanijaViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "autoKompanija":
                    CurrentViewModel = autoKompanijaViewModel;
                    break;
                case "automobil":
                    CurrentViewModel = automobilViewModel;
                    break;
                case "autoSalon":
                    CurrentViewModel = autoSalonViewModel;
                    break;
                case "kes":
                    CurrentViewModel = kesViewModel;
                    break;
                case "kredit":
                    CurrentViewModel = kreditVIewModel;
                    break;
                case "kupac":
                    CurrentViewModel = kupacViewModel;
                    break;
                case "lizing":
                    CurrentViewModel = lizingViewModel;
                    break;
                case "placanje":
                    CurrentViewModel = placanjeViewModel;
                    break;
                case "sluzbenik":
                    CurrentViewModel = sluzbenikViewModel;
                    break;
            }
        }
    }
}
