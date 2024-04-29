using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfClient
{
    public class RentalWindowViewModel : ObservableRecipient
    {
        public RestCollection<Rental> Rentals { get; set; }
        private Rental selectedRental;

        public Rental SelectedRental
        {
            get { return selectedRental; }
            set
            {
                if (value != null)
                {
                    selectedRental = new Rental()
                    {
                        RentalDate = value.RentalDate,
                        RentalId = value.RentalId,
                    };
                    OnPropertyChanged();
                    (DeleteRentalCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateRentalCommand { get; set; }
        public ICommand DeleteRentalCommand { get; set; }
        public ICommand UpdateRentalCommand { get; set; }
        public ICommand IsThereAnOngoingRentalCommand { get; set; }
        public RentalWindowViewModel()
        {

            Rentals = new RestCollection<Rental>("http://localhost:55149/", "Rental", "hub");
            CreateRentalCommand = new RelayCommand(() =>
            {
                Rentals.Add(new Rental()
                {
                    RentalDate = SelectedRental.RentalDate
                });
            });

            UpdateRentalCommand = new RelayCommand(() =>
            {
                Rentals.Update(SelectedRental);
            });

            DeleteRentalCommand = new RelayCommand(() =>
            {
                Rentals.Delete(SelectedRental.RentalId);
            },
            () =>
            {
                return SelectedRental != null;
            });


        }
        private bool isThereAnOngoingRentalResult;
        public bool IsThereAnOngoingRentalResult
        {
            get => isThereAnOngoingRentalResult;
            set => SetProperty(ref isThereAnOngoingRentalResult, value);
        }
        private void GetIsThereAnOngoingRental()
        {
            IsThereAnOngoingRentalResult = new RestService("http://localhost:55149/").GetSingle<bool>("/Stat/IsThereAnOngoingRental");
        }

    }
    
}
