using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfClient
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
                        BrandName = value.BrandName,
                        BrandId = value.BrandId,
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }
        public ICommand UpdateBrandCommand { get; set; }
        
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
        }
        private int brandIdInput;
        public int BrandIdInput
        {
            get => brandIdInput;
            set => SetProperty(ref brandIdInput, value);
        }

        private ICommand getCountCarsForBrandCommand;
        public ICommand GetCountCarsForBrandCommand => getCountCarsForBrandCommand ??= new RelayCommand(GetCountCarsForBrand);

        private void GetCountCarsForBrand()
        {
            int brandId = BrandIdInput;
            CountCarsForBrandResult = new RestService("http://localhost:55149/").GetSingle<int>("/Stat/CountCarsForBrand/{brandId}");
        }

        private int countCarsForBrandResult;
        public int CountCarsForBrandResult
        {
            get => countCarsForBrandResult;
            set => SetProperty(ref countCarsForBrandResult, value);
        }
        private BrandWithMostCars brandWithMostCarsResult;
        public BrandWithMostCars BrandWithMostCarsResult
        {
            get => brandWithMostCarsResult;
            set => SetProperty(ref brandWithMostCarsResult, value);
        }
        public void GetBrandWithMostCars()
        {
            RestService restService = new RestService("http://localhost:55149");
            BrandWithMostCarsResult = restService.GetSingle<BrandWithMostCars>("Stat​/FindBrandWithMostCars");
        }
    }

}
