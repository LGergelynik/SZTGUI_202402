using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfClient
{
    public class CarWindowViewModel : ObservableRecipient
    {
        public RestCollection<Car> Cars { get; set; }
        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (value != null)
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
        public ICommand GetMostPopularBrandCommand { get; set; }
        public ICommand GetRentalEventCountCommand { get; }
        public CarWindowViewModel()
        {

            Cars = new RestCollection<Car>("http://localhost:55149/", "Car", "hub");
            CreateCarCommand = new RelayCommand(() =>
            {
                Cars.Add(new Car()
                {
                    Model = SelectedCar.Model
                });
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
            
        }
        private string mostPopularBrandResult;
        public string MostPopularBrandResult
        {
            get { return mostPopularBrandResult; }
            set
            {
                mostPopularBrandResult = value;
                OnPropertyChanged(nameof(MostPopularBrandResult));
            }
        }

        public void GetMostPopularBrand()
        {
            MostPopularBrandResult = new RestService("http://localhost:55149/").GetSingle<string>("/Stat/GetMostPopularBrand").ToString();
        }
        private int rentalEventCount;
        public int RentalEventCount
        {
            get { return rentalEventCount; }
            set
            {
                rentalEventCount = value;
                OnPropertyChanged(nameof(RentalEventCount));
            }
        }
        private void GetRentalEventCount()
        {
            int carId = SelectedCar.CarId;
            RentalEventCount = new RestService("http://localhost:55149/").GetSingle<int>("/Stat/CountRentalEvents/{carId}");
        }



    }
    
}