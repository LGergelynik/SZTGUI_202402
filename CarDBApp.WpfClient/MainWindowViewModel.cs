using CarDbApp.WpfClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarDBApp.WpfClient
{
   public class MainWindowViewModel: ObservableRecipient
    {
        public RestCollection<Car> Cars { get; set; }
        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set 
            { 
                if(value !=null)
                {
                    selectedCar = new Car()
                    {
                        Model = value.Model,
                        CarId = value.CarId,
                    };
                    OnPropertyChanged();
                    (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCarCommand { get; set; }
        public ICommand DeleteCarCommand { get; set; }
        public ICommand UpdateCarCommand { get; set; }
        public ICommand OpenBrandWindowCommand { get; set; }
        public ICommand OpenRentalWindowCommand { get; set; }
        public MainWindowViewModel() 
        {
            
            Cars = new RestCollection<Car>("http://localhost:55149/", "Car", "hub");
            CreateCarCommand = new RelayCommand(() =>
            {
                Cars.Add(new Car()
                {
                    Model = SelectedCar.Model
                }) ; 
            });

            UpdateCarCommand = new RelayCommand(() =>
            {
                Cars.Update(SelectedCar);
            });

            DeleteCarCommand = new RelayCommand(() =>
            {
                Cars.Delete(SelectedCar.CarId);
            },
            () =>
            {
                return SelectedCar != null;
            });
            SelectedCar = new Car();



            OpenBrandWindowCommand = new RelayCommand(() =>
            {
                var brandWindow = new BrandWindow();
               
                brandWindow.ShowDialog();
            });

            OpenRentalWindowCommand = new RelayCommand(() =>
            {
                var rentalWindow = new RentalWindow();
                rentalWindow.ShowDialog();
            });

        }
    }
}
