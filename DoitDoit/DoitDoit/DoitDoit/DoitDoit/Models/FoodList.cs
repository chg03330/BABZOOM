using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoitDoit.Models {
    class FoodList {
        public ICommand AddFoodCommand => new Command(AddFood);

        public ICommand RemoveFoodCommand => new Command(RemoveFood);

        public ObservableCollection<string> Foods { get; set; }

        public string FoodName { get; set; }
        public string SelectedFood { get; set; }

        public FoodList() {
            Foods = new ObservableCollection<string>();

            Foods.Add("가자미구이");
            Foods.Add("현미밥");
            Foods.Add("미역국");
        }

        public void AddFood() {
            Foods.Add(FoodName);

        }
        public void RemoveFood() {
            Foods.Remove(SelectedFood);
        }
    }
}
