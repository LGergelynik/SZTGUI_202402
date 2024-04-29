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
    public class BrandWindowViewModel : ObservableRecipient
    {
        public RestCollection<Brands> Brand { get; set; }
        private Brands selectedBrand;

        public Brands SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (value != null)
                {
                    selectedBrand = new Brands()
                    {
                        BrandId = value.BrandId,
                        BrandName = value.BrandName,
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }
        public ICommand UpdateBrandCommand { get; set; }
        public ICommand OpenMainWindowCommand { get; set; }
        public ICommand OpenRentalWindowCommand { get; set; }
        public BrandWindowViewModel()
        {

            Brand = new RestCollection<Brands>("http://localhost:55149/", "Brands", "hub");
            CreateBrandCommand = new RelayCommand(() =>
            {
                Brand.Add(new Brands()
                {
                    BrandName = SelectedBrand.BrandName
                });
            });

            UpdateBrandCommand = new RelayCommand(() =>
            {
                Brand.Update(SelectedBrand);
            });

            DeleteBrandCommand = new RelayCommand(() =>
            {
                Brand.Delete(SelectedBrand.BrandId);
            },
            () =>
            {
                return SelectedBrand != null;
            });

            SelectedBrand = new Brands();
            OpenMainWindowCommand = new RelayCommand(() =>
            {
                var mainWindow = new BrandWindow();
                mainWindow.ShowDialog();
            });

            OpenRentalWindowCommand = new RelayCommand(() =>
            {
                var rentalWindow = new RentalWindow();
                rentalWindow.ShowDialog();
            });
            

        }
    }
}
