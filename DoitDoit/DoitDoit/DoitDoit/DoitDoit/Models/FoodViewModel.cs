using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DoitDoit.Models
{
    class FoodViewModel
    {
        private ObservableCollection<Food> foods = new ObservableCollection<Food>();
        string Title;
        public ObservableCollection<Food> Foods
        {
            get {
                return foods;
            }
            set {
                foods = value;
            }
        }
        public FoodViewModel(String a)
        {
            Foods = new ObservableCollection<Food>();
            Title = a;
        }
    }
}
